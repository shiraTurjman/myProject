#include <netinet/in.h>
#include <errno.h>
#include <netdb.h>
#include <stdio.h>  //For standard things
#include <stdlib.h> //malloc
#include <string.h> //strlen

#include <netinet/ip_icmp.h>  //Provides declarations for icmp header
#include <netinet/udp.h>      //Provides declarations for udp header
#include <netinet/tcp.h>      //Provides declarations for tcp header
#include <netinet/ip.h>       //Provides declarations for ip header
#include <netinet/if_ether.h> //For ETH_P_ALL
#include <net/ethernet.h>     //For ether_header
#include <sys/socket.h>
#include <arpa/inet.h>
#include <sys/ioctl.h>
#include <sys/time.h>
#include <sys/types.h>
#include <unistd.h>

#include <sys/ipc.h>
#include <sys/msg.h>
#include <sys/types.h>
#include <pthread.h>
#include <stdatomic.h>
#include "queue.h"
#include "hashtable.h"
#include "find_name_server.c"
#include  <signal.h>

#include <sqlite3.h>

void INThandler(int sig);

void print_ip_header(unsigned char *, int);
void print_tcp_packet(unsigned char *, int);
void print_udp_packet(unsigned char *, int);
void print_icmp_packet(unsigned char *, int);
void PrintData(unsigned char *, int);
void addPacket(packet* newPacket,int type);
packet* takePacket(int type);
//threade function 
void ProcessPacket();
void receivePacket();
void checkThread();
void askUserThread();
int checkPacket(packet *packet);

int find_IP_and_check(char *packet_IP);
static int callback(void *data, int argc, char **argv, char **azColName);
int check(void *data, int argc, char **argv, char **azColName);
int inDB(sqlite3 *DB, const char *todb);
int ask_user(char *packet_IP);
int check_exit(int exit,char *messaggeError);
int insertTable(sqlite3 *DB, const char *toDB);
void insert(int approved,char* ip);
static int initHashFromDB(void *data, int argc, char **argv, char **azColName);
void initHash();
int select_from_db(char *select, void callback());

sqlite3 *DB;
ht *hash;
//queue ,mutex ,cv for the threade 
pacQueue *queueCheck;     //0
pacQueue *queuePrint;      //1
pacQueue *queueAskUser;   //2
// pthread_mutex_t m_check = PTHREAD_MUTEX_INITIALIZER; // mutex for cv_check
// pthread_cond_t cv_check = PTHREAD_COND_INITIALIZER; // cv for packet to check

// pthread_mutex_t m_print = PTHREAD_MUTEX_INITIALIZER; // mutex for cv_priint
// pthread_cond_t cv_print = PTHREAD_COND_INITIALIZER; // cv for packet to print

// pthread_mutex_t m_AskUser = PTHREAD_MUTEX_INITIALIZER; // mutex for cv_AskUser
// pthread_cond_t cv_AskUser = PTHREAD_COND_INITIALIZER; // cv for packet to Ask User

// pthread_mutex_t allMutex[]={&m_check,&m_print,&m_askUser};
// pthread_cond_t allCV[]={&cv_check,&cv_print,&cv_askUser};

pacQueue *allQueue[]={&queueCheck,&queuePrint,&queueAskUser};
pthread_cond_t allCV[]={PTHREAD_COND_INITIALIZER,PTHREAD_COND_INITIALIZER,PTHREAD_COND_INITIALIZER};
pthread_mutex_t allM[]={PTHREAD_MUTEX_INITIALIZER,PTHREAD_MUTEX_INITIALIZER,PTHREAD_MUTEX_INITIALIZER};

FILE *logfile;
char *logfile_template;
struct sockaddr_in source, dest;
int tcp = 0, udp = 0, icmp = 0, others = 0, igmp = 0, total = 0, i, j;
int sock_raw;


