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
    public partial class Search : Form
    {
        public delegate void FilterChangedHandler(Filter f);
        public event FilterChangedHandler FilterChanged;
        public Filter f;
        public Search()
        {
            InitializeComponent();
            f = new Filter();
            FormClosing += Search_FormClosing;
        }

        private void Search_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall && e.CloseReason != CloseReason.FormOwnerClosing && e.CloseReason != CloseReason.WindowsShutDown)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void Search_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = comboBox2.SelectedIndex = 0;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                f.Read = dateTimePicker2.Value;
            else f.Read = DateTime.MinValue;
            
            if (checkBox1.Checked)
                f.Got = dateTimePicker1.Value;
            else f.Got = DateTime.MinValue;
            if (int.TryParse(comboBox2.SelectedItem.ToString(), out int i))
                f.Mark = i;
            else f.Mark = -1;
            
            if (int.TryParse(textBox9.Text, out int j))
                f.Year = j;
            else f.Year = -1;
            
            f.ISBN = textBox8.Text;
            
            f.Comment = textBox7.Text;
            
            f.Source = textBox6.Text;
            
            if (comboBox1.SelectedIndex > 0)
                f.Binding = comboBox1.SelectedItem.ToString();
            
            f.PbHouse = textBox4.Text;
            
            f.Genre = textBox3.Text;
            
            f.Authors = textBox2.Text;
            
            f.Name = textBox1.Text;

            FilterChanged(f);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f = new Filter();
            FilterChanged(f);

            for (int i = 0; i < 3; i++)
                foreach (Control c in Controls)
                    c.Dispose();
            InitializeComponent();
            comboBox1.SelectedIndex = comboBox2.SelectedIndex = 0;
        }
    }
}

public class Filter
{
    public string Name { get; set; }
    public string Authors { get; set; }
    public string Genre { get; set; }
    public string PbHouse { get; set; }
    public string Binding { get; set; }
    public string Source { get; set; }
    public string Comment { get; set; }
    public string ISBN { get; set; }
    public int Year { get; set; }
    public int Mark { get; set; }
    public DateTime Got { get; set; }
    public DateTime Read { get; set; }
    public Filter()
    {
        Name = null;
        Authors = null;
        Genre = null;
        PbHouse = null;
        Binding = null;
        Source = null;
        Comment = null;
        ISBN = null;
        Year = -1;
        Mark = -1;
        Got = DateTime.MinValue;
        Read = DateTime.MinValue;
    }
}
