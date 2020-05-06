using System.Windows;
using System.Windows.Input;

namespace lab6_1
{
    public partial class AddOwner : Window
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
        public object[] value;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            value = new object[] { null, pSeries.Text + "|" + pNum.Text + "|" + pInfo.Text, Name.Text };
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
