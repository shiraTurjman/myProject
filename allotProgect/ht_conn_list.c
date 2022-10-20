#include "ht_conn_list.h"
#include <assert.h>
#include <stdint.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

// #include "TDR.h"
// #ifndef _CONNSTRUCTֹ
// #include "conn_struct.h"
// #endif


#define FNV_OFFSET 14695981039346656037UL
#define FNV_PRIME 1099511628211UL
#define INITIAL_CAPACITY 2000
#define SIZE_CHAR_CONNKEY 40


ht *ht_create()
{
    // Allocate space for hash table struct.
    ht *table = (ht *)malloc(sizeof(ht));
    if (table == NULL)
    {
        return NULL;
    }
    table->length = 0;
    table->capacity = INITIAL_CAPACITY;

    // Allocate (zero'd) space for entry buckets.
    table->entries = (ht_entry *)calloc(table->capacity, sizeof(ht_entry));
    if (table->entries == NULL)
    {   printf("error creating table\n"); 
        free(table); // error, free table before we return!
        return NULL;
    }
    return table;
}

void ht_destroy(ht *table)
{
   
    //  free entries array andn table itself.
    free(table->entries);
    free(table);
}


// Return 64-bit FNV-1a hash for key (NUL-terminated). See description:
// https://en.wikipedia.org/wiki/Fowler–Noll–Vo_hash_function
static uint64_t hash_key(const char *key)
{
    // uint64_t hash=(uint64_t)malloc(sizeof(uint64_t));
    uint64_t hash = FNV_OFFSET;
    // const char *p=(char *)malloc(sizeof(char *));
    for (const char *p = key; *p; p++)
    {
        hash ^= (uint64_t)(unsigned char)(*p);
        hash *= FNV_PRIME;
    }
    return hash;
}


int ht_get(ht *table,const five_tuple key)
{
    
     char *connection_key = (char *)malloc(sizeof(char) * SIZE_CHAR_CONNKEY);

    sprintf(connection_key,"%d%d%d",key.client_ip,key.server_ip,key.client_port);

    uint64_t hash = hash_key(connection_key);
    free(connection_key);
    size_t index = (size_t)(hash & (uint64_t)(table->capacity - 1));
    // printf("index: %d\n",index);
    // Loop till we find an empty entry.
    while (table->entries[index].value != 0)
    {
        if (compare_tuple(key, table->entries[index].key) == 0)
        {
            // Found key, return value.
            return (int)table->entries[index].value;
        }
        // Key wasn't in this slot, move to next (linear probing).
        index++;
        if (index >= table->capacity)
        {
            // At end of entries array, wrap around.
            index = 0;
        }
    }
    return -1;
}

void ht_delete(ht* table, const five_tuple key)
{
    char *connection_key = (char *)malloc(sizeof(char) * SIZE_CHAR_CONNKEY);
    // connection_key=(char *)&key;
    sprintf(connection_key,"%d%d%d",key.client_ip,key.server_ip,key.client_port);
    

    uint64_t hash = hash_key(connection_key);
    size_t index = (size_t)(hash & (uint64_t)(table->capacity - 1));
    free(connection_key);
    

    table->entries[index].value = 0;
    // table->entries[index].key = NULL;
    table->length=table->length-1;

}
// Internal function to set an entry (without expanding table).
static int ht_set_entry(ht_entry *entries, size_t capacity,five_tuple key,  int value, size_t *plength)
{
   
    char *connection_key = (char *)malloc(sizeof(char) * SIZE_CHAR_CONNKEY);
    // connection_key=(char *)&key;
    sprintf(connection_key,"%d%d%d",key.client_ip,key.server_ip,key.client_port);
    
    uint64_t hash = hash_key(connection_key);
    
    size_t index = (size_t)(hash & (uint64_t)(capacity - 1));
    free(connection_key);
    
    // Loop till we find an empty entry.
    while (entries[index].value != 0)
    {

        // Key wasn't in this slot, move to next (linear probing).
        index++;
        if (index >= capacity)
        {
            // At end of entries array, wrap around.
            index = 0;
        }
    }
     
    // Didn't find key, allocate+copy if needed, then insert it.
    if (plength != NULL)
    {
        
        (*plength)++;
    }
    entries[index].key = key;
    
    entries[index].value = value;
   
    return 0;
}

int ht_set(ht *table,const five_tuple key,int value)
{
    
    assert(value >=0 && value <= table->capacity);
    if (value == NULL)
    {
        printf("The index(value) is invalid");
        return -1;
    }

    // If length will exceed half of current capacity, expand it.
    if (table->length >= table->capacity / 2)
    {
        printf("can't add connection... there are already 1000 connection");
        // return NULL;
        return -1;
    }

    // Set entry and update length.
    return ht_set_entry(table->entries, table->capacity, key, value,
                        &table->length);
}

/////////////////////////////////////////////
//--------------------TDR------------------

int compare_tuple(five_tuple t1, five_tuple t2)
{
    if(t1.client_ip==t2.client_ip&&t1.client_port==t2.client_port&&t1.server_ip==t2.server_ip&&t1.server_port==t2.server_port&&t1.l4==t2.l4)
    {return 0;}
    return -1;
}

//////////////LINKED LIST////////////////////
node *node_create(int i)
{

    node *newnode =(node *)malloc(sizeof(node));
    newnode->next = NULL;
    newnode->prev = NULL;
    newnode->value = i;
    return newnode;
}
linked_list *list_create()
{
    linked_list *list =(linked_list *)malloc(sizeof(linked_list));
    
    list->first=NULL;
    list->last=NULL;
    return list;
}

void add_node(linked_list *list,node *n)
{
    if(list->last==NULL)
    {
        //the list empty
        list->last = n;
        list->first = n;
        return;
    }
    
        n->prev = list->last;
        list->last->next = n;
        n->next = NULL;
        list->last = n;
    

}
void move_node(linked_list *list,node *n)
{
    if(n->prev==NULL&&n->next==NULL)
    {
        add_node(list, n);
        return;
    }
    if(list->first == list->last )
    {return;}
    if(list->first == n)
    {
        list->first = n->next;
        n->next->prev=NULL;
        n->next = NULL;
        list->last->next = n;
        n->prev=list->last;
        list->last = n;
        return;
    }
    if(list->last == n)
    {
        return;
    }
    
    n->prev->next= n->next;
    n->next->prev = n->prev;
    add_node(list,n);

}

void remove_node(linked_list *list,node *n)
{
    if(list->first==list->last)
    {
        list->first=list->last= NULL;
    }
    else{
        if(list->first == n)
        {
            list->first = n->next;
            n->next->prev=NULL;
        
        }
        else{
            if(list->last == n)
            {
                list->last = n->prev;
                n->prev->next = NULL;
            }
            else
            {
                n->prev->next= n->next;
                n->next->prev = n->prev;
            }
        }
    }
    n->prev=NULL;
    n->next=NULL;
}



