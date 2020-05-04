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
    /// Логика взаимодействия для AddCar.xaml
    /// </summary>
    public partial class AddCar : Window
    {
        public bool Valid
        {
            get
            {
                return !String.IsNullOrEmpty(owner.Text) && !String.IsNullOrEmpty(Model.Text) && Number.Text.Length >= 6;
                //todo
                //Upgrade validation code here
            }
        }
        public object[] value = new object[4];
        public void Fill(object[] tuple, DataSet1 set)
        {//code model owner number
            Number.Text = tuple[3].ToString();
            owner.Text = set.Owner.Rows.Find(tuple[2].ToString()).ItemArray[2].ToString();
            Model.Text = set.Model.Rows.Find(tuple[1].ToString()).ItemArray[1].ToString();
        }
        public AddCar()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            value[0] = null;
            value[3] = Number.Text;
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void refresh(object sender, RoutedEventArgs e)
        {
            owner.Text = Model.Text = Number.Text = "";
        }

        private void mObserve(object sender, RoutedEventArgs e)
        {
            Models m = new Models();
            m.Owner = Owner;
            if ((bool)m.ShowDialog())
            {
                value[1] = (m.Val as DataRow).ItemArray[0].ToString();
                Model.Text = (m.Val as DataRow).ItemArray[1].ToString();
            }
        }

        private void oObserve(object sender, RoutedEventArgs e)
        {
            Owners m = new Owners();
            m.Owner = Owner;
            if ((bool)m.ShowDialog())
            {
                value[2] = (m.Val as DataRow).ItemArray[0].ToString();
                owner.Text = (m.Val as DataRow).ItemArray[2].ToString();
            }
        }
    }
}
