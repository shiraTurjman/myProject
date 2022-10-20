#include <stdio.h>
#include <stdlib.h>
#include <pcap.h>
#include <arpa/inet.h>
#include <net/ethernet.h>
#include <netinet/ip.h>
#include <netinet/udp.h>
#include <netinet/in.h>
// #include "TDR.h"
#include "ht_conn_list.h"
#include "queueArr.h"
// #include "linked_list.h"
// #include "conn_struct.h"
#include <json-c/json.h>
#include <string.h>

#define IP_VERSION 4
#define YOUTUBE_PORT 443
#define UDP_PROTOCOL 17
#define SIZE_CHAR_CONNDATA 300000//655360
                           

int	request_packet_threshold ; //B  
int	Minimum_video_connection_size ; //MB
int	inbound_packets_in_range_min ; //B
int	inbound_packets_in_range_max ; //B
int	outbound_packets_in_range_min ;
int	outbound_packets_in_range_max ;
int	max_diff_time_inbound_threshold ;
int	min_diff_time_inbound_threshold ;
int	number_of_videos_to_output_statistics_per_video ;
int	max_number_of_connections ;
int	max_number_of_transaction_per_video;
int	video_connection_timeout ;// seconds

int connection_id = 1;
int num_conn=0;
ht *hash_table;
connection *connArr;
char **dataArr;
queueArr *vacIndex;
linked_list *list_time;

FILE *fpt;
int num_video=0;
double all_tdr_time=0;
int all_size=0;
int num_all_tdr=0;
double all_diff_time=0;

struct in_addr *addr_client;  //to print ip client
char ipClient[16];
struct in_addr *addr_server;  //to print ip server
char ipServer[16];
double all_conn_time=0;
double all_time_between_two_tdr=0;

void readJson();
void get_packet(u_char *args, const struct pcap_pkthdr *header, const u_char *packet);
void serverToClient(int index,double time,int size,five_tuple conn_key);
void clientToServer(int index,double time,int size,five_tuple conn_key);
void data_new_req(int index,double time);
int new_connection(five_tuple tuple,double time);
void data_new_conn(five_tuple tuple5,int index,double time);
void save_tdr(int index,double nowTime);
void close_conn(int index,five_tuple conn_key);
void reset_conn(int index);
void save_conn(int index);
double mydiff(double start, double end);
void init();
void free_all();
void video_statistics();

int main()
{
 
    readJson();
    init();
   
    fpt = fopen("TDRFile.csv", "w+");
    fprintf(fpt,"Conn_id, Client_IP, Server_IP, IP_protocol, UDP_client_port, UDP_server_port, Transaction_id ,Start time,num_in_packets,num_out_packets,max_packet_size_in,min_packet_size_in,max_diff_time_inbound,min_diff_time_inbound,sumSquareInboundPacketTimeDiff,RTT ,total size\n");

    addr_client = malloc(sizeof(struct in_addr));
    addr_server = malloc(sizeof(struct in_addr));
    
    char errbuf[PCAP_ERRBUF_SIZE]; /* error buffer */
	pcap_t *handle;	

    handle = pcap_open_offline("capture_file.pcap", errbuf);
	if (handle == NULL)
	{
		fprintf(stderr, "Couldn't open offline  %s\n", errbuf); 
		exit(EXIT_FAILURE);
	}

    pcap_loop(handle, 0, get_packet, NULL);

    
     while(list_time->first!=NULL)
        {    
            // printf("close %d\n",list_time->first->value);
            close_conn(list_time->first->value,connArr[list_time->first->value].tuple);
            // printf("finish close %d\n",list_time->first->value);
        }


    video_statistics();
    free_all();
    free(handle);
    fclose(fpt);
    printf("\n\n%d\n",num_conn);
   

    return 0;
    
}
void readJson()
{
    json_object *root = json_object_from_file("config.json");
    request_packet_threshold = (json_object_get_int(json_object_object_get(root, "request_packet_threshold")));
    Minimum_video_connection_size = json_object_get_int(json_object_object_get(root, "minimum_video_connection_size"));
    inbound_packets_in_range_min = json_object_get_int(json_object_object_get(root, "inbound_packets_in_range_min"));
    inbound_packets_in_range_max = json_object_get_int(json_object_object_get(root, "inbound_packets_in_range_max"));
    outbound_packets_in_range_min = json_object_get_int(json_object_object_get(root, "outbound_packets_in_range_min"));
    outbound_packets_in_range_max = json_object_get_int(json_object_object_get(root, "outbound_packets_in_range_max"));
    max_diff_time_inbound_threshold = json_object_get_int(json_object_object_get(root, "max_diff_time_inbound_threshold"));
    number_of_videos_to_output_statistics_per_video = json_object_get_int(json_object_object_get(root, "number_of_videos_to_output_statistics_per_video"));
    max_number_of_connections = json_object_get_int(json_object_object_get(root, "max_number_of_connections"));
    max_number_of_transaction_per_video = json_object_get_int(json_object_object_get(root, "max_number_of_transaction_per_video"));
    video_connection_timeout = json_object_get_int(json_object_object_get(root, "video_connection_timeout"));
    

}

