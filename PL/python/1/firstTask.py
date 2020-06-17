import sql

#main
ret = sql.sql("select top " + str(input('Количество позиций топа:\t')) + " user.occupation, count(rating.id) from user inner join rating on user.id = rating.userid group by user.occupation order by count(rating.id) desc")
for row in ret:
    print(row)
