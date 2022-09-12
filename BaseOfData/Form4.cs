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
    public partial class Form4 : Form
    {
        MySqlConnection conn;
        List<string[]> faculty;
        List<string[]> dep;
        List<string[]> groupp;
        List<string[]> student;
        List<string> IDstudents = new List<string>();

        public Form4(MySqlConnection connection)
        {
            InitializeComponent();
            conn = connection;
            copyBD();
        }

        private void copyBD()
        {
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

                name = "SELECT * FROM dep ORDER BY name_dep";
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

                name = "SELECT * FROM faculty ORDER BY name_faculty";
                command = new MySqlCommand(name, conn);
                reader = command.ExecuteReader();
                faculty = new List<string[]>();
                while (reader.Read())
                {
                    faculty.Add(new string[2]);

                    faculty[faculty.Count - 1][0] = reader[0].ToString();
                    faculty[faculty.Count - 1][1] = reader[1].ToString();
                }
                reader.Close();

                name = "SELECT * FROM student ORDER BY surname_student";
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

                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                comboBox4.Items.Clear();
                for (int i = 0; i < faculty.Count; i++) comboBox1.Items.Add(faculty[i][1]);
                for (int i = 0; i < dep.Count; i++) comboBox2.Items.Add(dep[i][1]);
                for (int i = 0; i < groupp.Count; i++) comboBox3.Items.Add(groupp[i][1]);
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
                if (comboBox4.SelectedIndex != -1)
                {
                    for (int i = 0; i < IDstudents.Count; i++)
                        if (comboBox4.SelectedIndex == i)
                        {
                            string name = "DELETE FROM `accounting`.`student` WHERE (`idstudent` = '" + IDstudents[i] + "');";
                            MySqlCommand command = new MySqlCommand(name, conn);
                            command.ExecuteNonQuery();
                            copyBD();
                            break;
                        }
                }
                else MessageBox.Show(
                        "Выберите студента, которого нужно удалить из базы данных.",
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            for (int i = 0; i < faculty.Count; i++)
                if (comboBox1.SelectedItem.ToString() == faculty[i][1])
                    for (int j = 0; j < dep.Count; j++)
                        if (faculty[i][0] == dep[j][2]) comboBox2.Items.Add(dep[j][1]);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            for (int i = 0; i < dep.Count; i++)
                if (comboBox2.SelectedItem.ToString() == dep[i][1])
                    for (int j = 0; j < groupp.Count; j++)
                        if (dep[i][0] == groupp[j][2]) comboBox3.Items.Add(groupp[j][1]);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Text = "";
            comboBox4.Items.Clear();
            for (int i = 0; i < groupp.Count; i++)
                if (comboBox3.SelectedItem.ToString() == groupp[i][1])
                    for (int j = 0; j < student.Count; j++)
                        if (groupp[i][0] == student[j][4]) {
                            IDstudents.Add(student[j][0]);
                            comboBox4.Items.Add(student[j][2] + " " + student[j][1] + " " + student[j][3]);
                        }
        }
    }
}