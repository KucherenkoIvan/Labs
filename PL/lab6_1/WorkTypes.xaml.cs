using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace lab6_1
{
    /// <summary>
    /// Логика взаимодействия для WorkTypes.xaml
    /// </summary>
    public partial class WorkTypes : Window
    {
        MainWindow owner;
        DataRowCollection oRows;
        public object Val = null;
        public WorkTypes()
        {
            InitializeComponent();
            Loaded += Masters_Loaded; list.MouseDoubleClick += List_MouseDoubleClick; ;
        }

        private void List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Val = owner.set.WorkType.Rows[list.SelectedIndex];
                DialogResult = true;
            }
            catch
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
            foreach (DataRow r in oRows)
                list.Items.Add(r.ItemArray[0] + " " + r.ItemArray[1]);
        }

        private void Masters_Loaded(object sender, RoutedEventArgs e)
        {
            owner = this.Owner as MainWindow;
            oRows = owner.set.WorkType.Rows;
            owner.set.WorkType.RowChanged += refresh;
            owner.set.WorkType.RowDeleted += refresh;
            owner.set.WorkType.TableNewRow += refresh;
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
                AddWorkType adm = new AddWorkType();
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
                    AddWorkType adm = new AddWorkType();
                    adm.Fill(oRows[list.SelectedIndex].ItemArray);
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