int main()
{
    //db 
    int exit;
    char *filename = "example.db";
    char *messaggeError;
    char *data = "IP TABLE:";
    exit = sqlite3_open(filename, &DB);
    if (check_exit(exit,messaggeError))
        printf("Connect Table Successfully\n");

    const char *query = "SELECT * FROM IP;";//
    printf("\nSTATE OF TABLE BEFORE INSERT\n");//
    sqlite3_exec(DB, query, callback, (void *)data, NULL);
    signal(SIGINT, INThandler);
    //
    printf("hash\n");

    hash = ht_create();
    printf("create hash\n");
     initHash();
    printf("init hash\n");

    

    logfile_template = calloc(655360, sizeof(char *));
    logfile = fopen("log.txt", "w");
    if (logfile == NULL)
    {
        printf("Unable to create log.txt file.\n");
    }
    printf("Starting...\n");

    sock_raw = socket(AF_PACKET, SOCK_RAW, htons(ETH_P_ALL));
    // setsockopt(sock_raw , SOL_SOCKET , SO_BINDTODEVICE , "eth0" , strlen("eth0")+ 1 );

    if (sock_raw < 0)
    {
        // Print the error with proper message
        perror("Socket Error");
        return 1;
    }
    //malloc a queues
    queueCheck = (pacQueue *)malloc(sizeof(pacQueue));
    queueCheck->end = queueCheck->first = NULL;
    queuePrint = (pacQueue *)malloc(sizeof(pacQueue));
    queuePrint->end = queuePrint->first = NULL;
    queueAskUser = (pacQueue *)malloc(sizeof(pacQueue));
    queueAskUser->end = queueAskUser->first = NULL;
    
    //create threade 
    
    // pthread_t thread;
    int numThread=8;
     pthread_t threads[numThread];

    int threadNum[numThread];

    //  pthread_create(&thread, NULL, receivePacket,1);
    //  printf("create receivePacket thread  %d\n",1);

    int thread_id;
   
   //  3 threads that receive a packet
   for(thread_id = 0; thread_id < 3; thread_id++)
    {
        threadNum[thread_id] = thread_id;
         pthread_create(&threads[thread_id], NULL, receivePacket,thread_id);// need to sent a parameter
        //  printf("create receivePacket thread  %d\n",thread_id);
    }
    // 3 threads check packet
    for(; thread_id < 6; thread_id++)
    {
        threadNum[thread_id] = thread_id;
        //  printf("create checkThread thread  %d\n",thread_id);
         pthread_create(&threads[thread_id], NULL, checkThread,thread_id);
    }
    // 1 thread that ask the user
     for(; thread_id < 7; thread_id++)
    {
        threadNum[thread_id] = thread_id;
        //  printf("create askUserThread thread %d\n",thread_id);
         pthread_create(&threads[thread_id], NULL, askUserThread,thread_id);
    }
//1 threade print a packet
     for(; thread_id < 8; thread_id++)
    {
        threadNum[thread_id] = thread_id;
        //  printf("create ProcessPacket thread %d\n",thread_id);
         pthread_create(&threads[thread_id], NULL,ProcessPacket,thread_id );
    }
  for(int i = 0; i <numThread; i++)
    {
        pthread_join(threads[i], NULL);
    }

    // pthread_join(thread, NULL);
//free all cv and m etc 
   
    return 0;
}

void INThandler(int sig)
{
    //  char c;

     signal(sig, SIG_IGN);
    //  printf("Do you really want to quit? [y/n] ");
    //  c = getchar();
    //  if (c == 'y' || c == 'Y')
    //  {

            // for(int i = 0; i <numThread; i++)
            //     {
            //         pthread_join(threads[i], NULL);
            //     }
        printf("TCP : %d   UDP : %d   ICMP : %d   IGMP : %d   Others : %d   Total : %d\r", tcp, udp, icmp, igmp, others, total);

         close(sock_raw);
         fprintf(logfile, logfile_template);
         printf("\nprint to logfile\n");
         // fprintf(logfile, "%s", logfile_template);
         strcpy(logfile_template, " ");
         free(logfile_template);
        printf("free logfile template\n");
            // const char *query = "SELECT * FROM IP;";//
            // char *data = "IP TABLE:";
            // sqlite3_exec(DB, query, callback, (void *)data, NULL);//
            // sqlite3_exec(DB, query, callback, (void *)data, NULL);//

          sqlite3_close(DB);
        //   for(int i=0;i<3;i++)
        //   {
        //     free(allQueue[i]);
        //     free(allCV[i]);
        //     free(allM[i]);
        //   }
        //  free(allQueue);
        //  free(allCV);
        //  printf("all cv\n");
        //  free(allM);
        //  printf("all m\n");
        //    free(buffer);
        //    printf("free buffer");
            printf("\n exit \n");
          exit(0);
    //  }
    //  else
    //       signal(SIGINT, INThandler);
    //  getchar(); // Get new line character
}