void init()
{
    hash_table=ht_create();
    connArr=(connection*)malloc(sizeof(connection)*max_number_of_connections+1);
    dataArr=(char**)malloc(sizeof(char*)*max_number_of_connections+1);
    for (int i=0; i<max_number_of_connections; i++)
    { 
        connArr[i].time_node=node_create(i); 
        dataArr[i]=(char*)malloc(sizeof(char)*SIZE_CHAR_CONNDATA);
    }
    vacIndex=qa_create();
    // list_time=(linked_list*)malloc(sizeof(linked_list));
    list_time=list_create();
}

void free_all()
{
    ht_destroy(hash_table);
    for (int i=0; i<max_number_of_connections; i++)
    { 
        free(connArr[i].time_node);
        free(dataArr[i]);
    }
    free(connArr);
    free(dataArr);
    qa_destroy(vacIndex);
    free(list_time);
    free(addr_client);
    free(addr_server);
}
void get_packet(u_char *args, const struct pcap_pkthdr *header, const u_char *packet)
{
    
    struct ether_header *e_hdr;
    struct ip *ip_hdr;  
    struct udphdr *u_hdr;

    if (header->len < sizeof(struct ether_header)) {
        // puts("Defevtive packet\n");
        return;
    }

    e_hdr = (struct ether_header *)packet;

    if (ntohs(e_hdr->ether_type) != ETHERTYPE_IP) {
    //    printf("This packet is 0x%04x\n", ntohs(e_hdr->ether_type));
        return;
    }

    ip_hdr = (struct ip *)(packet + sizeof(struct ether_header));

    if (ip_hdr->ip_v != IP_VERSION){
        //  printf("Unkown packet");
        return;
    }

    if (ip_hdr->ip_p != UDP_PROTOCOL) {
        //  puts("This packet is not UDP");
        return;
     }
    u_hdr = (struct udphdr *)(packet + sizeof(struct ether_header) + sizeof(struct ip));
   

     uint16_t source_port= ntohs(u_hdr->source);
     uint16_t destination_port=ntohs(u_hdr->dest);
     uint8_t l4=ip_hdr->ip_p;
      double time;
//if udp and port 443 
   if(YOUTUBE_PORT==source_port||YOUTUBE_PORT==destination_port){

        
        five_tuple tuple5;
        tuple5.l4=l4;
//         __time_t sec=header->ts.tv_sec;
// //    __time_t tv_sec;    /* Seconds.  */
//        __suseconds_t usec=header->ts.tv_usec;  /* Microseconds.  */
       
//        printf("time: %f\n",(double)sec);
//        printf("sec %f\n",(double)usec*1e-6);
//        double time=(double)sec+(double)usec*1e-6;
//        printf("time: %f\n",time);

        time=1.0 *header->ts.tv_sec+ 1e-6 * header->ts.tv_usec; 
        int size=ntohs(u_hdr->len);

        int src_ip;
        int dst_ip;
        src_ip = ip_hdr->ip_src.s_addr;
        dst_ip = ip_hdr->ip_dst.s_addr;
        
        if(YOUTUBE_PORT==source_port)
        {
            //source_port=443..... packet from the server

            tuple5.client_port=destination_port;
            tuple5.server_port=source_port;
            tuple5.server_ip=src_ip;
            tuple5.client_ip=dst_ip;
            
            
            int index = ht_get(hash_table,tuple5);
            
            if(index == -1)
            {
                // this connection not found... and this packet is not request
                return;
            }
            serverToClient(index,time,size,tuple5);
            
           
        }
        else
        if(YOUTUBE_PORT==destination_port)
        {
            //destination_port=443... packet from client
            tuple5.client_port=source_port;
            tuple5.server_port=destination_port;
            tuple5.server_ip=dst_ip;
            tuple5.client_ip=src_ip;
    
            
            int index = ht_get(hash_table,tuple5);
            if(index == -1)
            {
               //the connect not found
                if(size>=request_packet_threshold)
                //this packet is request
               {
                 new_connection(tuple5,time);
                 
               }
               return;

            }

           else{

            clientToServer(index,time,size,tuple5);

           }
        }
     //שעברו  connection היא תעבור רק על   timeout לולאה לבדיקת 
     // אחרי 20 שניות  connection  לולאה זאת כאן (ולא בשורה 298- רק כשנגמר המקום ) כי היתה דרישה לסגור 
        while(list_time->first!=NULL&&mydiff(connArr[list_time->first->value].last_packet_time,time)>video_connection_timeout)
        {    
            // printf("timeout to %d\n",list_time->first->value);
            close_conn(list_time->first->value,connArr[list_time->first->value].tuple);
            
        }
    
    
   }

   
}
    
