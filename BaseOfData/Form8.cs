using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BaseOfData
{
    public partial class Form8 : Form
    {
        private MySqlConnection conn;
        List<string[]> groupp;
        List<string[]> subject;
        List<string[]> data;
        int IDgroupp, IDsubject;

        public Form8(MySqlConnection connection)
        {
            InitializeComponent();
            conn = connection;
            AddComboBox();
        }

        private void AddComboBox()
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

                name = "SELECT * FROM subject ORDER BY name_subject";
                command = new MySqlCommand(name, conn);
                reader = command.ExecuteReader();
                subject = new List<string[]>();
                while (reader.Read())
                {
                    subject.Add(new string[5]);

                    subject[subject.Count - 1][0] = reader[0].ToString();
                    subject[subject.Count - 1][1] = reader[1].ToString();
                    subject[subject.Count - 1][2] = reader[2].ToString();
                    subject[subject.Count - 1][3] = reader[3].ToString();
                    subject[subject.Count - 1][4] = reader[4].ToString();
                }
                reader.Close();

                comboBox1.Text = "";
                comboBox1.Items.Clear();
                comboBox2.Text = "";
                comboBox2.Items.Clear();
                for (int i = 0; i < groupp.Count; i++) comboBox1.Items.Add(groupp[i][1]);
                for (int i = 0; i < subject.Count; i++) comboBox2.Items.Add(subject[i][1]);
            }
            else MessageBox.Show(
                        "Установите соединение с БД, иначе функции будут недоступны.",
                        "Проверьте соединение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
        }

        private void InitData()
        {
            string name = "SELECT name_group, surname_student, name_student, name_subject, type_mark, mark, day_ex, surname_teacher FROM accounting.ap " +
                "inner join accounting.teacher on teacher.idTeacher = ap.id_teacher " +
                "inner join accounting.student on student.idstudent = ap.id_student " +
                "inner join accounting.subject on subject.idsubject = ap.id_subject " +
                "inner join accounting.groupp on groupp.idGroup = student.id_group " +
                "WHERE groupp.idGroup = '" + IDgroupp.ToString() + "' AND subject.idsubject = '" + IDsubject.ToString() + "' " +
                "ORDER BY surname_student;";
            MySqlCommand command = new MySqlCommand(name, conn);
            MySqlDataReader reader = command.ExecuteReader();
            data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[8]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
                data[data.Count - 1][6] = reader[6].ToString();
                data[data.Count - 1][7] = reader[7].ToString();
            }
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open) {
               if (comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1) {

                    // Создаём экземпляр нашего приложения
                    Excel.Application excelApp = new Excel.Application();
                    // Создаём экземпляр рабочий книги Excel
                    Excel.Workbook workBook;
                    // Создаём экземпляр листа Excel
                    Excel.Worksheet workSheet;

                    workBook = excelApp.Workbooks.Add();
                    workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);
                    InitData();
                    workSheet.Cells[1, 1] = data[0][0];
                    for (int i = 0; i < data.Count; i++)
                        for (int j = 1; j < 8; j++) {
                            workSheet.Cells[i+2, j+1] = data[i][j];
                        }

                    // Открываем созданный excel-файл
                    excelApp.Visible = true;
                    excelApp.UserControl = true;
                }
               else MessageBox.Show(
                        "Выберите пункты 'Группа' и 'Дисциплина'.",
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < subject.Count; i++) {
                if (comboBox2.SelectedItem.ToString() == subject[i][1]) {
                    IDsubject = Convert.ToInt32(subject[i][0]);
                }
            }
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < groupp.Count; i++) {
                if (comboBox1.SelectedItem.ToString() == groupp[i][1]) {
                    IDgroupp = Convert.ToInt32(groupp[i][0]);
                }
            }
        }
    }
}