static int initHashFromDB(void *data, int argc, char **argv, char **azColName)
{
	// int i;

	{   
        // int v=argv[0]-48;
        printf("\nthis IP %s is: %s\n", argv[1],(argv[0]));
         
		 void *value = (void *)1;
         printf("value = %p\n", value);
        // printf(" ip=%s\n",argv[3]);
		// ht_set(hash, (char *)argv[1] ? (char *)argv[1] : NULL, argv[0] ? argv[0] : "-1");
         ht_set(hash, argv[1] , (void *)1);
          printf("insert ip=%s\n",argv[1]);
	}

	return 0;
}
void initHash()
{
	// select_from_db("SELECT * FROM IP", initHashFromDB);
     printf("in initHash\n");
    char *query_to_DB=calloc(50, sizeof(char *));
    sprintf(query_to_DB,"SELECT * FROM IP");
    printf("sprintf query_to_DB\n");
    char *data = "IP TABLE:";
    char *messaggeError;
    int exit ;
    printf("befur exit\n");
    exit = sqlite3_exec(DB, query_to_DB, initHashFromDB, (void *)data, NULL);
    // printf("\nSQLITE_OK: %d\n", SQLITE_OK);
     printf("exit : %d\n", exit);
    free(query_to_DB);
    printf("end in initHash\n");
    // return exit;
    // printf("accses sqlite3_exec");
    //  if(check_exit(exit,messaggeError))
    //     printf("\nRecords created Successfully!\n");//
    //  return 1;
}

void addPacket(packet* newPacket,int type)
{
    node *newNode = (node*)malloc(sizeof(node));
//    printf("addPacket: type=%d \n",type);
    newNode->pac=newPacket;
    newNode->next=NULL;
    newNode->prev=NULL;
    pacQueue *queue = (pacQueue*)malloc(sizeof(pacQueue));
    switch(type){
        case 0:
        {
            queue=queueCheck;
        }
        break;
        case 1:
        {
            queue=queuePrint;
        }
        break;
        case 2:
        {
            queue=queueAskUser;
        }
      default:
      break;
    }
    // queue = allQueue[type];
//if the queue empty
    if(queue->end==NULL)
    {
         
        queue->end=queue->first=newNode;
        // printf("Boss add the first task %d\n",task);
        //  printf("!!!!the  sent signal to cv %d\n",type);
        pthread_cond_signal(&allCV[type]);
        
    }
    else{
 // Add the new packet at the end of queue and change end
    // printf("boss start to add\n");
    queue->end->next=newNode;
    newNode->prev=queue->end;
    queue->end=newNode;
    // printf("Boss add the  task %d\n",task);
    
    if(queue->end==queue->first)
    {
        //  printf(" !!!!the  sent signal to cv %d\n",type);
        pthread_cond_signal(&allCV[type]);
    }
    }
    // printf(" finish addPacket: type=%d \n",type);
    //  free(queue);
}


packet* takePacket(int type){
//    printf("takePacket : type=%d \n",type);
   pacQueue *queue = (pacQueue*)malloc(sizeof(pacQueue));
	// If queue is empty, return NULL.
    // queue = allQueue[type];
    switch(type){
        case 0:
        {
            queue=queueCheck;
        }
        break;
        case 1:
        {
            queue=queuePrint;
        }
        break;
        case 2:
        {
            queue=queueAskUser;
        }
      default:
      break;
    }

	while (queue->first == NULL){
		
        // printf("the queue empty\n");
        //  printf("-->worker sleep cv %d\n",type);
        pthread_cond_wait(&allCV[type],&allM[type]);
        //  printf("-->wakeup cv %d\n",type);
	    //  return tackePacket(type);
      }

      packet* pac=(packet *)malloc(sizeof(packet));
      pac=queue->first->pac;

         if(queue->first->next == NULL)
         {
            queue->first=NULL;
            
        }

         else{
        
         queue->first=queue->first->next;
         queue->first->prev=NULL;

         }
        //  printf("i take\n");

         if(queue->first== NULL)
         {  
            // printf("i take the lest\n");
            queue->end=NULL;
         }
         //how i can to free packet if i return this packet 
        //  printf("takePacket : type=%d \n",type);
         return pac;
    }
    


