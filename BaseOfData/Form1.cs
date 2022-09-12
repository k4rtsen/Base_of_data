using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

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
                    "Закрыть приложение?",
                    "Выход",
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

        private void студентToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                string name = "SELECT surname_student, name_student, date_of_berth, name_group FROM " +
                    "student INNER JOIN accounting.groupp on groupp.idGroup = student.id_group ORDER BY surname_student;";
                MySqlCommand command = new MySqlCommand(name, conn);
                MySqlDataReader reader = command.ExecuteReader();

                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[4]);

                    data[data.Count - 1][0] = reader[0].ToString();
                    data[data.Count - 1][1] = reader[1].ToString();
                    data[data.Count - 1][2] = reader[2].ToString();
                    data[data.Count - 1][3] = reader[3].ToString();
                }
                reader.Close();
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.ColumnCount = data.Count;
                dataGridView1.Columns[0].HeaderText = "Имя";
                dataGridView1.Columns[1].HeaderText = "Фамилия";
                dataGridView1.Columns[2].HeaderText = "Дата рождения";
                dataGridView1.Columns[3].HeaderText = "Группа";
                foreach (string[] s in data) dataGridView1.Rows.Add(s);
            }
            else MessageBox.Show(
                        "Соединение не установленно.",
                        "Проверьте соединение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        }

        private void преподавателиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                string name = "SELECT surname_teacher, name_teacher, patronymic, name_dep FROM " +
                    "teacher inner join accounting.dep on dep.id_dep = teacher.id_dep ORDER BY surname_teacher;";
                MySqlCommand command = new MySqlCommand(name, conn);
                MySqlDataReader reader = command.ExecuteReader();

                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[4]);

                    data[data.Count - 1][0] = reader[0].ToString();
                    data[data.Count - 1][1] = reader[1].ToString();
                    data[data.Count - 1][2] = reader[2].ToString();
                    data[data.Count - 1][3] = reader[3].ToString();
                }
                reader.Close();
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.ColumnCount = data.Count + 1;
                dataGridView1.Columns[0].HeaderText = "Фамилия";
                dataGridView1.Columns[1].HeaderText = "Имя";
                dataGridView1.Columns[2].HeaderText = "Отчество";
                dataGridView1.Columns[3].HeaderText = "Кафедра";
                foreach (string[] s in data) dataGridView1.Rows.Add(s);
            }
            else MessageBox.Show(
                        "Соединение не установленно.",
                        "Проверьте соединение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        }

        private void дисциплинаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                string name = "SELECT name_subject, type_mark, count, surname_teacher  FROM " +
                    "subject inner join accounting.teacher on teacher.idTeacher = subject.id_teacher ORDER BY name_subject;";
                MySqlCommand command = new MySqlCommand(name, conn);
                MySqlDataReader reader = command.ExecuteReader();

                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[4]);

                    data[data.Count - 1][0] = reader[0].ToString();
                    data[data.Count - 1][1] = reader[1].ToString();
                    data[data.Count - 1][2] = reader[2].ToString();
                    data[data.Count - 1][3] = reader[3].ToString();
                }
                reader.Close();
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.ColumnCount = data.Count + 2;
                dataGridView1.Columns[0].HeaderText = "Дисциплина";
                dataGridView1.Columns[1].HeaderText = "Тип отестации";
                dataGridView1.Columns[2].HeaderText = "Количество часов";
                dataGridView1.Columns[3].HeaderText = "Семестр";
                dataGridView1.Columns[4].HeaderText = "Преподаватель";
                foreach (string[] s in data) dataGridView1.Rows.Add(s);
            }
            else MessageBox.Show(
                        "Соединение не установленно.",
                        "Проверьте соединение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        }

        private void успеваемостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                string name = "SELECT surname_student, name_student, name_subject, mark, day_ex, surname_teacher FROM " +
                    "ap inner join accounting.teacher on teacher.idTeacher = ap.id_teacher " +
                    "inner join accounting.student on student.idstudent = ap.id_student " +
                    "inner join accounting.subject on subject.idsubject = ap.id_subject ORDER BY surname_student;";
                MySqlCommand command = new MySqlCommand(name, conn);
                MySqlDataReader reader = command.ExecuteReader();

                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[5]);

                    data[data.Count - 1][0] = reader[0].ToString() + " " + reader[1].ToString();
                    data[data.Count - 1][1] = reader[2].ToString();
                    data[data.Count - 1][2] = reader[3].ToString();
                    data[data.Count - 1][3] = reader[4].ToString();
                    data[data.Count - 1][4] = reader[5].ToString();
                }
                reader.Close();
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.ColumnCount = data.Count + 2;
                dataGridView1.Columns[0].HeaderText = "Студент";
                dataGridView1.Columns[1].HeaderText = "Дисциплина";
                dataGridView1.Columns[2].HeaderText = "Оценка";
                dataGridView1.Columns[3].HeaderText = "Дата экзамена";
                dataGridView1.Columns[4].HeaderText = "Преподаватель";
                foreach (string[] s in data) dataGridView1.Rows.Add(s);
            }
            else MessageBox.Show(
                        "Соединение не установленно.",
                        "Проверьте соединение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        }

        private void факультетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                string name = "SELECT * FROM dep";
                MySqlCommand command = new MySqlCommand(name, conn);
                MySqlDataReader reader = command.ExecuteReader();

                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[3]);

                    data[data.Count - 1][0] = reader[0].ToString();
                    data[data.Count - 1][1] = reader[1].ToString();
                    data[data.Count - 1][2] = reader[2].ToString();
                }
                reader.Close();

                name = "SELECT * FROM faculty ORDER BY name_faculty";
                command = new MySqlCommand(name, conn);
                reader = command.ExecuteReader();

                List<string[]> data1 = new List<string[]>();
                while (reader.Read())
                {
                    data1.Add(new string[2]);

                    data1[data1.Count - 1][0] = reader[0].ToString();
                    data1[data1.Count - 1][1] = reader[1].ToString();
                }
                reader.Close();

                dataGridView1.Rows.Clear();
                dataGridView1.ColumnCount = 2;
                dataGridView1.Columns[0].HeaderText = "Факультет";
                dataGridView1.Columns[1].HeaderText = "Кафедры";
                for (int i = 0; i < data1.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = data1[i][1];
                    for (int j = 0; j < data.Count; j++)
                    {
                        if (data1[i][0] == data[j][2])
                            dataGridView1.Rows[i].Cells[1].Value += data[j][1] + " ";
                    }
                }
            }
            else MessageBox.Show(
                        "Соединение не установленно.",
                        "Проверьте соединение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        }

        private void кафедраToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                string name = "SELECT * FROM groupp";
                MySqlCommand command = new MySqlCommand(name, conn);
                MySqlDataReader reader = command.ExecuteReader();

                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[3]);

                    data[data.Count - 1][0] = reader[0].ToString();
                    data[data.Count - 1][1] = reader[1].ToString();
                    data[data.Count - 1][2] = reader[2].ToString();
                }
                reader.Close();

                name = "SELECT * FROM dep ORDER BY name_dep";
                command = new MySqlCommand(name, conn);
                reader = command.ExecuteReader();

                List<string[]> data1 = new List<string[]>();
                while (reader.Read())
                {
                    data1.Add(new string[3]);

                    data1[data1.Count - 1][0] = reader[0].ToString();
                    data1[data1.Count - 1][1] = reader[1].ToString();
                    data1[data1.Count - 1][2] = reader[2].ToString();
                }
                reader.Close();

                dataGridView1.Rows.Clear();
                dataGridView1.ColumnCount = 2;
                dataGridView1.Columns[0].HeaderText = "Кафедра";
                dataGridView1.Columns[1].HeaderText = "Группа";
                for (int i = 0; i < data1.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = data1[i][1];
                    for (int j = 0; j < data.Count; j++)
                    {
                        if (data1[i][0] == data[j][2])
                            dataGridView1.Rows[i].Cells[1].Value += data[j][1] + " ";
                    }
                }
            }
            else MessageBox.Show(
                        "Соединение не установленно.",
                        "Проверьте соединение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        }

        private void группаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                string name = "SELECT * FROM student ORDER BY surname_student";
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

                name = "SELECT * FROM groupp name_group";
                command = new MySqlCommand(name, conn);
                reader = command.ExecuteReader();

                List<string[]> data1 = new List<string[]>();
                while (reader.Read())
                {
                    data1.Add(new string[3]);

                    data1[data1.Count - 1][0] = reader[0].ToString();
                    data1[data1.Count - 1][1] = reader[1].ToString();
                    data1[data1.Count - 1][2] = reader[2].ToString();
                }
                reader.Close();

                dataGridView1.Rows.Clear();
                this.dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView1.ColumnCount = 2;
                dataGridView1.Columns[0].HeaderText = "Группа";
                dataGridView1.Columns[1].HeaderText = "Студенты";
                for (int i = 0; i < data1.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = data1[i][1];
                    for (int j = 0; j < data.Count; j++)
                    {
                        if (data1[i][0] == data[j][4])
                            dataGridView1.Rows[i].Cells[1].Value += data[j][2] + " " + data[j][1] + "; ";
                    }
                }
            }
            else MessageBox.Show(
                        "Соединение не установленно.",
                        "Проверьте соединение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        }

        private void добавитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(conn);
            form2.Show();
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(conn);
            form3.Show();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(conn);
            form4.Show();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(conn);
            form5.Show();
        }

        private void изменитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6(conn);
            form6.Show();
        }

        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7(conn);
            form7.Show();
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8(conn);
            form8.Show();
        }
    }
}