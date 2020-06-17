import os
import pyodbc as database

#sql-запрос в бд
def sql(S):
    connectionString = (r"DRIVER={Microsoft Access Driver (*.mdb, *.accdb)};"
        r"DBQ=" + os.path.realpath('data/database.mdb') + ";")
    connection = database.connect(connectionString)
    cursor = connection.cursor()
    try:
        cursor.execute(str(S))
        return cursor.fetchall()
    except:
        return 'exception!'