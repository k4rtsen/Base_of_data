using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;

namespace BaseOfData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string connStr = "server=localhost;user=root;database=accounting;password=root";
        MySqlConnection conn = new MySqlConnection(connStr);

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                    "Хочешь выйти?",
                    "Exit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information) == DialogResult.Yes) Application.Exit();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MessageBox.Show(
                    "Соединение успешно установленно.",
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                if (conn.State == ConnectionState.Open)
                    MessageBox.Show(
                        "Соединение уже установленно.",
                        "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                else
                    MessageBox.Show(
                        "Соединение не установленно.",
                        "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                MessageBox.Show(
                    "Соединение успешно отключено.",
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                        "Отключить соединение не удалось.",
                        "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                string name = "SELECT * FROM student;";
                MySqlCommand command = new MySqlCommand(name, conn);
                MySqlDataReader reader = command.ExecuteReader();

                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[5]);

                    data[data.Count - 1][0] = reader[0].ToString();
                    data[data.Count - 1][1] = reader[1].ToString();
                    data[data.Count - 1][2] = reader[2].ToString();
                    data[data.Count - 1][3] = reader[3].ToString();
                    data[data.Count - 1][4] = reader[4].ToString();
                }
                reader.Close();
                foreach (string[] s in data) dataGridView1.Rows.Add(s);
            }
            else MessageBox.Show(
                        "Соединение не установленно.",
                        "Проверьте соединение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        }
    }
}
