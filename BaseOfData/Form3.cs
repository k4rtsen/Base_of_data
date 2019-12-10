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
    public partial class Form3 : Form
    {
        MySqlConnection conn;
        List<string[]> student;
        List<string> IDstudents;

        public Form3(MySqlConnection connection)
        {
            InitializeComponent();
            conn = connection;
            Change();
        }

        private void Change()
        {
            if (conn.State == ConnectionState.Open) {
                string name = "SELECT * FROM student ORDER BY surname_student";
                MySqlCommand command = new MySqlCommand(name, conn);
                MySqlDataReader reader = command.ExecuteReader();
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

                comboBox1.Text = "";
                comboBox1.Items.Clear();
                IDstudents = new List<string>();
                for (int i = 0; i < student.Count; i++) {
                    IDstudents.Add(student[i][0]);
                    comboBox1.Items.Add(student[i][2] + " " + student[i][1] + " " + student[i][3]);
                }
                
                textBox1.Text = "";
                comboBox2.Text = "";
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Фамилия");
                comboBox2.Items.Add("Имя");
                comboBox2.Items.Add("Дата рождения");
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
                if (comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1 && textBox1.Text != "")
                {
                    string change = "";
                    switch (comboBox2.SelectedIndex) {
                        case 0:
                            change = "surname_student";
                            break;
                        case 1:
                            change = "name_student";
                            break;
                        case 2:
                            change = "date_of_berth";
                            break;
                    }

                    string name = "UPDATE `accounting`.`student` SET `" + change + "` = '" + textBox1.Text +
                        "' WHERE (`idstudent` = '" + IDstudents[comboBox1.SelectedIndex].ToString() + "');";
                    MySqlCommand command = new MySqlCommand(name, conn);
                    command.ExecuteNonQuery();
                    Change();
                }
                else MessageBox.Show(
                        "Заполните все поля.",
                        "",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
            else MessageBox.Show(
                        "Установите соединение с БД, иначе функции будут недоступны.",
                        "Проверьте соединение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
        }
    }
}