void receivePacket()
{   
        // printf("start receivePacket\n");
     int saddr_size = 0, data_size = 0;
    struct sockaddr saddr;
    // logfile_template = calloc(655360, sizeof(char *));
    unsigned char *buffer = (unsigned char *)malloc(65536); // Its Big!
   struct iphdr *iph;
   int n=100000;
    while(1){
    //catch a packet 
        // printf("while receivePacket\n");
         saddr_size = sizeof saddr;
        // Receive a packet
        data_size = recvfrom(sock_raw, buffer, 65536, 0, &saddr, (socklen_t *)&saddr_size);
        if (data_size < 0)
        {
            printf("Recvfrom error , failed to get packets\n");
            //in the func need to return ????
            // return 1;
            break;
        }
        // printf("Recvfrom : %d\n", data_size);
        // printf("buffer %s\n", buffer);
        iph = (struct iphdr *)(buffer + sizeof(struct ethhdr));
        
        memset(&source, 0, sizeof(source));
        source.sin_addr.s_addr = iph->saddr;

        packet *pac = (packet *)malloc(sizeof(packet));
        pac->buffer=buffer;
        pac->size=data_size;
        pac->ip=inet_ntoa(source.sin_addr);
        //  printf(" catch packet ip: %s\n",pac->ip);

        //insert to queueCheck
            pthread_mutex_lock(&allM[0]);
                addPacket(pac,0);
            pthread_mutex_unlock(&allM[0]);
            // printf("add packet ip: %s\n",pac->ip);
    }

}

void checkThread()
{   
    // printf("start checkpacket\n");
    packet* pac = (packet*)malloc(sizeof(packet));
    int exists,right;
    while(1)
    {  
        //  printf("while checkpacket\n");
        //tack packet from queueCheck
        pthread_mutex_lock(&allM[0]);
            pac = takePacket(0);
        pthread_mutex_unlock(&allM[0]);
        //    printf("take packet ip to check: %s\n",pac->ip);
        // sent to check if in db  
        // exists=find_IP_and_check(pac->ip);
        exists = ht_get(hash,pac->ip);
        //  printf("found ip: %s,exists :%d\n",pac->ip,exists);
        //if true : insert to queuePrint
        if(exists==1)
        {   
            pthread_mutex_lock(&allM[1]);
            addPacket(pac,1);
            pthread_mutex_unlock(&allM[1]);
        }
        // else :
        else{ 
        // check if Right : if true : insert to queuePrint and to db
            if(exists==-1)
            {
                printf("this IP: %s (%s) is blocked\n", reverse_dns_lookup(pac->ip),pac->ip);
            }
            else{
            right=checkPacket(pac);
            // printf(" ip: %s,right :%d\n",pac->ip,right);
            if(right==1)
            {
                pthread_mutex_lock(&allM[1]);
                addPacket(pac,1);
                pthread_mutex_unlock(&allM[1]);
                //insert to db packet ip
                // insert(1, pac->ip);
                //  printf("insert ip: %s\n",pac->ip);
            }
            // else :insert  to queueAskUser
            pthread_mutex_lock(&allM[2]);
                addPacket(pac,2);
            pthread_mutex_unlock(&allM[2]);
            }
        }

    }
}


int checkPacket(packet *pac)
{
    //  printf("Checking packet \n");
    
    return (rand() % 1);
}

void askUserThread()
{   
    // printf("start ask user thread\n"); 
    packet* pac = (packet*)malloc(sizeof(packet));
     int approved,exists,right;
    while(1)
    {   
        // printf("while ask user thread\n"); 
        //tack packet from queueAskUser
        pthread_mutex_lock(&allM[2]);
        pac=takePacket(2);
        pthread_mutex_unlock(&allM[2]);

        exists = ht_get(hash,pac->ip);
        if(exists==0)
    {        
           approved = ask_user(pac->ip);
            printf("ask user approvedp: %d\n",approved);
            //if y: insert to queuePrint and to the DB 
            if(approved==1)
            {
                pthread_mutex_lock(&allM[1]);
                    addPacket(pac,1);
                pthread_mutex_unlock(&allM[1]);
                //insert to db packet ip
                insert(approved, pac->ip); 
                ht_set(hash, pac->ip, (void *)1);
            }
            else
            {

                insert(approved, pac->ip);
                ht_set(hash, pac->ip, (void *)-1);
                // printf("this IP: %s (%s) is blocked\n", reverse_dns_lookup(pac->ip),pac->ip);
            }
    }
    else 
    {
        if(exists==1)
        {   
            pthread_mutex_lock(&allM[1]);
            addPacket(pac,1);
            pthread_mutex_unlock(&allM[1]);
        }
        else
        {
            right=checkPacket(pac);
            // printf(" ip: %s,right :%d\n",pac->ip,right);
            if(right==1)
            {
                pthread_mutex_lock(&allM[1]);
                addPacket(pac,1);
                pthread_mutex_unlock(&allM[1]);
                //insert to db packet ip
                // insert(1, pac->ip);
                //  printf("insert ip: %s\n",pac->ip);
            }
            // else :insert  to queueAskUser
            pthread_mutex_lock(&allM[2]);
                addPacket(pac,2);
            pthread_mutex_unlock(&allM[2]);
            
        }
    }
    }

}




