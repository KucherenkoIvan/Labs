using System.Data;
using System.Windows;
using System.Windows.Input;

namespace lab6_1
{
    public partial class Owners : Window
    {
        MainWindow owner;
        DataRowCollection oRows;
        public object[] Val = null;
        public Owners()
        {
            InitializeComponent();
            Loaded += Owners_Loaded;
            list.MouseDoubleClick += List_MouseDoubleClick; ;
        }

        private void List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Val = owner.set.Owner.Rows[list.SelectedIndex].ItemArray;
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
                list.Items.Add(r.ItemArray[0] + " " + r.ItemArray[2]);
        }

        private void Owners_Loaded(object sender, RoutedEventArgs e)
        {
            owner = this.Owner as MainWindow;
            oRows = owner.set.Owner.Rows;
            owner.set.Owner.RowChanged += refresh;
            owner.set.Owner.RowDeleted += refresh;
            owner.set.Owner.TableNewRow += refresh;
            refresh(null, (DataRowChangeEventArgs)null);
        }
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            int index = list.SelectedIndex;
            if (index != -1 && MessageBox.Show("Удаление этого элемента может повлечь удаление связанных с ним записей\nПродолжить?", "Удаление",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                oRows.RemoveAt(list.SelectedIndex);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            while (flag)
            {
                AddOwner adm = new AddOwner();
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
            int index = list.SelectedIndex;
            if (index != -1)
            {
                bool flag = true;
                while (flag)
                {
                    AddOwner adm = new AddOwner();
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
