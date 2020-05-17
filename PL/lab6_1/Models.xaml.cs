using System.Windows;

namespace lab6_1
{
    public partial class Models : Window, IValue
    {
        public object[] Val { get; set; } // возвращаемое значение, которое определяется в классе DataWindow<>
        DataWindow<AddModel> W; // Объект класса с общей логикой
        public Models()
        {
            InitializeComponent(); //все как обычно
            Loaded += Models_Loaded;

            W = new DataWindow<AddModel>(this);

            addButton.Click += W.addButton_Click; //делегируем обработку событий в "общий" класс
            editButton.Click += W.editButton_Click;
            removeButton.Click += W.removeButton_Click;
            list.MouseDoubleClick += W.List_MouseDoubleClick;
        }
        private void Models_Loaded(object sender, RoutedEventArgs e)
        {
            //Подцепляем таблицу и события DataSet'a, причем именно здесь, потому что значение поля Owner будет задано после создания объекта

            W.oTable = MainWindow.set.Model;
            W.oTable.RowChanged += W.refresh;
            W.oTable.RowDeleted += W.refresh;
            W.oTable.TableNewRow += W.refresh;
            W.List = list;
            W.refresh();
        }
    }
}