void ProcessPacket()
{  

    while(1){
     packet *pac = (packet *)malloc(sizeof(packet));
    //take a packet from queuePrint 
    pthread_mutex_lock(&allM[1]);
        pac=takePacket(1);
    pthread_mutex_unlock(&allM[1]);

    int size= pac->size;
    unsigned char *buffer=pac->buffer;
    // Get the IP Header part of this packet , excluding the ethernet header
    struct iphdr *iph = (struct iphdr *)(buffer + sizeof(struct ethhdr));
    //שליחה לבדיקה 51000
    ++total;
    switch (iph->protocol) // Check the Protocol and do accordingly...
    {
    case 1: // ICMP Protocol
        ++icmp;
        print_icmp_packet(buffer, size);
        break;

    case 2: // IGMP Protocol
        ++igmp;
        break;

    case 6: // TCP Protocol
        ++tcp;
        print_tcp_packet(buffer, size);
        break;

    case 17: // UDP Protocol
        ++udp;
        print_udp_packet(buffer, size);
        break;

    default: // Some Other Protocol like ARP etc.
        ++others;
        break;
    }
    // printf("TCP : %d   UDP : %d   ICMP : %d   IGMP : %d   Others : %d   Total : %d\r", tcp, udp, icmp, igmp, others, total);
    }
    
}

void print_ethernet_header(unsigned char *Buffer, int Size)
{
    struct ethhdr *eth = (struct ethhdr *)Buffer;

    // sprintf(logfile_template, "", logfile_template);
    sprintf(logfile_template, "%s\nEthernet Header\n", logfile_template);
    sprintf(logfile_template, "%s   |-Destination Address : %.2X-%.2X-%.2X-%.2X-%.2X-%.2X \n", logfile_template, eth->h_dest[0], eth->h_dest[1], eth->h_dest[2], eth->h_dest[3], eth->h_dest[4], eth->h_dest[5]);
    sprintf(logfile_template, "%s   |-Source Address      : %.2X-%.2X-%.2X-%.2X-%.2X-%.2X \n", logfile_template, eth->h_source[0], eth->h_source[1], eth->h_source[2], eth->h_source[3], eth->h_source[4], eth->h_source[5]);
    sprintf(logfile_template, "%s   |-Protocol            : %u \n", logfile_template, (unsigned short)eth->h_proto);
}

void print_ip_header(unsigned char *Buffer, int Size)
{
    print_ethernet_header(Buffer, Size);

    unsigned short iphdrlen;

    struct iphdr *iph = (struct iphdr *)(Buffer + sizeof(struct ethhdr));
    iphdrlen = iph->ihl * 4;

    // memset(&source, 0, sizeof(source));
    // source.sin_addr.s_addr = iph->saddr;

    memset(&dest, 0, sizeof(dest));
    dest.sin_addr.s_addr = iph->daddr;

    sprintf(logfile_template, "%s\n", logfile_template);
    sprintf(logfile_template, "%sIP Header\n", logfile_template);
    sprintf(logfile_template, "%s   |-IP Version        : %d\n", logfile_template, (unsigned int)iph->version);
    sprintf(logfile_template, "%s   |-IP Header Length  : %d DWORDS or %d Bytes\n", logfile_template, (unsigned int)iph->ihl, ((unsigned int)(iph->ihl)) * 4);
    sprintf(logfile_template, "%s   |-Type Of Service   : %d\n", logfile_template, (unsigned int)iph->tos);
    sprintf(logfile_template, "%s   |-IP Total Length   : %d  Bytes(Size of Packet)\n", logfile_template, ntohs(iph->tot_len));
    sprintf(logfile_template, "%s   |-Identification    : %d\n", logfile_template, ntohs(iph->id));
    // fprintf(logfile , "   |-Reserved ZERO Field   : %d\n",(unsigned int)iphdr->ip_reserved_zero);
    // fprintf(logfile , "   |-Dont Fragment Field   : %d\n",(unsigned int)iphdr->ip_dont_fragment);
    // fprintf(logfile , "   |-More Fragment Field   : %d\n",(unsigned int)iphdr->ip_more_fragment);
    sprintf(logfile_template, "%s   |-TTL      : %d\n", logfile_template, (unsigned int)iph->ttl);
    sprintf(logfile_template, "%s   |-Protocol : %d\n", logfile_template, (unsigned int)iph->protocol);
    sprintf(logfile_template, "%s   |-Checksum : %d\n", logfile_template, ntohs(iph->check));
    sprintf(logfile_template, "%s   |-Source IP        : %s\n", logfile_template, inet_ntoa(source.sin_addr));
    sprintf(logfile_template, "%s   |-Destination IP   : %s\n", logfile_template, inet_ntoa(dest.sin_addr));
}

