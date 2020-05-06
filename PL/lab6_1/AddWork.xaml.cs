using System;
using System.Windows;

namespace lab6_1
{
    public partial class AddWork : Window
    {
        DataSet1 set;
        public bool Valid
        {
            get
            {
                return value[0] != null && value[1] != null && value[2] != null &&
                    ((DateTime)end.SelectedDate - (DateTime)start.SelectedDate).Days >= 0 &&
                    !string.IsNullOrEmpty(value[5].ToString());
            }
        }
        public object[] value = new object[6];
        public void Fill(object[] tuple, DataSet1 set)
        {
            value[0] = tuple[0];
            value[1] = tuple[1];
            value[2] = tuple[2];
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
            Loaded += AddWork_Loaded;
        }

        private void AddWork_Loaded(object sender, RoutedEventArgs e)
        {
            set = (Owner as MainWindow).set;
        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            value[3] = ((DateTime)start.SelectedDate).ToShortDateString();
            value[4] = ((DateTime)end.SelectedDate).ToShortDateString();
            value[5] = shortcut.Text;
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
            m.Owner = Owner;
            if ((bool)m.ShowDialog())
            {
                value[2] = set.Car.FindBycode((int)m.Val[0]);
                car.Text = set.Model.Rows.Find(set.Car.Rows.Find(m.Val[0].ToString()).ItemArray[1]).ItemArray[1].ToString() + " (" +
                    m.Val[3].ToString() + ")";
            }
        }

        private void masters_observe(object sender, RoutedEventArgs e)
        {
            Masters m = new Masters();
            m.Owner = Owner;
            if ((bool)m.ShowDialog())
            {
                value[1] = set.Master.FindBycode((int)m.Val[0]);
                master.Text = m.Val[2].ToString();
            }
        }

        private void type_observe(object sender, RoutedEventArgs e)
        {
            WorkTypes m = new WorkTypes();
            m.Owner = Owner;
            if ((bool)m.ShowDialog())
            {
                value[0] = set.WorkType.FindBycode((int)m.Val[0]);
                type.Text = m.Val[1].ToString();
            }
        }
    }
}
