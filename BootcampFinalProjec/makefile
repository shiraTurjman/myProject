CC := gcc

ifeq ($(DEBUG),1)
	CFLAGS := -Wall -O0 -g
else
	# CFLAGS := -Wall -O1
	CFLAGS := -O0
endif

LDFLAGS := -l sqlite3 -pthread  

# -fsanitize=address -Wall

run: write_to_log_in_end.o hashtable.o 
	$(CC) -o run write_to_log_in_end.o  hashtable.o $(LDFLAGS)
	
write_to_log_in_end.o: write_to_log_in_end.c
	$(CC) $(CFLAGS) -c write_to_log_in_end.c $(LDFLAGS)
hashtable.o: hashtable.c hashtable.h
	$(CC) $(CFLAGS) -c hashtable.c $(LDFLAGS) 
# db_function.o:db_function.c db_function.h
# 	$(CC) $(CFLAGS) -c db_function.c $(LDFLAGS)
# list.o: list.c list.h
# 	$(CC) $(CFLAGS) -c list.c $(LDFLAGS)
clean:
	rm  -f *.o run log.txt
	
