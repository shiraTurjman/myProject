#include <stdlib.h>
#include <stdint.h>

typedef struct
{
    const char *key; // key is NULL if this slot is empty
    void *value;
} ht_entry;

// Hash table structure: create with ht_create, free with ht_destroy.
typedef struct
{
    ht_entry *entries; // hash slots
    size_t capacity;   // size of _entries array
    size_t length;     // number of items in hash table
} ht;

typedef struct
{
    const char *key; // current key
    void *value;     // current value

    // Don't use these fields directly.
    ht *_table;    // reference to hash table being iterated
    size_t _index; // current index into ht._entries
} hti;

ht *ht_create(void);
void ht_destroy(ht *table);
int ht_get(ht *table, const char *key);
const char *ht_set(ht *table, const char *key, void *value);
static const char *ht_set_entry(ht_entry *entries, size_t capacity, const char *key, void *value, size_t *plength);
void ht_reset(ht *table);