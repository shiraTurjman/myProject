#include <stdio.h>
#include <sqlite3.h>
#include <string.h>
static int callback(void *data, int argc, char **argv, char **azColName)
{
	int i;
	fprintf(stderr, "%s: ", (const char *)data);
	for (i = 0; i < argc; i++)
	{
		printf("%d:%s = %s\n", i, azColName[i], argv[i] ? argv[i] : "NULL");
	}
	printf("\n");
	return 0;
}
int createTable(sqlite3 *DB, const char *str, char *filename)
{
	int exit = 0;
	exit = sqlite3_open(filename, &DB);
	char *messaggeError;
	exit = sqlite3_exec(DB, str, NULL, 0, &messaggeError);

	if (exit != SQLITE_OK)
	{
		perror("Error Create Table\n");
		sqlite3_free(messaggeError);
		return -1;
	}
	else
		printf("Table created Successfully\n");
	return 1;
}

int insertTable(sqlite3 *DB, const char *toInsert)
{
	char *messaggeError;
	int exit = sqlite3_exec(DB, toInsert, NULL, 0, &messaggeError);
	if (exit != SQLITE_OK)
	{
		perror("Error Insert\n");
		sqlite3_free(messaggeError);
		return -1;
	}
	else
		printf("\nRecords created Successfully!\n");
	return 0;
}

int main()
{
    sqlite3 *DB;
	char *filename = "example.db";
	char *data = "IP TABLE:";
	const char* str ="CREATE TABLE IP("
					// "ID INT PRIMARY KEY	 NOT NULL UNIQUE, "
					"APPROVAL		 INT NOT NULL, "
					"NUM		 TEXT	 NOT NULL UNIQUE);";
	int wasWork = createTable(DB, str, filename);
    if (wasWork < 0)
	{
		perror("the table not created");
		return 1;
	}
	char *messaggeError;
    int exit = sqlite3_open(filename, &DB);
	char* ind="CREATE UNIQUE INDEX UNIQUE_ID ON contacts (NUM);";
    (DB, ind,  NULL, 0, &messaggeError);

    const char *query = "SELECT rowid * FROM IP;";
	printf("\nSTATE OF TABLE BEFORE INSERT\n");
	sqlite3_exec(DB, query, callback, (void *)data, NULL);
    const char *toInsert ="INSERT INTO IP VALUES(1, '225.14.1.51');"
						   "INSERT INTO IP VALUES(0, '88.86.54.50');"
						   "INSERT INTO IP VALUES(1, '197.86.12.1');"
						   "INSERT INTO IP VALUES(0, '225.225.54.50');";
	wasWork = insertTable(DB, toInsert);
    if (wasWork < 0)
	{
		perror("the insert was felled");
		return 1;
	}
	printf("success");

	printf("\nSTATE OF TABLE AFTER INSERT\n");
    sqlite3_exec(DB, query, callback, (void *)data, NULL);
    return 0;
}
