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
    /// Логика взаимодействия для Cars.xaml
    /// </summary>
    public partial class Cars : Window
    {
        MainWindow owner;
        DataRowCollection oRows;
        public object Val = null;
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
                try
                {
                    Val = owner.set.Car.Rows[list.SelectedIndex];
                    DialogResult = true;
                }
                catch
                {
                    editButton_Click(null, null);
                }
            }
        }

        private void refresh(object sender, DataTableNewRowEventArgs e)
        {
            refresh(null, (DataRowChangeEventArgs)null);
        }

        private void refresh(object sender, DataRowChangeEventArgs e)
        {
            list.Items.Clear();
            foreach (DataRow r in oRows)
                list.Items.Add(r.ItemArray[0] + " " + owner.set.Owner.Rows.Find(r.ItemArray[2]).ItemArray[2].ToString() + " " + r.ItemArray[3]);
        }

        private void Cars_Loaded(object sender, RoutedEventArgs e)
        {
            owner = this.Owner as MainWindow;
            oRows = owner.set.Car.Rows;
            owner.set.Car.RowChanged += refresh;
            owner.set.Car.RowDeleted += refresh;
            owner.set.Car.TableNewRow += refresh;
            owner.set.Owner.TableNewRow += refresh;
            owner.set.Owner.RowChanged += refresh;
            owner.set.Owner.RowDeleted += refresh;
            refresh(null, (DataRowChangeEventArgs)null);
        }
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (list.SelectedIndex != -1)
                oRows.RemoveAt(list.SelectedIndex);
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
                        oRows.Add(adm.value);
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
                    AddCar adm = new AddCar();
                    adm.Owner = owner;
                    adm.Fill(oRows[list.SelectedIndex].ItemArray, owner.set);
                    if ((bool)adm.ShowDialog())
                        if (adm.Valid)
                        {
                            oRows[list.SelectedIndex].ItemArray = adm.value;
                            flag = false;
                        }
                        else
                            MessageBox.Show("Валидация не пройдена");
                    else flag = false;
                }
            }
        }
    }
}
