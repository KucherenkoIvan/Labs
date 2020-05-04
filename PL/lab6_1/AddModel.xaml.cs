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

namespace lab6_1
{
    /// <summary>
    /// Логика взаимодействия для AddWorkType.xaml
    /// </summary>
    public partial class AddModel : Window
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
        public AddModel()
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