int new_connection(five_tuple tuple,double time)
{
    num_conn++;
    int index = dequeue(vacIndex);
    
    int add= ht_set(hash_table,tuple,index);
    if(add==-1)
    {
        printf("can't add new connection\n");
        return -1;
        //close packet time out 

        //  while(list_time->first!=NULL&&mydiff(connArr[list_time->first->value].last_packet_time,time)>video_connection_timeout)
        // {    
        //     printf("timeout to %d\n",list_time->first->value);
        //     close_conn(list_time->first->value,connArr[list_time->first->value].tuple);
            
        // }

    }
    
    data_new_conn(tuple,index,time);
    data_new_req(index,time);
    add_node(list_time, connArr[index].time_node);
   
    return 0;
}

void serverToClient(int index,double time,int size,five_tuple conn_key)
{
    double deff = mydiff(connArr[index].last_packet_time,time);
    
    if(deff>video_connection_timeout)
    {
       //timeout
       //close conn
       close_conn(index,conn_key);
       return;
    }

    if(size<=inbound_packets_in_range_max && size>=inbound_packets_in_range_min)
    {    
        //data packet 
        all_diff_time+=deff;
         connArr[index].total_size+=size;
         connArr[index].tdr.num_inbound_packet_in_range++;
        
        if(size>connArr[index].tdr.max_packet_size_inbound)
        {
            connArr[index].tdr.max_packet_size_inbound=size;
        }
        else
        {
            if(size<connArr[index].tdr.min_packet_size_inbound)
            {
                connArr[index].tdr.min_packet_size_inbound=size;
            }
        }
        if(connArr[index].tdr.last_time_inbound!=0)
        {
            //not first packet from server
            double deffIn = mydiff(connArr[index].tdr.last_time_inbound,time);
            float square=deffIn*deffIn;
            connArr[index].tdr.sumSquareInboundPacketTimeDiff+=square;
        
            if(deffIn>connArr[index].tdr.max_diff_time_inbound)
            {
                connArr[index].tdr.max_diff_time_inbound=deffIn;
            }
            else
            {
                if(deffIn<connArr[index].tdr.min_diff_time_inbound)
                {
                    connArr[index].tdr.min_diff_time_inbound=deffIn;
                }

            }
        }
        connArr[index].tdr.last_time_inbound=time;
         
    }
    if(connArr[index].tdr.RTT==0)
    {   
        //first packet from the server
        connArr[index].tdr.RTT=mydiff(connArr[index].tdr.start_time,time);
        //update RTT
    }
    connArr[index].last_packet_time=time;
    
    move_node(list_time, connArr[index].time_node);
    
    

}

