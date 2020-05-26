using System;
class SimpleSorting
{
    /// <summary>
    /// Инициализирует массив и заполняет его случайными числами в диапазоне [0..max]
    /// </summary>
    /// <param name="array">Заполняемый массив</param>
    /// <param name="len">Требуемая длина массива</param>
    /// <param name="max">Максимальное допустимое значение. Если <= 0, за max берется int.MaxValue (2147483647)</param>
    public static void MakeData(ref int[] array, int len, int max = 0)
    {
        array = new int[len];
        Random r = new Random();
        for (int i = 0; i < len; i++)
            array[i] = max > 0 ? r.Next(0, max) : r.Next(0, int.MaxValue);
    }
    /// <summary>
    /// Обменивает значениями a и b
    /// </summary>
    /// <param name="a">Аргумент 1</param>
    /// <param name="b">Аргумент 2</param>
    public static void Swap(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }
    /// <summary>
    /// Пузырьковая сортировка
    /// </summary>
    /// <param name="array">Массив для сортировки</param>
    public static void Bubble(ref int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
            for (int j = i; j < array.Length - 1; j++)
                if (array[j] > array[j + 1])
                    Swap(ref array[j], ref array[j + 1]);
    }
    /// <summary>
    /// Шейкер-сортировка
    /// </summary>
    /// <param name="array">Массив для сортировки</param>
    public static void Shaker(ref int[] array)
    {
        int left = 0, right = array.Length - 1;

        while (left < right) // пока левая и правая границы не "сошлись"
        {
            for (int i = left; i < right; i++) // перебираем массив пузырьком слева направо
                if (array[i] > array[i + 1])
                    Swap(ref array[i], ref array[i + 1]);
            right--;

            for (int i = right; i > left; i--) // перебираем массив пузырьком справа налево
                if (array[i - 1] > array[i])
                    Swap(ref array[i - 1], ref array[i]);
            left++;
        }
    }
    /// <summary>
    /// Cортировка выбором
    /// </summary>
    /// <param name="array">Массив для сортировки</param>
    public static void Selection(ref int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            int min = i; // индекс минимального элемента
            for (int j = i + 1; j < array.Length; j++)
                if (array[j] < array[min])
                    min = j; // если найден новый минимум, записываем индекс
            Swap(ref array[i], ref array[min]); // обмен
        }

    }
    /// <summary>
    /// Сортировка вставками
    /// </summary>
    /// <param name="array">Массив для сортировки</param>
    public static void Insertion(ref int[] array)
    {
        for (int i = 1; i < array.Length; i++)
            for (int j = i; j > 0 && array[j - 1] > array[j]; j--) // пока j>0 и элемент j-1 > j, x-массив int
                Swap(ref array[j - 1], ref array[j]);
    }
    /// <summary>
    /// Сортировка бинарными вставками
    /// </summary>
    /// <param name="array">Массив для сортировки</param>
    public static void BinaryInsertion(ref int[] array)
    {
        for (int i = 0; i < array.Length; i++) // перебираем элементы
        {
            int low = 0, high = i - 1, temp = array[i];
            while (low < high) //бинарный поиск места для вставки
            {
                int mid = low + ((high - low) / 2);
                if (array[i] < array[mid])
                    high = mid;
                else low = mid + 1;
            }
            for (int j = i; j > low + 1; j--) //смещение элементов массива, для освобождения места для вставки
                array[j] = array[j - 1];
            array[low] = temp;
        }
    }
    public static void Shell(ref int[] array)
    {
        for (int inc = array.Length / 2; inc >= 1; inc = inc / 2)
            for (int step = 0; step < inc; step++)
                ParticularInsertionSort(array, step, inc);
    }

    private static void ParticularInsertionSort(int[] arr, int start, int inc) // Частичная сортировка вставками для сортировки Шелла
    {
        int tmp;
        for (int i = start; i < arr.Length - 1; i += inc)
            for (int j = Math.Min(i + inc, arr.Length - 1); j - inc >= 0; j = j - inc)
                if (arr[j - inc] > arr[j])
                {
                    tmp = arr[j];
                    arr[j] = arr[j - inc];
                    arr[j - inc] = tmp;
                }
                else break;
    }
}
