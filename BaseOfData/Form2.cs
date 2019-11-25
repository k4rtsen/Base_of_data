using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BaseOfData
{
    public partial class Form2 : Form
    {
        MySqlConnection conn;
        List<string[]> faculty;
        List<string[]> dep;
        List<string[]> groupp;
        List<string[]> student;
        List<string[]> ap;
        List<string[]> subject;
        List<string[]> teacher;

        public Form2(MySqlConnection connection)
        {
            InitializeComponent();
            conn = connection;
            if (conn.State == ConnectionState.Open)
            {
                string name = "SELECT * FROM faculty;";
                MySqlCommand command = new MySqlCommand(name, conn);
                MySqlDataReader reader = command.ExecuteReader();
                faculty = new List<string[]>();
                while (reader.Read())
                {
                    faculty.Add(new string[2]);

                    faculty[faculty.Count - 1][0] = reader[0].ToString();
                    faculty[faculty.Count - 1][1] = reader[1].ToString();
                }
                reader.Close();

                name = "SELECT * FROM dep";
                command = new MySqlCommand(name, conn);
                reader = command.ExecuteReader();
                dep = new List<string[]>();
                while (reader.Read())
                {
                    dep.Add(new string[3]);

                    dep[dep.Count - 1][0] = reader[0].ToString();
                    dep[dep.Count - 1][1] = reader[1].ToString();
                    dep[dep.Count - 1][2] = reader[2].ToString();
                }
                reader.Close();

                name = "SELECT * FROM groupp";
                command = new MySqlCommand(name, conn);
                reader = command.ExecuteReader();
                groupp = new List<string[]>();
                while (reader.Read())
                {
                    groupp.Add(new string[3]);

                    groupp[groupp.Count - 1][0] = reader[0].ToString();
                    groupp[groupp.Count - 1][1] = reader[1].ToString();
                    groupp[groupp.Count - 1][2] = reader[2].ToString();
                }
                reader.Close();

                name = "SELECT * FROM student";
                command = new MySqlCommand(name, conn);
                reader = command.ExecuteReader();
                student = new List<string[]>();
                while (reader.Read())
                {
                    student.Add(new string[5]);

                    student[student.Count - 1][0] = reader[0].ToString();
                    student[student.Count - 1][1] = reader[1].ToString();
                    student[student.Count - 1][2] = reader[2].ToString();
                    student[student.Count - 1][3] = reader[3].ToString();
                    student[student.Count - 1][4] = reader[4].ToString();
                }
                reader.Close();

                name = "SELECT * FROM ap";
                command = new MySqlCommand(name, conn);
                reader = command.ExecuteReader();
                ap = new List<string[]>();
                while (reader.Read())
                {
                    ap.Add(new string[6]);

                    ap[ap.Count - 1][0] = reader[0].ToString();
                    ap[ap.Count - 1][1] = reader[1].ToString();
                    ap[ap.Count - 1][2] = reader[2].ToString();
                    ap[ap.Count - 1][3] = reader[3].ToString();
                    ap[ap.Count - 1][4] = reader[4].ToString();
                    ap[ap.Count - 1][5] = reader[5].ToString();
                }
                reader.Close();

                name = "SELECT * FROM subject";
                command = new MySqlCommand(name, conn);
                reader = command.ExecuteReader();
                subject = new List<string[]>();
                while (reader.Read())
                {
                    subject.Add(new string[6]);

                    subject[subject.Count - 1][0] = reader[0].ToString();
                    subject[subject.Count - 1][1] = reader[1].ToString();
                    subject[subject.Count - 1][2] = reader[2].ToString();
                    subject[subject.Count - 1][3] = reader[3].ToString();
                    subject[subject.Count - 1][4] = reader[4].ToString();
                    subject[subject.Count - 1][5] = reader[5].ToString();
                }
                reader.Close();

                name = "SELECT * FROM teacher";
                command = new MySqlCommand(name, conn);
                reader = command.ExecuteReader();
                teacher = new List<string[]>();
                while (reader.Read())
                {
                    teacher.Add(new string[5]);

                    teacher[teacher.Count - 1][0] = reader[0].ToString();
                    teacher[teacher.Count - 1][1] = reader[1].ToString();
                    teacher[teacher.Count - 1][2] = reader[2].ToString();
                    teacher[teacher.Count - 1][3] = reader[3].ToString();
                    teacher[teacher.Count - 1][4] = reader[4].ToString();
                }
                reader.Close();
            }
            else MessageBox.Show(
                        "Установите соединение с БД, иначе функции будут недоступны.",
                        "Проверьте соединение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                foreach (var pb in this.Controls.OfType<TextBox>())
                {
                    if (textBox1.Text == "") {
                        textBox1.ForeColor = Color.Red;
                        textBox1.Text = "Заполните все поля!";
                    }
                    if (textBox2.Text == "") {
                        textBox2.ForeColor = Color.Red;
                        textBox2.Text = "Заполните все поля!"; 
                    }
                    if (textBox3.Text == "") {
                        textBox3.ForeColor = Color.Red;
                        textBox3.Text = "Заполните все поля!"; 
                    }
                    if (textBox4.Text == "") {
                        textBox4.ForeColor = Color.Red;
                        textBox4.Text = "Заполните все поля!" + dep.Count; 
   
                    }
                    else
                    {
                        bool k;
                        foreach (var tb in groupp[1])
                        {
                            if (tb == textBox4.Text)
                            {
                                k = true;
                            }
                            else k = false;
                        }
                    }
                }
            }
            else MessageBox.Show(
                        "Установите соединение с БД, иначе функции будут недоступны.",
                        "Проверьте соединение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
        }
    }
}