void clientToServer(int index,double time,int size,five_tuple conn_key)
{

     double deff = mydiff(connArr[index].last_packet_time,time);
    if(deff>(double)video_connection_timeout)
    {
       //timeout
        if(size>=request_packet_threshold)
        {   
            //this packet is request
            //close conn and open new conn
            save_conn(index);
            move_node(list_time, connArr[index].time_node);
            data_new_conn(connArr[index].tuple,index,time);
            data_new_req(index,time);
            
            return;
        }
       //not request... close conn
        close_conn(index,conn_key);
       return;
    }
    if(size>=request_packet_threshold)
    {
        //new request
        //close tdr
        save_tdr(index,time);
        data_new_req(index,time);
        
    }
    else
    {  
        //not time out and not request
        all_diff_time+=deff;
        connArr[index].tdr.num_outbound_packet_in_range++;
        connArr[index].last_packet_time=time;
        
    }

     
    
    move_node(list_time, connArr[index].time_node);
    
}

void data_new_conn(five_tuple tuple5,int index,double time)
{
    reset_conn(index);
    connArr[index].conn_id=connection_id++;
    connArr[index].tuple=tuple5;
    connArr[index].tdr.tdr_id=0;
    connArr[index].total_size=0;
    connArr[index].start_time=time;
    connArr[index].time_between_two_tdr=0;
   
}

void data_new_req(int index,double time)
{
        connArr[index].last_packet_time=time;
        connArr[index].tdr.tdr_id++;
        connArr[index].tdr.start_time=time;
        connArr[index].tdr.num_inbound_packet_in_range=0;
        connArr[index].tdr.num_outbound_packet_in_range=0;
        connArr[index].tdr.max_packet_size_inbound=0;
        connArr[index].tdr.min_packet_size_inbound=inbound_packets_in_range_max+1;
        connArr[index].tdr.max_diff_time_inbound=0;
        connArr[index].tdr.min_diff_time_inbound=video_connection_timeout;
         
        // connArr[index].tdr.diff_time=0;
       if(connArr[index].tdr.tdr_id>=max_number_of_transaction_per_video)
       {
            printf("can't create new transaction... There are already %d transactions\n",max_number_of_transaction_per_video);
            // close_conn(index,connArr[index].tuple);
            //close connection and open new connection
            save_conn(index);
            data_new_conn(connArr[index].tuple,index,time);
            data_new_req(index,time);

       }

}

