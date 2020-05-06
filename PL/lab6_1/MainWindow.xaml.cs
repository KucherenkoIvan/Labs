using Microsoft.Win32;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace lab6_1
{
    public partial class MainWindow : Window
    {
        public DataSet1 set = new DataSet1();
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            list.MouseDoubleClick += List_MouseDoubleClick; ;
        }

        private void List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (list.SelectedIndex != -1) //Если клик был по записи - открыть для редактирования
            {
                editButton_Click(null, null);
            }
            else refresh(); //иначе - обновить список
        }


        //три перегрузки метода для обновления списка
        void refresh() //для обращения из кода
        {
            refresh(null, (DataRowChangeEventArgs)null);
        }
        private void refresh(object sender, DataTableNewRowEventArgs e) //для подписки на события таблиц
        {
            refresh(null, (DataRowChangeEventArgs)null);
        }

        private void refresh(object sender, DataRowChangeEventArgs e) //для подписки на события строк
        {
            try
            {
                list.Items.Clear();
                foreach (DataRow r in set.Work.Rows)
                    list.Items.Add(set.Model.Rows.Find(set.Car.Rows.Find(r.ItemArray[2]).ItemArray[1]).ItemArray[1] + " (" +
                    set.Car.Rows.Find(r.ItemArray[2]).ItemArray[3] + ") " +
                    set.WorkType.Rows.Find(r.ItemArray[0]).ItemArray[1]);
            }
            catch { }
        }
        //подписка на события
        void setEvents()
        {
            set.Work.RowChanged += refresh;
            set.Work.RowDeleted += refresh;
            set.Work.TableNewRow += refresh;

            //Если перестанет работать - раскомментить
            /*
            set.Car.RowChanged += refresh;
            set.Car.RowDeleted += refresh;
            set.Car.TableNewRow += refresh;

            set.WorkType.RowChanged += refresh;
            set.WorkType.RowDeleted += refresh;
            set.WorkType.TableNewRow += refresh;

            set.Model.RowChanged += refresh;
            set.Model.RowDeleted += refresh;
            set.Model.TableNewRow += refresh;
            */
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            setEvents();
            refresh(null, (DataRowChangeEventArgs)null);
        }
        //Удаление
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            int index = list.SelectedIndex;
            if (index != -1)
                set.Work.RemoveWorkRow(set.Work.ElementAt(index));
        }
        //добавление
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                AddWork adm = new AddWork();
                adm.Owner = this;
                if ((bool)adm.ShowDialog())
                    if (adm.Valid)
                    {
                        //конструктор строки
                        set.Work.AddWorkRow((adm.value[0] as DataSet1.WorkTypeRow),
                            adm.value[1] as DataSet1.MasterRow,
                            adm.value[2] as DataSet1.CarRow,
                            DateTime.Parse(adm.value[3].ToString()),
                            DateTime.Parse(adm.value[4].ToString()),
                            adm.value[5].ToString()); //Ну оооооочень длинный конструктор
                        break;
                    }
                    else
                        MessageBox.Show("Валидация не пройдена");
                else break;
            }
        }
        //редактирование
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            int index = list.SelectedIndex;
            if (index != -1)
            {
                DataSet1.WorkRow row = set.Work.ElementAt(index);
                while (true)
                {
                    AddWork adm = new AddWork();
                    adm.Owner = this;
                    adm.Fill(set.Work.Rows[index].ItemArray, set); //Создаем окно редактирование и заполняем его данными

                    if ((bool)adm.ShowDialog()) //отображаем его в модальном режиме
                        if (adm.Valid) //если с данными все ок - применяем
                        {
                            try
                            {
                                row.BeginEdit();
                                row[0] = adm.value[0];
                                row[1] = adm.value[1];
                                row[2] = adm.value[2];
                                row[3] = adm.value[3];
                                row[4] = adm.value[4];
                                row[5] = adm.value[5];
                                row.AcceptChanges();
                            }

                            catch
                            {
                                //если во время редактирования удалить редактируемую строку - попадаем сюда
                                row.RejectChanges();
                                MessageBox.Show("Ошибка при редактировании!\nВозможно запись была удалена во время редактирования");
                            }
                            break;
                        }
                        else
                            MessageBox.Show("Валидация не пройдена");
                    else break; //если cancel
                }
            }
        }
        //отображение окон-таблиц
        //типы работ
        private void Types(object sender, RoutedEventArgs e)
        {
            WorkTypes c = new WorkTypes();
            c.Owner = this;
            c.Show();
        }
        //автомобили
        private void cars(object sender, RoutedEventArgs e)
        {
            Cars c = new Cars();
            c.Owner = this;
            c.Show();
        }
        //владельцы
        private void owners(object sender, RoutedEventArgs e)
        {
            Owners c = new Owners();
            c.Owner = this;
            c.Show();
        }
        //модели машин
        private void models(object sender, RoutedEventArgs e)
        {
            Models c = new Models();
            c.Owner = this;
            c.Show();
        }
        //мастера
        private void masters(object sender, RoutedEventArgs e)
        {
            Masters c = new Masters();
            c.Owner = this;
            c.Show();
        }
        //сохранение
        private void save(object sender, RoutedEventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog
            {
                Filter = "xml|*.xml",
                AddExtension = true,
                DefaultExt = "xml"
            };
            try
            {
                f.ShowDialog();
                Stream xml = File.OpenWrite(f.FileName);
                set.WriteXml(xml);
                xml.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
        //загрузка
        private void open(object sender, RoutedEventArgs e)
        {
            set = new DataSet1();
            setEvents();
            list.Items.Clear();
            OpenFileDialog f = new OpenFileDialog
            {
                Filter = "xml|*.xml",
                AddExtension = true,
                DefaultExt = "xml"
            };
            try
            {
                f.ShowDialog();
                Stream xml = File.OpenRead(f.FileName);
                set.ReadXml(xml);
                xml.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            refresh();
        }
    }
}
