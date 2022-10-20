
typedef struct Packet
{   unsigned char *buffer;
    int size;
    char* ip;
} packet;

typedef struct Node
{
    struct packet *pac;
    struct Node *next;
    struct Node *prev;
} node;

typedef struct PacQueue
{
    node *first;
    node *end;
} pacQueue;
