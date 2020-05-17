using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace lab6_1
{
    public partial class AddWorkType : Window, IAddSomething
    {
        public bool Valid
        {
            get
            {
                return !string.IsNullOrEmpty(Name.Text);
            }
        }
        public object[] Value { get; set; }
        public void Fill(object[] tuple)
        {
            Name.Text = tuple[1].ToString();
        }
        public AddWorkType()
        {
            InitializeComponent();
            Name.GotFocus += gotFocus;
        }

        private void gotFocus(object sender, RoutedEventArgs e)
        {
            (sender as Control).Background = Brushes.White;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Value = new object[] { null, Name.Text };

            if (!Valid)
            {
                Name.Background = (new SolidColorBrush(Color.FromArgb(90, 250, 20, 20)));
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
            Name.Text = "";
        }
    }
}
