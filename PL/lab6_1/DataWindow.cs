using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace lab6_1
{
    /*
     * Ситуация вынуждает лепить костыли:
     *  - Окна таблиц с данными по сути своей имеют одинаковую логику, и , чтобы ее не дублировать, 
     *    резонно вынести общую логику в отдельный класс и подтянуть ее через наследование  [DataWindow<T> : Window]
     *  - Visual Studio не дает унаследовать DataWindow, на этапе сборки изменяя содержимое файлов
     *  - Так что вместо наследования у нас теперь связь через поля. Не очень правильно, зато работает и для редактирования 
     *    общей логики нужно менять в 6 раз меньше кода.
     *  - Profit
    */
    public interface IValue
    {
        object[] Val { get; set; } //Возвращаемое значение
    }
    public interface IAddSomething //Унифицируем окно добавления нового элемента
    {
        object[] Value { get; set; } //Возвращаемое значение
        bool? ShowDialog();
        void Fill(object[] tuple); //Заполнение полей
    }
    public class DataWindow<T> : Window where T : IAddSomething, new()
    {
        static string GetStringData(DataRow row)
        {
            if (MainWindow.loading) return "";
            if (row is DataSet1.CarRow)
                return row.ItemArray[0] + " " + MainWindow.set.Model.FindBycode((int)row.ItemArray[1]).ItemArray[1].ToString() + " (" + row.ItemArray[3] + ")";
            if (row is DataSet1.MasterRow)
                return row.ItemArray[0] + " " + row.ItemArray[2];
            if (row is DataSet1.WorkRow)
                return MainWindow.set.Model.Rows.Find(MainWindow.set.Car.Rows.Find(row.ItemArray[2]).ItemArray[1]).ItemArray[1] + " (" +
                    MainWindow.set.Car.Rows.Find(row.ItemArray[2]).ItemArray[3] + ") " +
                    MainWindow.set.WorkType.Rows.Find(row.ItemArray[0]).ItemArray[1];
            if (row is DataSet1.WorkTypeRow)
                return row.ItemArray[0] + " " + row.ItemArray[1];
            if (row is DataSet1.OwnerRow)
                return row.ItemArray[0] + " " + row.ItemArray[2];
            if (row is DataSet1.ModelRow)
                return row.ItemArray[0] + " " + row.ItemArray[1];
            throw new Exception("Неопознанный тип");
        }
        public DataTable oTable; //Таблица с данным
        public ListBox List;
        Window child;
        public DataWindow(Window child)
        {
            this.child = child;
            child.Closing += closing;
        }

        private void closing(object sender, System.ComponentModel.CancelEventArgs e) //При закрытии "дочернего" окна закрывает текущее
        {
            Close();
        }

        public void List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                (child as IValue).Val = oTable.Rows[List.SelectedIndex].ItemArray; //Если окно открыто в диалоговом режиме - возвращает текущий item
                child.DialogResult = true;
            }
            catch
            {
                editButton_Click(null, null); //Иначе открывает его для редактирования
            }
        }
        public void editButton_Click(object sender, RoutedEventArgs e)
        {
            int index = List.SelectedIndex; //Фиксируем selectedIndex, потому что он может поменяться в процессе
            if (index != -1)
            {
                T adm = new T();
                adm.Fill(oTable.Rows[List.SelectedIndex].ItemArray);
                if ((bool)adm.ShowDialog()) //Если все прошло ОК - добавляем
                    oTable.Rows[List.SelectedIndex].ItemArray = adm.Value;
            }
        }
        public void addButton_Click(object sender, RoutedEventArgs e)
        {
            T adm = new T();
            if ((bool)adm.ShowDialog())
                oTable.Rows.Add(adm.Value);
        }
        //Три перегрузки обновления списка
        public void refresh() // для удобного доступа из кода
        {
            List.Items.Clear();
            foreach (DataRow r in oTable.Rows)
                List.Items.Add(GetStringData(r)); //
        }
        public void refresh(object sender, DataTableNewRowEventArgs e) // для обработки событий
        {
            refresh();
        }
        public void refresh(object sender, DataRowChangeEventArgs e) // для обработки событий
        {
            refresh();
        }
        public void removeButton_Click(object sender, RoutedEventArgs e) //Показывает предупреждение и удаляет из oTable выбранный item
        {
            int index = List.SelectedIndex;
            if (index != -1 && MessageBox.Show("Удаление этого элемента может повлечь удаление связанных с ним записей\nПродолжить?", "Удаление",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                oTable.Rows.RemoveAt(List.SelectedIndex);
        }
    }
}
