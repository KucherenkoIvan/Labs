using System.Windows;

namespace lab6_1
{
    public partial class Cars : Window, IValue
    {
        public object[] Val { get; set; } // возвращаемое значение, которое определяется в классе DataWindow<>
        DataWindow<AddCar> W; // Объект класса с общей логикой
        public Cars()
        {
            InitializeComponent(); //все как обычно
            Loaded += Cars_Loaded;

            W = new DataWindow<AddCar>(this);

            addButton.Click += W.addButton_Click; //делегируем обработку событий в "общий" класс
            editButton.Click += W.editButton_Click;
            removeButton.Click += W.removeButton_Click;
            list.MouseDoubleClick += W.List_MouseDoubleClick;
        }
        private void Cars_Loaded(object sender, RoutedEventArgs e)
        {
            //Подцепляем таблицу и события DataSet'a, причем именно здесь, потому что значение поля Owner будет задано после создания объекта
            W.oTable = MainWindow.set.Car;
            W.oTable.RowChanged += W.refresh;
            W.oTable.RowDeleted += W.refresh;
            W.oTable.TableNewRow += W.refresh;
            W.List = list;
            W.refresh();
        }
        //private void editButton_Click(object sender, RoutedEventArgs e)
        //{
        //    int index = list.SelectedIndex;
        //    if (index != -1)
        //    {
        //        bool flag = true;
        //        while (flag)
        //        {
        //            AddCar adm = new AddCar();
        //            adm.Owner = owner;
        //            adm.Fill(oTable.Rows[list.SelectedIndex].ItemArray, owner.set);
        //            if ((bool)adm.ShowDialog())
        //                if (adm.Valid)
        //                {
        //                    try
        //                    {
        //                        DataSet1.CarRow row = oTable.ElementAt(list.SelectedIndex);
        //                        row.BeginEdit();
        //                        row[1] = adm.value[1] as DataSet1.ModelRow;
        //                        row[2] = adm.value[2] as DataSet1.OwnerRow;
        //                        row[3] = adm.value[3].ToString();
        //                        row.EndEdit();
        //                        flag = false;
        //                    }
        //                    catch
        //                    {
        //                        MessageBox.Show("Ошибка при редактировании!\nВозможно запись была удалена во время редактирования");
        //                    }
        //                }
        //                else
        //                    MessageBox.Show("Валидация не пройдена");
        //            else flag = false;
        //        }
        //    }
        //}
    }
}
