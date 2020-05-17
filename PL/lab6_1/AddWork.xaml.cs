using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace lab6_1
{
    public partial class AddWork : Window, IAddSomething
    {
        DataSet1 set = MainWindow.set;
        public bool Valid
        {
            get
            {
                return Value[0] != null && Value[1] != null && Value[2] != null &&
                    ((DateTime)end.SelectedDate - (DateTime)start.SelectedDate).Days >= 0 &&
                    ((DateTime)start.SelectedDate - DateTime.Now).Days >= 0 &&
                    !string.IsNullOrEmpty(Value[5].ToString());
            }
        }
        public object[] Value { get; set; } = new object[6];
        public void Fill(object[] tuple)
        {
            Value[0] = tuple[0];
            Value[1] = tuple[1];
            Value[2] = tuple[2];
            type.Text = set.WorkType.Rows.Find(tuple[0].ToString()).ItemArray[1].ToString();

            master.Text = set.Master.Rows.Find(tuple[1].ToString()).ItemArray[2].ToString();

            car.Text = set.Model.Rows.Find(set.Car.Rows.Find(tuple[2].ToString()).ItemArray[1]).ItemArray[1] + " ("
                + set.Car.Rows.Find(tuple[2].ToString()).ItemArray[3].ToString() + ")";

            start.SelectedDate = DateTime.Parse(tuple[3].ToString());

            end.SelectedDate = DateTime.Parse(tuple[4].ToString());

            shortcut.Text = tuple[5].ToString();
        }
        public AddWork()
        {
            InitializeComponent();
            start.SelectedDate = end.SelectedDate = DateTime.Now;
            type.GotFocus += gotFocus;
            master.GotFocus += gotFocus;
            car.GotFocus += gotFocus;
            start.GotFocus += gotFocus;
            end.GotFocus += gotFocus;
            shortcut.GotFocus += gotFocus;
        }
        private void gotFocus(object sender, RoutedEventArgs e)
        {
            (sender as Control).Background = Brushes.White;
        }
        private void Ok(object sender, RoutedEventArgs e)
        {
            Value[3] = ((DateTime)start.SelectedDate).ToShortDateString();
            Value[4] = ((DateTime)end.SelectedDate).ToShortDateString();
            Value[5] = shortcut.Text;
            if (!Valid)
            {
                if (string.IsNullOrEmpty(type.Text))
                    type.Background = (new SolidColorBrush(Color.FromArgb(90, 250, 20, 20)));
                if (string.IsNullOrEmpty(car.Text))
                    car.Background = (new SolidColorBrush(Color.FromArgb(90, 250, 20, 20)));
                if (string.IsNullOrEmpty(master.Text))
                    master.Background = (new SolidColorBrush(Color.FromArgb(90, 250, 20, 20)));
                if (string.IsNullOrEmpty(shortcut.Text))
                    shortcut.Background = (new SolidColorBrush(Color.FromArgb(90, 250, 20, 20)));
                if (((DateTime)end.SelectedDate - (DateTime)start.SelectedDate).Days < 0)
                    end.Background = (new SolidColorBrush(Color.FromArgb(90, 250, 20, 20)));
                if (((DateTime)start.SelectedDate - DateTime.Now).Days < 0)
                    start.Background = (new SolidColorBrush(Color.FromArgb(90, 250, 20, 20)));
                MessageBox.Show("Валидация не пройдена");
                return;
            }
            DialogResult = true;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void refresh(object sender, RoutedEventArgs e)
        {
            type.Text = shortcut.Text = master.Text = car.Text = "";
            start.SelectedDate = end.SelectedDate = null;
        }

        private void car_observe(object sender, RoutedEventArgs e)
        {
            Cars m = new Cars();
            m.Title = m.Title + " - выбор записи";
            m.list.Background = Brushes.Gainsboro;
            m.Owner = Owner;
            if ((bool)m.ShowDialog())
            {
                Value[2] = set.Car.FindBycode((int)m.Val[0]);
                car.Text = set.Model.Rows.Find(set.Car.Rows.Find(m.Val[0].ToString()).ItemArray[1]).ItemArray[1].ToString() + " (" +
                    m.Val[3].ToString() + ")";
            }
        }

        private void masters_observe(object sender, RoutedEventArgs e)
        {
            Masters m = new Masters();
            m.Title = m.Title + " - выбор записи";
            m.list.Background = Brushes.Gainsboro;
            m.Owner = Owner;
            if ((bool)m.ShowDialog())
            {
                Value[1] = set.Master.FindBycode((int)m.Val[0]);
                master.Text = m.Val[2].ToString();
            }
        }

        private void type_observe(object sender, RoutedEventArgs e)
        {
            WorkTypes m = new WorkTypes();
            m.Title = m.Title + " - выбор записи";
            m.list.Background = Brushes.Gainsboro;
            m.Owner = Owner;
            if ((bool)m.ShowDialog())
            {
                Value[0] = set.WorkType.FindBycode((int)m.Val[0]);
                type.Text = m.Val[1].ToString();
            }
        }
    }
}
