using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab1;
using System.Threading;

namespace Lab2
{
    public partial class User : Form
    {
        Search src;
        public List<Book> Base = new List<Book>();
        public Form1 Parent;

        public void B_Update()
        {
            listBox1.DataSource = Base.ToArray();
        }
        public User()
        {
            src = new Search();
            InitializeComponent();
            label1.Text = label2.Text = label3.Text = label4.Text = label5.Text = label6.Text = label7.Text = label8.Text = label9.Text = label10.Text = label11.Text = "";

            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;

            src.FilterChanged += UseFilter;

            listBox1.SelectedValueChanged += ListBox1_SelectedValueChanged;


            редактироватьToolStripMenuItem.Enabled = удалитьToolStripMenuItem.Enabled = поискToolStripMenuItem.Enabled = false;
        }

        private void ListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            добавитьЗаписьToolStripMenuItem.Enabled = удалитьToolStripMenuItem.Enabled = поискToolStripMenuItem.Enabled = listBox1.Items.Count != 0;
        }

        public void UseFilter(Filter f)
        {
            List<Book> Filtred = new List<Book>(Base);
            List<Book> Res = new List<Book>();
            if (!string.IsNullOrEmpty(f.Name))
            {
                foreach (Book b in Filtred)
                    if (b.Name.Contains(f.Name))
                        Res.Add(b);
                Filtred.Clear();
                Filtred.AddRange(Res);
                Res.Clear();
            }
            if (!string.IsNullOrEmpty(f.Authors) && Filtred.Count != 0)
            {
                foreach (Book b in Filtred)
                    if (b.Authors.Contains(f.Authors))
                        Res.Add(b);
                Filtred.Clear();
                Filtred.AddRange(Res);
                Res.Clear();
            }
            if (!string.IsNullOrEmpty(f.Genre) && Filtred.Count != 0)
            {
                foreach (Book b in Filtred)
                    if (b.Genre.Contains(f.Genre))
                        Res.Add(b);
                Filtred.Clear();
                Filtred.AddRange(Res);
                Res.Clear();
            }
            if (!string.IsNullOrEmpty(f.PbHouse) && Filtred.Count != 0)
            {
                foreach (Book b in Filtred)
                    if (b.PublishingHouse.Contains(f.PbHouse))
                        Res.Add(b);
                Filtred.Clear();
                Filtred.AddRange(Res);
                Res.Clear();
            }
            if (!string.IsNullOrEmpty(f.Binding) && Filtred.Count != 0)
            {
                foreach (Book b in Filtred)
                    if (b.Binding.Contains(f.Binding))
                        Res.Add(b);
                Filtred.Clear();
                Filtred.AddRange(Res);
                Res.Clear();
            }
            if (!string.IsNullOrEmpty(f.Source) && Filtred.Count != 0)
            {
                foreach (Book b in Filtred)
                    if (b.Source.Contains(f.Source))
                        Res.Add(b);
                Filtred.Clear();
                Filtred.AddRange(Res);
                Res.Clear();
            }
            if (!string.IsNullOrEmpty(f.Comment) && Filtred.Count != 0)
            {
                foreach (Book b in Filtred)
                    if (b.Comment.Contains(f.Comment))
                        Res.Add(b);
                Filtred.Clear();
                Filtred.AddRange(Res);
                Res.Clear();
            }
            if (!string.IsNullOrEmpty(f.ISBN) && Filtred.Count != 0)
            {
                foreach (Book b in Filtred)
                    if (b.ISBN.Contains(f.ISBN))
                        Res.Add(b);
                Filtred.Clear();
                Filtred.AddRange(Res);
                Res.Clear();
            }
            if (f.Year != -1 && Filtred.Count != 0)
            {
                foreach (Book b in Filtred)
                    if (b.Year.ToString().Contains(f.Year.ToString()))
                        Res.Add(b);
                Filtred.Clear();
                Filtred.AddRange(Res);
                Res.Clear();
            }
            if (f.Mark != -1 && Filtred.Count != 0)
            {
                foreach (Book b in Filtred)
                    if (b.Mark.ToString().Contains(f.Mark.ToString()))
                        Res.Add(b);
                Filtred.Clear();
                Filtred.AddRange(Res);
                Res.Clear();
            }
            if (f.Got != DateTime.MinValue && Filtred.Count != 0)
            {
                foreach (Book b in Filtred)
                    if (!(b.Got.Day != f.Got.Day && b.Got.Month != f.Got.Month && b.Got.Year != f.Got.Year))

                        Res.Add(b);
                Filtred.Clear();
                Filtred.AddRange(Res);
                Res.Clear();
            }
            if (f.Read != DateTime.MinValue && Filtred.Count != 0)
            {
                foreach (Book b in Filtred)
                    if (!(b.Read.Day != f.Read.Day && b.Read.Month != f.Read.Month && b.Read.Year != f.Read.Year))
                        Res.Add(b);
                Filtred.Clear();
                Filtred.AddRange(Res);
                Res.Clear();
            }
            listBox1.DataSource = Filtred.ToArray();
        }
        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                label1.Text = label2.Text = label3.Text = label4.Text = label5.Text = label6.Text = label7.Text = label8.Text = label9.Text = label10.Text = label11.Text = "";
            else
            {
                Book b = Base[listBox1.SelectedIndex];
                label1.Text = $"Название: {b.Name}";
                label2.Text = $"Авторы: {b.Authors}";
                label3.Text = $"Жанр: {b.Genre}";
                label4.Text = $"Издательство: {b.PublishingHouse}";
                label5.Text = $"Переплет: {b.Binding}";
                label8.Text = $"ISBN: {b.ISBN}";
                label6.Text = $"Источник появления: {b.Source}";
                label7.Text = $"Оценка и Комментарий: {b.Mark}  -  {b.Comment}";
                label9.Text = $"Год выпуска: {b.Year}";
                label10.Text = $"Дата получения: {b.Got.ToShortDateString()}";
                label11.Text = $"Дата прочтения: {b.Read.ToShortDateString()}";
            }
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            src.Show();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add d = new Add();
            d.Update(Base[listBox1.SelectedIndex]);
            if (d.ShowDialog() == DialogResult.OK)
            {
                Parent.Edit((Book)d.value, listBox1.SelectedIndex);
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                Parent.Remove(listBox1.SelectedIndex);
            }
        }

        private void добавитьЗаписьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add d = new Add();
            if (d.ShowDialog() == DialogResult.OK)
            {
                Parent.Add((Book)d.value);
            }
        }
    }
}
