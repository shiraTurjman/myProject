CC := gcc

LDFLAGS := -lpcap -g -ljson-c

run: project.o ht_conn_list.o queueArr.o 
	$(CC) -o run project.o ht_conn_list.o queueArr.o $(LDFLAGS)

project.o: project.c
	$(CC) -c project.c $(LDFLAGS)
ht_conn_list.o: ht_conn_list.c ht_conn_list.h
	$(CC) -c ht_conn_list.c $(LDFLAGS)
# conn_struct.o:conn_struct.c conn_struct.h
# 	$(CC)  -c conn_struct.c $(LDFLAGS)
queueArr.o: queueArr.c queueArr.h
	$(CC) -c queueArr.c $(LDFLAGS)
# linked_list.o: linked_list.c linked_list.h
# 	$(CC) -c linked_list.c $(LDFLAGS)

clean:
	rm -f *.o 
	
