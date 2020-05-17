using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace lab6_1
{
    public partial class AddOwner : Window, IAddSomething
    {
        public bool Valid
        {
            get
            {
                return (!string.IsNullOrEmpty(Name.Text) &&
                    !string.IsNullOrEmpty(pInfo.Text) &&
                    pSeries.Text.Length == 4 && pNum.Text.Length == 6);
            }
        }
        public object[] Value { get; set; }
        public void Fill(object[] tuple)
        {
            string[] passport = tuple[1].ToString().Split('|');
            Name.Text = tuple[2].ToString();
            pSeries.Text = passport[0];
            pNum.Text = passport[1];
            pInfo.Text = passport[2];
        }
        public AddOwner()
        {
            InitializeComponent();
            Name.GotFocus += gotFocus;
            pSeries.GotFocus += gotFocus;
            pNum.GotFocus += gotFocus;
            pInfo.GotFocus += gotFocus;
        }
        private void gotFocus(object sender, RoutedEventArgs e)
        {
            (sender as Control).Background = Brushes.White;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Value = new object[] { null, pSeries.Text + "|" + pNum.Text + "|" + pInfo.Text, Name.Text };
            if (!Valid)
            {
                if (string.IsNullOrEmpty(Name.Text))
                    Name.Background = (new SolidColorBrush(Color.FromArgb(90, 250, 20, 20)));
                if (string.IsNullOrEmpty(pInfo.Text))
                    pInfo.Background = (new SolidColorBrush(Color.FromArgb(90, 250, 20, 20)));
                if (pSeries.Text.Length != 4)
                    pSeries.Background = (new SolidColorBrush(Color.FromArgb(90, 250, 20, 20)));
                if (pNum.Text.Length != 6)
                    pNum.Background = (new SolidColorBrush(Color.FromArgb(90, 250, 20, 20)));
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
            Name.Text = pInfo.Text = pNum.Text = pSeries.Text = "";
        }
        private void NumFilter(object sender, KeyEventArgs e)
        {
            if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)))
                e.Handled = true;
        }
    }
}