void print_tcp_packet(unsigned char *Buffer, int Size)
{
    unsigned short iphdrlen;

    // Get the IP Header part of this packet
    struct iphdr *iph = (struct iphdr *)(Buffer + sizeof(struct ethhdr));

    // TODO: check
    iphdrlen = iph->ihl * 4;

    // tcph -> pointer to the header data
    struct tcphdr *tcph = (struct tcphdr *)(Buffer + iphdrlen + sizeof(struct ethhdr));

    int header_size = sizeof(struct ethhdr) + iphdrlen + tcph->doff * 4;

    sprintf(logfile_template, "%s\n\n***********************TCP Packet*************************\n", logfile_template);

    print_ip_header(Buffer, Size);

    sprintf(logfile_template, "%s\n", logfile_template);
    sprintf(logfile_template, "%sTCP Header\n", logfile_template);
    sprintf(logfile_template, "%s   |-Source Port      : %u\n", logfile_template, ntohs(tcph->source));
    sprintf(logfile_template, "%s   |-Destination Port : %u\n", logfile_template, ntohs(tcph->dest));
    sprintf(logfile_template, "%s   |-Sequence Number    : %u\n", logfile_template, ntohl(tcph->seq));
    sprintf(logfile_template, "%s   |-Acknowledge Number : %u\n", logfile_template, ntohl(tcph->ack_seq));
    sprintf(logfile_template, "%s   |-Header Length      : %d DWORDS or %d BYTES\n", logfile_template, (unsigned int)tcph->doff, (unsigned int)tcph->doff * 4);
    // fprintf(logfile , "   |-CWR Flag : %d\n",(unsigned int)tcph->cwr);
    // fprintf(logfile , "   |-ECN Flag : %d\n",(unsigned int)tcph->ece);
    sprintf(logfile_template, "%s   |-Urgent Flag          : %d\n", logfile_template, (unsigned int)tcph->urg);
    sprintf(logfile_template, "%s   |-Acknowledgement Flag : %d\n", logfile_template, (unsigned int)tcph->ack);
    sprintf(logfile_template, "%s   |-Push Flag            : %d\n", logfile_template, (unsigned int)tcph->psh);
    sprintf(logfile_template, "%s   |-Reset Flag           : %d\n", logfile_template, (unsigned int)tcph->rst);
    sprintf(logfile_template, "%s   |-Synchronise Flag     : %d\n", logfile_template, (unsigned int)tcph->syn);
    sprintf(logfile_template, "%s   |-Finish Flag          : %d\n", logfile_template, (unsigned int)tcph->fin);
    sprintf(logfile_template, "%s   |-Window         : %d\n", logfile_template, ntohs(tcph->window));
    sprintf(logfile_template, "%s   |-Checksum       : %d\n", logfile_template, ntohs(tcph->check));
    sprintf(logfile_template, "%s   |-Urgent Pointer : %d\n", logfile_template, tcph->urg_ptr);
    sprintf(logfile_template, "%s\n", logfile_template);
    sprintf(logfile_template, "%s                        DATA Dump                         ", logfile_template);
    sprintf(logfile_template, "%s\n", logfile_template);

    sprintf(logfile_template, "%sIP Header\n", logfile_template);
    PrintData(Buffer, iphdrlen);

    sprintf(logfile_template, "%sTCP Header\n", logfile_template);
    PrintData(Buffer + iphdrlen, tcph->doff * 4);

    sprintf(logfile_template, "%sData Payload\n", logfile_template);
    PrintData(Buffer + header_size, Size - header_size);

    sprintf(logfile_template, "%s\n###########################################################", logfile_template);
}

