import os
import pyodbc as database

import numpy as np
import matplotlib.pyplot as plt

def graphics(K):
    x = np.arange(1, 8)
    y = np.random.randint(1, 20, size = 7)

    fig, ax = plt.subplots()

    ax.bar(x, y)

    ax.set_facecolor('seashell')
    fig.set_facecolor('floralwhite')
    fig.set_figwidth(12)    #  ширина Figure
    fig.set_figheight(6)    #  высота Figure

    plt.show()

def sql(N):
    connectionString = (r"DRIVER={Microsoft Access Driver (*.mdb, *.accdb)};"
        r"DBQ=" + os.path.realpath('data/database.mdb') + ";")
    connection = database.connect(connectionString)
    cursor = connection.cursor()
    s = "select top " + str(N) + " user.occupation, count(rating.id) from user inner join rating on user.id = rating.userid group by user.occupation order by count(rating.id) desc"    
    try:
        cursor.execute(s)
        for row in cursor.fetchall():
            print(str(row))
    except:
        print('exception!')



graphics(0)
#select distinct top 5 user.id from user inner join rating on rating.userid = user.id where user.gender = 'm' and rating.rate = 3 order by user.id desc