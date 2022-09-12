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
        private MySqlConnection conn;
        List<string[]> groupp;

        public Form2(MySqlConnection connection)
        {
            InitializeComponent();
            conn = connection;
            InitGroupp();
        }

        private void InitGroupp() {
            if (conn.State == ConnectionState.Open)
            {
                string name = "SELECT * FROM groupp ORDER BY name_group";
                MySqlCommand command = new MySqlCommand(name, conn);
                MySqlDataReader reader = command.ExecuteReader();
                groupp = new List<string[]>();
                while (reader.Read())
                {
                    groupp.Add(new string[3]);

                    groupp[groupp.Count - 1][0] = reader[0].ToString();
                    groupp[groupp.Count - 1][1] = reader[1].ToString();
                    groupp[groupp.Count - 1][2] = reader[2].ToString();
                }
                reader.Close();

                comboBox1.Text = "";
                comboBox1.Items.Clear();
                for (int i = 0; i < groupp.Count; i++) comboBox1.Items.Add(groupp[i][1]);
            }
            else MessageBox.Show(
                        "Установите соединение с БД, иначе функции будут недоступны.",
                        "Проверьте соединение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
        }

        private void button1_Click(object sender, EventArgs e) {
            if (conn.State == ConnectionState.Open) {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && comboBox1.Text != "") {
                    for (int i = 0; i < groupp.Count; i++)
                        if (groupp[i][1] == comboBox1.Text) {
                            string name = "INSERT INTO `accounting`.`student` (`name_student`, `sur" +
                                "name_student`, `date_of_berth`, `id_group`) VALUES " +
                                "('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "'," +
                                " '" + groupp[i][0] + "');";
                            MySqlCommand command = new MySqlCommand(name, conn);
                            command.ExecuteNonQuery();
                            InitGroupp();
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