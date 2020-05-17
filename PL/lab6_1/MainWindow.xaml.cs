using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace lab6_1
{
    public partial class MainWindow : Window
    {
        public static DataSet1 set = new DataSet1();
        Window car;
        Window master;
        Window owner;
        Window model;
        Window type;

        public static bool loading = false;
        public object[] Val { get; set; } // возвращаемое значение, которое определяется в классе DataWindow<>
        DataWindow<AddWork> W; // Объект класса с общей логикой

        public MainWindow()
        {
            InitializeComponent(); //все как обычно
            Loaded += MainWindow_Loaded;

            W = new DataWindow<AddWork>(this);

            addButton.Click += W.addButton_Click; //делегируем обработку событий в "общий" класс
            editButton.Click += W.editButton_Click;
            removeButton.Click += W.removeButton_Click;
            list.MouseDoubleClick += W.List_MouseDoubleClick;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Подцепляем таблицу и события DataSet'a, причем именно здесь, потому что значение поля Owner будет задано после создания объекта

            W.oTable = MainWindow.set.Work;
            W.oTable.RowChanged += W.refresh;
            W.oTable.RowDeleted += W.refresh;
            W.oTable.TableNewRow += W.refresh;
            W.List = list;
            W.refresh();
        }
        //типы работ
        private void Types(object sender, RoutedEventArgs e)
        {
            if (type == null)
            {
                WorkTypes c = new WorkTypes();
                c.Owner = this;
                type = c;
                c.Show();
            }
            else type.Focus();
        }
        //автомобили
        private void cars(object sender, RoutedEventArgs e)
        {
            if (car == null)
            {
                Cars c = new Cars();
                c.Owner = this;
                car = c;
                c.Show();
            }
            else car.Focus();
        }
        //владельцы
        private void owners(object sender, RoutedEventArgs e)
        {
            if (owner == null)
            {
                Owners c = new Owners();
                c.Owner = this;
                owner = c;
                c.Show();
            }
            else owner.Focus();
        }
        //модели машин
        private void models(object sender, RoutedEventArgs e)
        {
            if (model == null)
            {
                Models c = new Models();
                c.Owner = this;
                model = c;
                c.Show();
            }
            else model.Focus();
        }
        //мастера
        private void masters(object sender, RoutedEventArgs e)
        {
            if (master == null)
            {

                Masters c = new Masters();
                c.Owner = this;
                master = c;
                c.Show();
            }
            else master.Focus();
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
            set.Clear();
            list.Items.Clear();
            loading = true;
            OpenFileDialog f = new OpenFileDialog
            {
                Filter = "xml|*.xml",
                AddExtension = true,
                DefaultExt = "xml"
            };
            try
            {
                if (MessageBox.Show("Открытие новой базы данных приведет к закрытию всех окон кроме главного\nПродолжить?",
                    "Открытие файла", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    loading = false;
                    return;
                }
                f.ShowDialog();
                Stream xml = File.OpenRead(f.FileName);
                set.ReadXml(xml);
                xml.Close();
                foreach (Window w in OwnedWindows)
                    w.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            loading = false;
            W.refresh();
        }
    }
}
