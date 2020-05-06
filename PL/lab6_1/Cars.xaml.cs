using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace lab6_1
{
    public partial class Cars : Window
    {
        MainWindow owner; //Родительское окно
        DataSet1.CarDataTable oTable; //Рабочая таблица
        public object[] Val = null; //возвращаемая строка в виде ItemArray
        public Cars()
        {
            InitializeComponent();
            Loaded += Cars_Loaded;
            list.MouseDoubleClick += List_MouseDoubleClick; ;
        }
        private void List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (list.SelectedIndex != -1)
            {
                try //Если окно было открыто в модальном режиме
                {
                    Val = owner.set.Car.Rows[list.SelectedIndex].ItemArray;
                    DialogResult = true; //Вернем выбранную строку
                }
                catch
                {
                    // Открыть редактирование выбранной строки
                    editButton_Click(null, null);
                }
            }
        }
        //три перегрузки метода обновления
        private void refresh()
        {
            refresh(null, (DataRowChangeEventArgs)null);
        }
        private void refresh(object sender, DataTableNewRowEventArgs e)
        {
            refresh(null, (DataRowChangeEventArgs)null);
        }
        private void refresh(object sender, DataRowChangeEventArgs e)
        {
            list.Items.Clear();
            foreach (DataRow r in oTable.Rows)
                list.Items.Add(r.ItemArray[0] + " " + owner.set.Model.Rows.Find(r.ItemArray[1]).ItemArray[1].ToString() + " (" + r.ItemArray[3] + ")");
        }
        private void Cars_Loaded(object sender, RoutedEventArgs e)
        {
            owner = this.Owner as MainWindow;
            oTable = owner.set.Car;
            owner.set.Car.RowChanged += refresh;
            owner.set.Car.RowDeleted += refresh;
            owner.set.Car.TableNewRow += refresh;
            refresh();
        }
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            int index = list.SelectedIndex;
            if (index != -1 && MessageBox.Show("Удаление этого элемента может повлечь удаление связанных с ним записей\nПродолжить?", "Удаление", //предупреждение
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                oTable.Rows.RemoveAt(list.SelectedIndex);
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            while (flag)
            {
                AddCar adm = new AddCar();
                adm.Owner = owner;
                if ((bool)adm.ShowDialog())
                    if (adm.Valid)
                    {
                        oTable.AddCarRow((adm.value[1] as DataSet1.ModelRow),
                            (adm.value[2] as DataSet1.OwnerRow), adm.value[3].ToString());
                        flag = false;
                    }
                    else
                        MessageBox.Show("Валидация не пройдена");
                else flag = false;
            }
        }
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            int index = list.SelectedIndex;
            if (index != -1)
            {
                bool flag = true;
                while (flag)
                {
                    AddCar adm = new AddCar();
                    adm.Owner = owner;
                    adm.Fill(oTable.Rows[list.SelectedIndex].ItemArray, owner.set);
                    if ((bool)adm.ShowDialog())
                        if (adm.Valid)
                        {
                            try
                            {
                                DataSet1.CarRow row = oTable.ElementAt(list.SelectedIndex);
                                row.BeginEdit();
                                row[1] = adm.value[1] as DataSet1.ModelRow;
                                row[2] = adm.value[2] as DataSet1.OwnerRow;
                                row[3] = adm.value[3].ToString();
                                row.EndEdit();
                                flag = false;
                            }
                            catch
                            {
                                MessageBox.Show("Ошибка при редактировании!\nВозможно запись была удалена во время редактирования");
                            }
                        }
                        else
                            MessageBox.Show("Валидация не пройдена");
                    else flag = false;
                }
            }
        }
    }
}
