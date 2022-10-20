#include "hashtable.h"
#include <assert.h>
#include <stdint.h>
#include <stdlib.h>
#include <string.h>
#include <pthread.h>
#define FNV_OFFSET 14695981039346656037UL
#define FNV_PRIME 1099511628211UL
#define INITIAL_CAPACITY 100
pthread_mutex_t hashMutex = PTHREAD_MUTEX_INITIALIZER; // mutex for hashtab

ht *ht_create(void)
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
    {
        free(table); // error, free table before we return!
        return NULL;
    }
    return table;
}

void ht_destroy(ht *table)
{
    // First free allocated keys.
    for (size_t i = 0; i < table->capacity; i++)
    {
        free((void *)table->entries[i].key);
    }

    // Then free entries array and table itself.
    free(table->entries);
    free(table);
}

void ht_reset(ht *table)
{
    //# if the hash is empty-didnt need to run on it
    pthread_mutex_lock(&hashMutex);
    if (table->length == 0)
    {
        pthread_mutex_unlock(&hashMutex);
        return;
    }
    for (size_t i = 0; i < table->capacity; i++)
    {

        if (table->entries[i].key == NULL)
            continue;
        table->entries[i].key = NULL;
        table->entries[i].value = 0;
    }

    table->length = 0;

    pthread_mutex_unlock(&hashMutex);
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

int ht_get(ht *table, const char *key)
{
    // AND hash with capacity-1 to ensure it's within entries array.
    uint64_t hash = hash_key(key);
    size_t index = (size_t)(hash & (uint64_t)(table->capacity - 1));

    // Loop till we find an empty entry.
    while (table->entries[index].key != NULL)
    {
        if (strcmp(key, table->entries[index].key) == 0)
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
    return 0;
}

// Internal function to set an entry (without expanding table).
static const char *ht_set_entry(ht_entry *entries, size_t capacity, const char *key, void *value, size_t *plength)
{
    // AND hash with capacity-1 to ensure it's within entries array.
    // printf("in ht set entry\n");
    uint64_t hash = hash_key(key);
    // printf("hash\n");
    size_t index = (size_t)(hash & (uint64_t)(capacity - 1));
    // printf("index\n");
    pthread_mutex_lock(&hashMutex);
    // printf("lock a mutex\n");
    // Loop till we find an empty entry.
    while (entries[index].key != NULL)
    {

        if (strcmp(key, entries[index].key) == 0)
        {
            // Found key (it already exists), update value.
            entries[index].value = value;

            pthread_mutex_unlock(&hashMutex);

            return entries[index].key;
        }
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
        key = strdup(key);
        if (key == NULL)
        {
            pthread_mutex_unlock(&hashMutex);

            return NULL;
        }
        (*plength)++;
    }
    entries[index].key = (char *)key;
    // printf("entries[index].key : %s\n",entries[index].key);
    printf("value: %d\n",(int)value);
    // printf("entries[index].value : %s\n",entries[index].value);
    entries[index].value = value;
   
    // printf("entries[index].value : %s\n",entries[index].value);
   
    pthread_mutex_unlock(&hashMutex);
    // printf("unlock mutex\n");
    return key;
}
// Expand hash table to twice its current size. Return true on success,
// false if out of memory.
static int ht_expand(ht *table)
{
    // Allocate new entries array.
    size_t new_capacity = table->capacity * 2;
    if (new_capacity < table->capacity)
    {
        return 0; // overflow (capacity would be too big)
    }
    ht_entry *new_entries = (ht_entry *)calloc(new_capacity, sizeof(ht_entry));
    if (new_entries == NULL)
    {
        return 0;
    }

    // Iterate entries, move all non-empty ones to new table's entries.
    for (size_t i = 0; i < table->capacity; i++)
    {
        ht_entry entry = table->entries[i];
        if (entry.key != NULL)
        {
            ht_set_entry(new_entries, new_capacity, entry.key,
                         entry.value, NULL);
        }
    }

    // Free old entries array and update this table's details.
    free(table->entries);
    table->entries = new_entries;
    table->capacity = new_capacity;
    return 1;
}

const char *ht_set(ht *table, const char *key, void *value)
{
    // printf("in ht set\n");
    assert(value != NULL);
    if (value == NULL)
    {
        return NULL;
    }

    // If length will exceed half of current capacity, expand it.
    if (table->length >= table->capacity / 2)
    {
        if (!ht_expand(table))
        {
            return NULL;
        }
    }

    // Set entry and update length.
    return ht_set_entry(table->entries, table->capacity, key, value,
                        &table->length);
}

size_t ht_length(ht *table)
{
    return table->length;
}

hti ht_iterator(ht *table)
{
    hti it;
    it._table = table;
    it._index = 0;
    return it;
}

int ht_next(hti *it)
{
    // Loop till we've hit end of entries array.
    ht *table = it->_table;
    while (it->_index < table->capacity)
    {
        size_t i = it->_index;
        it->_index++;
        if (table->entries[i].key != NULL)
        {
            // Found next non-empty item, update iterator key and value.
            ht_entry entry = table->entries[i];
            it->key = entry.key;
            it->value = entry.value;
            return 1;
        }
    }
    return 0;
}