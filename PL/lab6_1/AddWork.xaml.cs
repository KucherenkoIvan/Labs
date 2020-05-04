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
    /// Логика взаимодействия для AddWork.xaml
    /// </summary>
    public partial class AddWork : Window
    {
        public bool Valid { get; set; }
        public object[] value = null;
        public void Fill(object[] tuple, DataSet1 set)
        {

        }
        public AddWork()
        {
            InitializeComponent();
        }

        private void Ok(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
        }
    }
}
