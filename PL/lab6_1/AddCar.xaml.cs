using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace lab6_1
{
    public partial class AddCar : Window, IAddSomething
    {
        public bool Valid
        {
            get
            {
                return
                    Value[1] != null && Value[2] != null &&
                    !String.IsNullOrEmpty(owner.Text) && !String.IsNullOrEmpty(Model.Text) && Number.Text.Length >= 6;

            }
        }
        DataSet1 set = MainWindow.set;
        public object[] Value { get; set; } = new object[4];
        public void Fill(object[] tuple)
        {
            Number.Text = tuple[3].ToString();
            owner.Text = set.Owner.Rows.Find(tuple[2].ToString()).ItemArray[2].ToString();
            Model.Text = set.Model.Rows.Find(tuple[1].ToString()).ItemArray[1].ToString();
            Value[1] = tuple[1];
            Value[2] = tuple[2];
        }
        public AddCar()
        {
            InitializeComponent();
            Number.GotFocus += gotFocus;
            owner.GotFocus += gotFocus;
            Model.GotFocus += gotFocus;
        }
        private void gotFocus(object sender, RoutedEventArgs e)
        {
            (sender as Control).Background = Brushes.White;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Value[0] = null;
            Value[3] = Number.Text;
            if (!Valid)
            {
                if (String.IsNullOrEmpty(owner.Text))
                    owner.Background = (new SolidColorBrush(Color.FromArgb(90, 250, 20, 20)));
                if (String.IsNullOrEmpty(Model.Text))
                    Model.Background = (new SolidColorBrush(Color.FromArgb(90, 250, 20, 20)));
                if (Number.Text.Length <= 8)
                    Number.Background = (new SolidColorBrush(Color.FromArgb(90, 250, 20, 20)));
                MessageBox.Show("Валидация не пройдена");
                return;
            }

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
            m.Title = m.Title + " - выбор записи";
            m.list.Background = Brushes.Gainsboro;
            m.Owner = Owner;
            if ((bool)m.ShowDialog())
            {
                Value[1] = m.Val[0];
                Model.Text = m.Val[1].ToString();
            }
        }

        private void oObserve(object sender, RoutedEventArgs e)
        {
            Owners m = new Owners();
            m.Title = m.Title + " - выбор записи";
            m.list.Background = Brushes.Gainsboro;
            m.Owner = Owner;
            if ((bool)m.ShowDialog())
            {
                Value[2] = m.Val[0];
                owner.Text = m.Val[2].ToString();
            }
        }
    }
}
