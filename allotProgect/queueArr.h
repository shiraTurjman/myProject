#include <stdlib.h>
#include <stdint.h>

#define SIZE 1000
typedef struct
{
    int queueA[SIZE];
    int start;
    int end; 
    int lenght;
} queueArr;

int inqueue(queueArr *q,int value);
int dequeue(queueArr *q);
queueArr *qa_create();
void qa_destroy(queueArr *q);
int isOverflow(queueArr *q);
