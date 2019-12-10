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
    public partial class Form5 : Form
    {
        private MySqlConnection conn;
        List<string[]> dep;

        public Form5(MySqlConnection connection)
        {
            InitializeComponent();
            conn = connection;
            InitDep();
        }

        private void InitDep()
        {
            if (conn.State == ConnectionState.Open)
            {
                string name = "SELECT * FROM dep ORDER BY name_dep";
                MySqlCommand command = new MySqlCommand(name, conn);
                MySqlDataReader reader = command.ExecuteReader();
                dep = new List<string[]>();
                while (reader.Read())
                {
                    dep.Add(new string[3]);

                    dep[dep.Count - 1][0] = reader[0].ToString();
                    dep[dep.Count - 1][1] = reader[1].ToString();
                    dep[dep.Count - 1][2] = reader[2].ToString();
                }
                reader.Close();

                comboBox1.Text = "";
                comboBox1.Items.Clear();
                for (int i = 0; i < dep.Count; i++) comboBox1.Items.Add(dep[i][1]);
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
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && comboBox1.Text != "")
                {
                    for (int i = 0; i < dep.Count; i++)
                        if (dep[i][1] == comboBox1.Text)
                        {
                            string name = "INSERT INTO `accounting`.`teacher` (`name_teacher`, `surname_teacher`, " +
                                "`patronymic`, `id_dep`) VALUES " +
                                "('" + textBox2.Text + "', '" + textBox1.Text + "', '" + textBox3.Text + "'," +
                                " '" + dep[i][0] + "');";
                            MySqlCommand command = new MySqlCommand(name, conn);
                            command.ExecuteNonQuery();
                            InitDep();
                            break;
                        }
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
