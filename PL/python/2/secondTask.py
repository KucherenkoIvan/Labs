import matplotlib.pyplot as plt
import sql

#визуализация dict
def graphics(arr: dict):
    fig, ax = plt.subplots()
    ax.bar(arr.keys(), arr.values())
    ax.set_facecolor('seashell')
    fig.set_facecolor('floralwhite')
    fig.set_figwidth(1200)    #  ширина Figure
    fig.set_figheight(6)    #  высота Figure
    plt.show()
#парсер
def parse(s:str):
    s = s.replace('(', '')
    s = s.replace(')', '')
    s = s.replace('\'', '')
    s = s.replace(' ', '')

    temp = s.split(',')
    return {temp[0]: int(temp[1])}
age_groups = {1: 'Under 18', 18: '18-24', 25: '25-34', 35: '35-44', 45: '45-49', 50: '50-55', 56: '56+'}
while True:
    K = int(input('Желаемая возрастная группа:\t'))
    if K in age_groups:
        break
    else: print('Недопустимое значение возрастной группы')
ret = sql.sql("select distinct top 10 movie.genres, count(rating.id) from ((movie inner join rating on movie.id = rating.movieid) inner join user on rating.userid = user.id) where user.age = '" + str(age_groups[K]) + "' group by movie.genres order by count(rating.id) desc")
d = {}
for row in ret:
    r =  parse(str(row))
    d.update(r)
graphics(d)