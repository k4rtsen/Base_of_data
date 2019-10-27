using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace BaseOfData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string connStr = "server=localhost;user=root;database=accounting;password=root";
            MySqlConnection conn = new MySqlConnection(connStr);

            conn.Open();

            string str = "SELECT name_student FROM student WHERE idstudent = 2";
            MySqlCommand command = new MySqlCommand(str, conn);
            string student = command.ExecuteScalar().ToString();

            str = "SELECT name_student, surname_student FROM accounting.student WHERE id_group = 1 or id_group = 2;";
            command = new MySqlCommand(str, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
            }
            reader.Close();
            conn.Close();
        }
    }
}
