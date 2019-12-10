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
    public partial class Form6 : Form
    {
        MySqlConnection conn;
        List<string[]> teacher;
        List<string> IDteacher;

        public Form6(MySqlConnection connection)
        {
            InitializeComponent();
            conn = connection;
            Change();
        }

        private void Change()
        {
            if (conn.State == ConnectionState.Open)
            {
                string name = "SELECT * FROM teacher ORDER BY surname_teacher";
                MySqlCommand command = new MySqlCommand(name, conn);
                MySqlDataReader reader = command.ExecuteReader();
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

                comboBox1.Text = "";
                comboBox1.Items.Clear();
                IDteacher = new List<string>();
                for (int i = 0; i < teacher.Count; i++)
                {
                    IDteacher.Add(teacher[i][0]);
                    comboBox1.Items.Add(teacher[i][2] + " " + teacher[i][1] + " " + teacher[i][3]);
                }

                textBox1.Text = "";
                comboBox2.Text = "";
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Фамилия");
                comboBox2.Items.Add("Имя");
                comboBox2.Items.Add("Отчество");
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
                    switch (comboBox2.SelectedIndex)
                    {
                        case 0:
                            change = "surname_teacher";
                            break;
                        case 1:
                            change = "name_teacher";
                            break;
                        case 2:
                            change = "patronymic";
                            break;
                    }

                    string name = "UPDATE `accounting`.`teacher` SET `" + change + "` = '" + textBox1.Text +
                        "' WHERE (`idTeacher` = '" + IDteacher[comboBox1.SelectedIndex].ToString() + "');";
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
