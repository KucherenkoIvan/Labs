namespace System.Collections.Extended
{
    //Соблюдая принцип единой ответственности разделим класс на 2 реализующих рразные задачи
    class PriorityQueue<T> //методы работы непосредственно с очередью
    {
        class QueueElement<T> //хранение и связь данных
        {
            public QueueElement<T> Next; //ссылка на следующий элемент
            public T Value; //хранимое значение
            public int Priority; //приоритет-ключ
            public QueueElement(T val, int priority) //одного конструктора будет достаточно
            {
                Value = val;
                Priority = priority;
            }
            public override string ToString() //возвращает пару (значение, ключ)
            {
                return $"({Value.ToString()}, {Priority})";
            }
        }
        QueueElement<T> first, last; //ссылки на начало и конец очереди
        int count = 0; //количество элементов
        /// <summary>
        /// Возвращает количество элементов в очереди
        /// </summary>
        public int Count 
        {
            get => count;
            //прямой рассчет занял бы O(N), так что бережно храним значение и верим в его истинность
        }
        /// <summary>
        /// Создает пустую очередь
        /// </summary>
        public PriorityQueue()
        {
        }
        /// <summary>
        /// Создает очередь и добавляет в нее элемент с приоритетом 0
        /// </summary>
        /// <param name="val">Значение</param>
        public PriorityQueue(T val)
        {
            last = first = new QueueElement<T>(val, 0);
        }
        /// <summary>
        /// Создает очередь и добавляет в нее элемент с заданным приоритетом
        /// </summary>
        /// <param name="val">Значение</param>
        /// <param name="priority">Приоритет</param>
        public PriorityQueue(T val, int priority)
        {
            last = first = new QueueElement<T>(val, priority);
        }
        /// <summary>
        /// Возвращает элемент с заданным индексом
        /// Если такого нет возвращает соответствующее исключение
        /// </summary>
        /// <param name="index">Индекс элемента</param>
        /// <returns></returns>
        public T this[int index] // Сложность - O(N)
        {
            get 
            {
                if (index < 0) //Проверка на отрицательный индекс
                    throw new IndexOutOfRangeException();
                if (first == null) //Проверка на наличие в очереди объектов
                    throw new Exception("Очередь пуста");
                QueueElement<T> ret = first;
                int i;
                for (i = 0; i < index && ret.Next != null; i++) //пытаемся i раз сместиться по очереди
                    ret = ret.Next;
                if (i != index) //если не получилось - исключение
                    throw new IndexOutOfRangeException();
                return ret.Value; //иначе возвращаем значение
            }
            set 
            {
                if (index < 0)
                    throw new IndexOutOfRangeException(); //отбрасываем отрицательные индексы
                if (first == null)
                    throw new Exception("Очередь пуста"); //и выходим если очередь пуста
                QueueElement<T> ret = first;
                int i;
                for (i = 0; i < index && ret.Next != null; i++) //смещение на i
                    ret = ret.Next;
                if (i != index) //не получилось - исключение
                    throw new IndexOutOfRangeException();
                ret.Value = value;//возвращаем значение
            }
        }
        /// <summary>
        /// Добавляет элемент с приоритетом 0 в конец очереди
        /// </summary>
        /// <param name="val">Значение</param>
        public void Enqueue(T val) //Сложность - O(1)
        {
            if (last == null) //если очередь пуста создаем первый элемент
            {
                first = last = new QueueElement<T>(val, 0);
                return;
            }
            last.Next = new QueueElement<T>(val, 0); //Иначе цепляем новый элемент в конец очереди
            last = last.Next; //
            count++;
        }
        /// <summary>
        /// Добавляет элемент с приоритетом 0 в указанную позицию
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="index">Позиция</param>
        public void Insert(T value, int index) //Временная сложность O(N)
        {
            if (index < 0)
                throw new IndexOutOfRangeException(); //проверка на отрицательный индекс
            if (first == null)
                throw new Exception("Очередь пуста");//и на пустую очередь тоже проверка
            QueueElement<T> ret = first;
            int i;
            for (i = 0; i < index - 1 && ret.Next != null; i++) //смещаемся на i-1 позиций
                ret = ret.Next;
            if (i != index - 1) //если не получилось - исключение
                throw new IndexOutOfRangeException();
            QueueElement<T> element = new QueueElement<T>(value, 0);
            element.Next = ret.Next;//цепляем "хвост" к новому элементу
            ret.Next = element;     //а новый элемент к i-1
            count++;
        }
        /// <summary>
        /// Добавляет элемент с заданным приоритетом в соответствующую позицию
        /// </summary>
        /// <param name="val">Значение</param>
        /// <param name="priority">Приоритет</param>
        public void Enqueue(T val, int priority)//Временная сложность - O(N)
        {
            QueueElement<T> element = new QueueElement<T>(val, priority);
            if (last == null)//При пустой очереди создаем первый элемент
            {
                first = last = element;
                count++;
                return;
            }
            if (element.Priority > first.Priority) //Частный случай: Приоритет элемента больше уже имеющихся
            {
                element.Next = first;//цепляем очередь к новому элементу
                first = element;//переназначаем ссылку
                count++;
                return;
            }
            if (element.Priority <= last.Priority) //Частный случай: Приоритет элемента меньше уже имеющихся
            {
                last.Next = element;//Цепляем новый элемент к очереди
                last = element;//Переназначаем ссылку
                count++;
                return;
            }
            QueueElement<T> prev = first;
            while (prev.Priority >= element.Priority && prev.Next != null) //Смещаемся пока не найдем подходящее место для вставки
                prev = prev.Next;
            element.Next = prev.Next;//вставляем
            prev.Next = element;
            count++;
        }
        /// <summary>
        /// Вынимает первый элемент из очереди
        /// </summary>
        /// <returns>Значение</returns>
        public T Dequeue() //Временная сложность - O(1)
        {
            if (first == null) //проверка на пустую очередь
                throw new Exception("Очередь пуста");
            else
            {
                T ret = first.Value; //сохраняем значение для возврата
                first = first.Next; //сдвигаем ссылку
                count--;
                return ret;
            }
        }
        /// <summary>
        /// Вынимает из очереди первый элемент с заданным приоритетом
        /// </summary>
        /// <param name="key">Приоритет</param>
        /// <returns>Значение</returns>
        public T Dequeue(int key) //Временная сложность - O(N)
        {
            if (first == null) //проверка на пустую очередь
                throw new Exception("Очередь пуста");
            QueueElement<T> t = first, prev = null;
            if (first.Priority == key) //частный случай - первый же элемент подошел
                return Dequeue();
            while (t.Next != null) //сдвигаем, пока не найдем подходящий ключ
            {
                if (t.Priority == key)
                {
                    count--;
                    if (t == last) //если подошел последний элемент - правим ссылки
                    {
                        last = prev;
                        return t.Value;
                    }
                    prev.Next = t.Next; //иначе вычленяем текущий элемент и возвращаем его
                    return t.Value;
                }
                prev = t;
                t = t.Next;
            }
            throw new Exception("Не найдено элементов с заданным приоритетом"); //если ничего не подошло
        }
        /// <summary>
        /// Возвращает первый элемент очереди, но не удаляет его
        /// </summary>
        /// <returns>Значение</returns>
        public T Peek() //O(1)
        {
            if (first.Value == null)//проверка на пустую очередь
                throw new Exception("Очередь пуста");
            return first.Value; //возвращаем ровно то, что написано выше
        }
        /// <summary>
        /// Возвращает первый элемент с заданным приоритетом
        /// </summary>
        /// <param name="priority">Приоритет</param>
        /// <returns>Значение</returns>
        public T Peek(int priority) //O(N)
        {
            if (first == null) //Бан за пустую очередь
                throw new Exception("Очередь пуста");
            QueueElement<T> t = first;
            if (first.Priority == priority) //проверка первого элемента
                return first.Value;
            while (t.Next != null) //движемся по элементам
            {
                if (t.Priority == priority)
                    return t.Value; //возвращаем подходящий
                t = t.Next;
            }
            throw new Exception("Не найдено элементов с заданным приоритетом"); //иначе - кидаемся исключением
        }
        /// <summary>
        /// Очищает очередь и вызывает сборку мусора
        /// </summary>
        public void Clear() // O(1)
        {
            first = last = null; //сбрасываем ссылки
            count = 0;
            GC.Collect(); //дальше сборщик мусора путь сам разбирается
            //Замечание:
            //  При попытке реализовать метод Dispose() интерфейса IDisposable
            //  документация предписывает сделать ровно то же, что сделано и в этом методе:
            //  сбросить все ссылки и предоставить дальнейшую работу сборщику мусора
        }
        /// <summary>
        /// Возвращает строковое представление очереди
        /// </summary>
        /// <returns></returns>
        public override string ToString() // O(N)
        {
            string ret = "";
            QueueElement<T> t = first;
            while (t != null) //перебираем всю очередь и склеиваем строковые представления ее элементов
            {
                ret += t.ToString() + "\n";
                t = t.Next;
            }
            return ret;
        }
    }
}