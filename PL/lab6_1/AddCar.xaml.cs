using System;
using System.Windows;

namespace lab6_1
{
    public partial class AddCar : Window
    {
        public bool Valid
        {
            get
            {
                return
                    value[1] != null && value[2] != null &&
                    !String.IsNullOrEmpty(owner.Text) && !String.IsNullOrEmpty(Model.Text) && Number.Text.Length >= 6;

            }
        }
        DataSet1 set = null;
        public object[] value = new object[4];
        public void Fill(object[] tuple, DataSet1 set)
        {
            Number.Text = tuple[3].ToString();
            owner.Text = set.Owner.Rows.Find(tuple[2].ToString()).ItemArray[2].ToString();
            Model.Text = set.Model.Rows.Find(tuple[1].ToString()).ItemArray[1].ToString();
            value[1] = tuple[1];
            value[2] = tuple[2];
        }
        public AddCar()
        {
            InitializeComponent();
            Loaded += AddCar_Loaded;
        }

        private void AddCar_Loaded(object sender, RoutedEventArgs e)
        {
            set = (Owner as MainWindow).set;
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
                value[1] = set.Model.FindBycode(int.Parse(m.Val[0].ToString()));
                Model.Text = m.Val[1].ToString();
            }
        }

        private void oObserve(object sender, RoutedEventArgs e)
        {
            Owners m = new Owners();
            m.Owner = Owner;
            if ((bool)m.ShowDialog())
            {
                value[2] = set.Owner.FindBycode(int.Parse(m.Val[0].ToString()));
                owner.Text = m.Val[2].ToString();
            }
        }
    }
}
