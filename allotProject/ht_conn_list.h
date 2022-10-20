#include <stdlib.h>
#include <stdint.h>
// #include "TDR.h"
#include <time.h>



/////////////LINKED LIST////////////////

typedef struct Node
{
    int value;
    struct Node *next;
    struct Node *prev;
}node;

typedef struct 
{
    node *first;
    node *last;
}linked_list;

node *node_create(int i);
linked_list *list_create();
void add_node(linked_list *list,node *n);
void move_node(linked_list *list,node *n);
void remove_node(linked_list *list,node *n);
///////////////////////////////////////////////
//-------------TDR-------------------------

typedef struct
{
    int client_ip;
    int server_ip;
    uint16_t client_port;
    uint16_t server_port;
    uint8_t l4;//Protocol
} five_tuple;

typedef struct
{
    
    int tdr_id;
    double start_time;
    int num_inbound_packet_in_range;
    int num_outbound_packet_in_range;
    int max_packet_size_inbound;
    int min_packet_size_inbound;
    double last_time_inbound;
    double max_diff_time_inbound;
    double min_diff_time_inbound;
    float sumSquareInboundPacketTimeDiff;
    double RTT; 
} TDR;

typedef struct
{
    int total_size;
    double last_packet_time;
    TDR tdr;
    int conn_id;
    five_tuple tuple;
    double start_time;
    double conn_tdrs_time;
    double time_between_two_tdr;
    node *time_node;
} connection;

int compare_tuple(five_tuple t1, five_tuple t2);


///////////////////////////////////////////////
//-------------HASH-------------------------
typedef struct
{
    five_tuple key; // key is NULL if this slot is empty
    int value;
} ht_entry;

// Hash table structure: create with ht_create, free with ht_destroy.
typedef struct
{
    ht_entry *entries; // hash slots
    size_t capacity;   // size of _entries array
    size_t length;     // number of items in hash table
} ht;


ht *ht_create();
void ht_destroy(ht *table);
int ht_get(ht *table, const five_tuple key);
int ht_set(ht *table, const five_tuple key, int value);
static int ht_set_entry(ht_entry *entries, size_t capacity, five_tuple key, int value, size_t *plength);
void ht_reset(ht *table);
void ht_delete(ht* table, const five_tuple key);


