#include <stdint.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include "queueArr.h"

#define SIZE 1000

queueArr *qa_create()
{
    queueArr *q=(queueArr *)malloc(sizeof(queueArr));
    if(q==NULL)
    {
        printf("error creating queueArr\n");
        return NULL;
    }

    q->start=0;
    q->end=SIZE-1;
    q->lenght=SIZE;
    for(int i=0; i<SIZE; i++)
    {
        q->queueA[i]=i+1;
    }
    return q;
}
void qa_destroy(queueArr *q)
{
    free(q);
}

int inqueue(queueArr *q,int value)
{
//    printf("in lenght %d\n",q->lenght);
    if(q->lenght>SIZE)
    {   printf("in lenght %d\n",q->lenght);
        printf("queue Overflow\n");
        return -1;
    }
    q->lenght=q->lenght+1;
    q->end=(q->end+1) % SIZE;
    q->queueA[q->end]=value;
    return value;
}

int dequeue(queueArr *q)
{
    
    if(q->lenght==0)
    {   
        printf("queue empty\n");
        return -1;
    }
    q->lenght=q->lenght-1;
    // printf("de lenght %d\n",q->lenght);
    int value= q->queueA[q->start];
    q->start = (q->start+1) %SIZE ;
    return  value;
}

int isOverflow(queueArr *q)
{
    if(q->lenght>SIZE)
        return 1;
    return 0;
}