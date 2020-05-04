using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Lab1;

namespace Lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<Book> Base = new List<Book>();
        public List<User> Users = new List<User>();
        string FileName = "";
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label6.Text = "Выбор файла";
            OpenFileDialog f = new OpenFileDialog
            {
                AddExtension = true,
                DefaultExt = "sbf",
                Filter = "Особые бинарные файлы|*.sbf",
                Title = "Выбор файла"
            };
            try
            {
                bool issues = false;
                f.ShowDialog();
                label6.Text = "Загрузка файла";
                BinaryReader r = new BinaryReader(File.OpenRead(f.FileName));
                int i = 0;
                Stopwatch s = new Stopwatch();
                s.Start();
                while (r.PeekChar() > -1 && i < 10000)
                {
                    try
                    {
                        Base.Add(new Book(r.ReadString(), r.ReadString(), r.ReadString(), r.ReadString(), r.ReadString(), r.ReadString(), r.ReadString(), r.ReadString(),
                            r.ReadInt32(), r.ReadInt32(), DateTime.Parse(r.ReadString()), DateTime.Parse(r.ReadString())));

                        //w.Write(Base[i].Name);
                        //w.Write(Base[i].Authors);
                        //w.Write(Base[i].Genre);
                        //w.Write(Base[i].PublishingHouse);
                        //w.Write(Base[i].Binding);
                        //w.Write(Base[i].Source);
                        //w.Write(Base[i].Comment);
                        //w.Write(Base[i].ISBN);
                        //w.Write(Base[i].Year);
                        //w.Write(Base[i].Mark);
                        //w.Write(Base[i].Got.ToShortDateString());
                        //w.Write(Base[i].Read.ToShortDateString());
                        i++;
                    }
                    catch
                    {
                        issues = true;
                    }
                }
                FileName = f.FileName;
                label4.Text = FileName + ", " + i.ToString() + " элементов";
                for (int j = 0; j < Users.Count; j++)
                {
                    Users[j].Base.Clear();
                    Users[j].Base.AddRange(Base.ToArray());
                    Users[j].B_Update();
                }
                label6.Text = "Загрузка завершена";
                s.Stop(); TimeSpan st = s.Elapsed;

                r.Close(); //w.Close();

                new Thread(() => File.Copy(f.FileName, f.FileName + ".temp"));

                if (!issues)
                    MessageBox.Show("Загрузка успешно завершена (" + st.TotalMilliseconds + "ms)");
                else MessageBox.Show("Загрузка завершена с ошибками (" + st.TotalMilliseconds + "ms)");
                label6.Text = "Ожидание";
            }
            catch (Exception E)
            {
                label6.Text = "Ошибка чтения";
                MessageBox.Show(E.Message);
            }
        }

        private void безопасноеЗакрытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                label6.Text = "Слияние файлов";
                label4.Text = "";
                label6.Update();
                File.Copy(FileName + ".temp", FileName, true);
                label6.Text = "Выход";
                this.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }


        private void новоеОкноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User u = new User();
            if (Base.Count != 0)
            {
                u.Base.Clear();
                u.Base.AddRange(Base.ToArray());
            }
            u.Parent = this;
            Users.Add(u);
            u.B_Update();
            u.Show();
        }

        public void Edit(Book value, int index)
        {
            Base[index] = value;
            U_UPdate();
        }
        public void Remove(int index)
        {
            Base.RemoveAt(index);
            U_UPdate();
        }
        public void Add(Book item)
        {
            Base.Add(item);
            U_UPdate();
        }
        public void U_UPdate()
        {
            for (int j = 0; j < Users.Count; j++)
            {
                Users[j].Base.Clear();
                Users[j].Base.AddRange(Base.ToArray());
                Users[j].B_Update();
            }
        }
    }
}