void print_udp_packet(unsigned char *Buffer, int Size)
{

    unsigned short iphdrlen;

    struct iphdr *iph = (struct iphdr *)(Buffer + sizeof(struct ethhdr));
    iphdrlen = iph->ihl * 4;

    struct udphdr *udph = (struct udphdr *)(Buffer + iphdrlen + sizeof(struct ethhdr));

    int header_size = sizeof(struct ethhdr) + iphdrlen + sizeof udph;

    sprintf(logfile_template, "%s\n\n***********************UDP Packet*************************\n", logfile_template);

    print_ip_header(Buffer, Size);

    sprintf(logfile_template, "%s\nUDP Header\n", logfile_template);
    sprintf(logfile_template, "%s   |-Source Port      : %d\n", logfile_template, ntohs(udph->source));
    sprintf(logfile_template, "%s   |-Destination Port : %d\n", logfile_template, ntohs(udph->dest));
    sprintf(logfile_template, "%s   |-UDP Length       : %d\n", logfile_template, ntohs(udph->len));
    sprintf(logfile_template, "%s   |-UDP Checksum     : %d\n", logfile_template, ntohs(udph->check));

    // sprintf(logfile_template, "");
    sprintf(logfile_template, "%s\nIP Header\n", logfile_template);
    PrintData(Buffer, iphdrlen);

    sprintf(logfile_template, "%sUDP Header\n", logfile_template);
    PrintData(Buffer + iphdrlen, sizeof udph);

    sprintf(logfile_template, "%sData Payload\n", logfile_template);

    // Move the pointer ahead and reduce the size of string
    PrintData(Buffer + header_size, Size - header_size);

    sprintf(logfile_template, "%s\n###########################################################", logfile_template);
}

void print_icmp_packet(unsigned char *Buffer, int Size)
{
    unsigned short iphdrlen;

    struct iphdr *iph = (struct iphdr *)(Buffer + sizeof(struct ethhdr));
    iphdrlen = iph->ihl * 4;

    struct icmphdr *icmph = (struct icmphdr *)(Buffer + iphdrlen + sizeof(struct ethhdr));

    int header_size = sizeof(struct ethhdr) + iphdrlen + sizeof icmph;

    sprintf(logfile_template, "%s\n\n***********************ICMP Packet*************************\n", logfile_template);

    print_ip_header(Buffer, Size);

    sprintf(logfile_template, "%s\n", logfile_template);

    sprintf(logfile_template, "%sICMP Header\n", logfile_template);
    sprintf(logfile_template, "%s   |-Type : %d", logfile_template, (unsigned int)(icmph->type));

    if ((unsigned int)(icmph->type) == 11)
    {
        sprintf(logfile_template, "%s  (TTL Expired)\n", logfile_template);
    }
    else if ((unsigned int)(icmph->type) == ICMP_ECHOREPLY)
    {
        sprintf(logfile_template, "%s  (ICMP Echo Reply)\n", logfile_template);
    }

    sprintf(logfile_template, "%s   |-Code : %d\n", logfile_template, (unsigned int)(icmph->code));
    sprintf(logfile_template, "%s   |-Checksum : %d\n", logfile_template, ntohs(icmph->checksum));
    // fprintf(logfile , "   |-ID       : %d\n",ntohs(icmph->id));
    // fprintf(logfile , "   |-Sequence : %d\n",ntohs(icmph->sequence));

    sprintf(logfile_template, "%s\nIP Header\n", logfile_template);
    PrintData(Buffer, iphdrlen);

    sprintf(logfile_template, "%sUDP Header\n", logfile_template);
    PrintData(Buffer + iphdrlen, sizeof icmph);

    sprintf(logfile_template, "%sData Payload\n", logfile_template);

    // Move the pointer ahead and reduce the size of string
    PrintData(Buffer + header_size, (Size - header_size));

    sprintf(logfile_template, "%s\n###########################################################", logfile_template);
}

void PrintData(unsigned char *data, int Size)
{
    int i = 0, j = 0;

    // TODO: change
    if (Size > 0)
        sprintf(logfile_template, "%s %02X", logfile_template, (unsigned int)data[0]);
    for (i = 0; i < Size; i++)
    {
        // TODO: change
        // if (/*i != 0 &&*/ i % 16 == 0) // if one line of hex printing is complete...
        // TODO 	CHANGE
        if (i & 0x0F == 0)
        {
            sprintf(logfile_template, "%s         ", logfile_template);
            for (j = i - 16; j < i; j++)
            {
                if (data[j] >= 32 && data[j] <= 128)
                    sprintf(logfile_template, "%s%c", logfile_template, (unsigned char)data[j]); // if its a number or alphabet

                else
                    sprintf(logfile_template, "%s.", logfile_template); // otherwise print a dot
            }
            sprintf(logfile_template, "%s\n   ", logfile_template);
        }

        // TODO: change
        //  if (i % 16 == 0)
        //  fprintf(logfile, "         ");
        sprintf(logfile_template, "%s %02X", logfile_template, (unsigned int)data[i]);
        // printf(" ttt%d",logfile_template[i]);
    }

    if (i == Size - 1) // print the last spaces
    {
        // TODO 	CHANGE
        //  int end = 15 -i%16;
        int end = 15 - i & 0x0F;

        for (j = 0; j < end; j++)
        {
            sprintf(logfile_template, "%s   ", logfile_template); // extra spaces
        }

        sprintf(logfile_template, "%s         ", logfile_template);
        // TODO 	CHANGE
        for (j = i & 0xF0; j <= i; j++)
        // for (j = i & 0xF0; j <= i; j++)
        {
            if (data[j] >= 32 && data[j] <= 128)
            {
                sprintf(logfile_template, "%s%c", logfile_template, (unsigned char)data[j]);
            }
            else
            {
                sprintf(logfile_template, "%s.", logfile_template);
            }
        }
        sprintf(logfile_template, "%s\n", logfile_template);
    }
}