void close_conn(int index,five_tuple conn_key)
{
   
    if(connArr[index].conn_id > 0){
    save_conn(index);
    reset_conn(index);
    remove_node(list_time, connArr[index].time_node);
    ht_delete(hash_table,conn_key);
    int in=inqueue(vacIndex,index);
   
    if(in==-1)
    {
        printf("can't insert to queue\n");
    }
     }   

}
void reset_conn(int index)
{
    connArr[index].total_size = 0;
    connArr[index].last_packet_time = 0;
    connArr[index].tdr.tdr_id = 0;
    connArr[index].tdr.start_time=0;
    connArr[index].tdr.num_inbound_packet_in_range = 0;
    connArr[index].tdr.num_outbound_packet_in_range = 0;
    connArr[index].tdr.max_packet_size_inbound = 0;
    connArr[index].tdr.min_packet_size_inbound = 0;
    connArr[index].tdr.max_diff_time_inbound = 0;
    connArr[index].tdr.min_diff_time_inbound = 0;
    connArr[index].tdr.sumSquareInboundPacketTimeDiff = 0;
    connArr[index].conn_tdrs_time = 0;
    connArr[index].tdr.last_time_inbound = 0;
    connArr[index].tdr.RTT = 0;
    connArr[index].time_between_two_tdr=0;
    connArr[index].conn_id = 0;
    connArr[index].tuple.client_ip = 0;
    connArr[index].tuple.server_ip = 0;
    connArr[index].tuple.client_port = 0;
    connArr[index].tuple.server_port = 0;
    connArr[index].tuple.l4 = 0;
}
void save_tdr(int index,double nowTime)
{
   
     if(connArr[index].conn_id > 0)
    {
        char buff[20];
        struct timeval time;
        time.tv_sec = (int)connArr[index].tdr.start_time;
        struct tm *timeinfo;
        timeinfo = localtime(&time);
        strftime(buff, sizeof(buff), " %H:%M:%S", timeinfo);
        // printf(" %s\n", buff);
        addr_client->s_addr = connArr[index].tuple.client_ip;
        addr_server->s_addr = connArr[index].tuple.server_ip;
        strcpy(ipClient,inet_ntoa(*addr_client));
        strcpy(ipServer,inet_ntoa(*addr_server));
        sprintf(dataArr[index],"%s %d, %s, %s, %d, %d, %d, %d, %f, %d, %d, %d, %d, %f, %f, %f, %f, %d\n",
        dataArr[index],
        connArr[index].conn_id,
        ipClient,
        ipServer,
        connArr[index].tuple.l4,
        connArr[index].tuple.client_port,
        connArr[index].tuple.server_port,
        connArr[index].tdr.tdr_id,
        connArr[index].tdr.start_time,
        connArr[index].tdr.num_inbound_packet_in_range,
        connArr[index].tdr.num_outbound_packet_in_range,
        connArr[index].tdr.max_packet_size_inbound,
        connArr[index].tdr.min_packet_size_inbound<inbound_packets_in_range_max?connArr[index].tdr.min_packet_size_inbound:0,
        connArr[index].tdr.max_diff_time_inbound,
        connArr[index].tdr.min_diff_time_inbound<video_connection_timeout?connArr[index].tdr.min_diff_time_inbound:0,
        connArr[index].tdr.sumSquareInboundPacketTimeDiff,
        connArr[index].tdr.RTT,
        connArr[index].total_size
        );
        //data from statistics
        double tdr_time=mydiff(connArr[index].tdr.start_time,connArr[index].last_packet_time);
        connArr[index].conn_tdrs_time+=tdr_time;
        if(nowTime>0)
       { double diff_dtr=mydiff(connArr[index].tdr.start_time,nowTime);
        connArr[index].time_between_two_tdr+=diff_dtr;
      
    }
}
}

void save_conn(int index)
{

    if(connArr[index].total_size >= Minimum_video_connection_size)
    {   //video connection
        double diff= mydiff(connArr[index].start_time,connArr[index].last_packet_time);
        all_conn_time+=diff;

        save_tdr(index,0);//save last tdr
        fprintf(fpt,dataArr[index]);
        num_video++;
        all_tdr_time+=connArr[index].conn_tdrs_time/connArr[index].tdr.tdr_id;
        all_size+=connArr[index].total_size;
        num_all_tdr+=connArr[index].tdr.tdr_id;
        all_time_between_two_tdr+=connArr[index].time_between_two_tdr;
    }
   
      strcpy(dataArr[index], " ");
}

double mydiff(double start, double end)
{
    return end-start;
           
}


void video_statistics()
{
    FILE *file;
    file = fopen("videoStat.csv", "w+");
    fprintf(file,"video statistics:\n");
    fprintf(file,"Num videos have been watched:   %d\n",num_video); 
    fprintf(file,"Average duration of the videos: %f\n",(double)all_conn_time/(double)num_video); 
    fprintf(file,"Average size of the videos:     %f\n" ,(double)all_size/(double)num_video);
    fprintf(file,"Average number of TDRs per video: %f\n",(double)num_all_tdr/(double)num_video);
    fprintf(file,"Average size of the TDRs per video: %f\n",(double)all_size/(double)num_all_tdr);
    fprintf(file,"Average duration of the TDRs per video: %f\n",(double)all_tdr_time/(double)num_video);
    fprintf(file,"Average time between two consecutive TDRs: %f\n \n",all_time_between_two_tdr/(double)num_all_tdr);
    
    // printf("Average time between two consecutive TDRs: %f\n",all_time_between_two_tdr/(double)num_all_tdr);

}


