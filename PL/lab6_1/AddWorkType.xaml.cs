using System.Windows;

namespace lab6_1
{
    public partial class AddWorkType : Window
    {
        public bool Valid
        {
            get
            {
                return !string.IsNullOrEmpty(Name.Text);
            }
        }
        public object[] value;
        public void Fill(object[] tuple)
        {
            Name.Text = tuple[1].ToString();
        }
        public AddWorkType()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            value = new object[] { null, Name.Text };
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
