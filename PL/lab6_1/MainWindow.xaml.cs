using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab6_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
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
            if (list.SelectedIndex != -1)
            {
                editButton_Click(null, null);

            }
        }

        private void refresh(object sender, DataTableNewRowEventArgs e)
        {
            refresh(null, (DataRowChangeEventArgs)null);
        }

        private void refresh(object sender, DataRowChangeEventArgs e)
        {
            list.Items.Clear();
            foreach (DataRow r in set.Work.Rows)
                list.Items.Add(r.ItemArray[2] + " " + r.ItemArray[0]);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            set.Work.RowChanged += refresh;
            set.Work.RowDeleted += refresh;
            set.Work.TableNewRow += refresh;
            refresh(null, (DataRowChangeEventArgs)null);
        }
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (list.SelectedIndex != -1)
                set.Work.Rows.RemoveAt(list.SelectedIndex);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            while (flag)
            {
                AddWork adm = new AddWork();
                adm.Owner = this;
                if ((bool)adm.ShowDialog())
                    if (adm.Valid)
                    {
                        set.Work.Rows.Add(adm.value);
                        flag = false;
                    }
                    else
                        MessageBox.Show("Валидация не пройдена");
                else flag = false;
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (list.SelectedIndex != -1)
            {
                bool flag = true;
                while (flag)
                {
                    AddWork adm = new AddWork();
                    adm.Owner = this;
                    adm.Fill(set.Work.Rows[list.SelectedIndex].ItemArray, set);
                    if ((bool)adm.ShowDialog())
                        if (adm.Valid)
                        {
                            set.Work.Rows[list.SelectedIndex].ItemArray = adm.value;
                            flag = false;
                        }
                        else
                            MessageBox.Show("Валидация не пройдена");
                    else flag = false;
                }
            }
        }

        private void Types(object sender, RoutedEventArgs e)
        {
            WorkTypes c = new WorkTypes();
            c.Owner = this;
            c.Show();
        }

        private void cars(object sender, RoutedEventArgs e)
        {
            Cars c = new Cars();
            c.Owner = this;
            c.Show();
        }

        private void owners(object sender, RoutedEventArgs e)
        {
            Owners c = new Owners();
            c.Owner = this;
            c.Show();
        }

        private void models(object sender, RoutedEventArgs e)
        {
            Models c = new Models();
            c.Owner = this;
            c.Show();
        }

        private void masters(object sender, RoutedEventArgs e)
        {
            Masters c = new Masters();
            c.Owner = this;
            c.Show();
        }
    }
}
