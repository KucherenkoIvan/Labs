using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Add : Form
    {
        public object value;
        public Add()
        {
            InitializeComponent();
            value = null;
        }
        public void Update(Book b)
        {
            textBox1.Text = b.Name;
            textBox2.Text = b.Authors;
            textBox3.Text = b.Genre;
            textBox4.Text = b.PublishingHouse;
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(b.Binding);
            textBox6.Text = b.Source;
            textBox7.Text = b.Comment;
            textBox8.Text = b.ISBN;
            textBox9.Text = b.Year.ToString();
            comboBox2.SelectedIndex = b.Mark;
            dateTimePicker1.Value = b.Got;
            dateTimePicker2.Value = b.Read;
        }

        private void Add_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(textBox9.Text, out int i))
                {
                    value = new Book(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, comboBox1.SelectedItem.ToString(), textBox6.Text, textBox7.Text,
                        textBox8.Text, i, int.Parse(comboBox2.SelectedItem.ToString()), dateTimePicker1.Value, dateTimePicker2.Value);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    MessageBox.Show("Введены некорректные данные");
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