//db 
static int callback(void *data, int argc, char **argv, char **azColName)
{
    int i;
    fprintf(stderr, "%s: \n", (const char *)data);
    for (i = 0; i < argc; i++)
    {
        printf("%d:%s = %s\n", i, azColName[i], argv[i] ? argv[i] : "NULL");
    }
    printf("\n");
    return 0;
}

int check(void *data, int argc, char **argv, char **azColName)
{   char* flag;
    // printf("\nthis IP %s is: %s\n", argv[2],argv[0]);//
    flag = argv[1];
    if (flag==1)
    {
        //the ip is approved 
        return 0;
    }
    // printf("flag is: %s\n", flag);
    // Here we need to block if this TRUE and confirm if this FALSE
    else
    {
    return -1;
    }
}
int inDB(sqlite3 *DB, const char *todb)
{
    // printf("entered inDB\n%s", todb);
    char *data = "IP TABLE:";
    char *messaggeError;
    int exit ;
    exit = sqlite3_exec(DB, todb, check, (void *)data, NULL);
    // printf("\nSQLITE_OK: %d\n", SQLITE_OK);
    // printf("exit : %d\n", exit);

    return exit;
    //printf("accses sqlite3_exec");
    //  if(check_exit(exit,messaggeError))
    //     printf("\nRecords created Successfully!\n");//
    //  return 1;
}

int ask_user(char *packet_IP)
{
    printf("you approve packet from:%s (ip: :%s) ?\npress y to accept or n to not\n", reverse_dns_lookup(packet_IP), packet_IP);
    char ansuer;
    scanf(" %s", &ansuer);
    printf("%c ", ansuer);
    int approved;
    if (ansuer == 'y'|| ansuer=='Y')
    {
        printf("ansuer: %c\n", ansuer);
        approved = 1;
        return approved;
    }
    // if(ansuer == 'n'|| ansuer=='N')
    // {

        approved=0;
         return approved;
    // }
    // printf("Enter only Y or N");
    // return ask_user(packet_IP);
}

int check_exit(int exit,char *messaggeError){
    
 if (exit != SQLITE_OK)
    {
        perror("Error query in check exit\n");
        sqlite3_free(messaggeError);
        return -1;
    }
   return 1;
}
int insertTable(sqlite3 *DB, const char *toDB)
{
    char *messaggeError;
    int exit = sqlite3_exec(DB, toDB, NULL, 0, &messaggeError);
    check_exit(exit,messaggeError);
    return 0;
}

void insert(int approved,char* ip)
{
    char *query_to_DB=calloc(50, sizeof(char *));
    sprintf(query_to_DB, "INSERT INTO IP VALUES('%d','%s');", approved, ip);
    printf("insert to DB ip:=%s\n",ip);
    // printf("APPROVED: %d\n%s \n", approved, query_to_DB);
    int wasWork = insertTable(DB, query_to_DB);
    if (wasWork < 0)
    {
        perror("the insert was felled");
    
    }
    free(query_to_DB);
}


int find_IP_and_check(char *packet_IP)
{
    // printf("entered find_IP_and_check\n");
    //APPROVED == NULL;
    char *query_to_DB=calloc(50, sizeof(char *));
    sprintf(query_to_DB,"SELECT * FROM IP WHERE NUM='%s';", packet_IP);
    // printf("entered find_IP_and_check\n%s\n",query_to_DB);
    int exists=inDB(DB, query_to_DB);
    // printf("succes inDB\n");
    if (exists == 0)
    {  
         return 0;
    }
    
    free(query_to_DB);
    return 1;
}

int select_from_db(char *select, void callback())
{
    int rc = sqlite3_exec(DB, select, callback, NULL, NULL);

    if (rc != SQLITE_OK)
    {
        return -1;
    }

    return 0;
   
}