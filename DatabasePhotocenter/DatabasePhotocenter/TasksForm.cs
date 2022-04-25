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

namespace DatabasePhotocenter
{
    public partial class TasksForm : Form
    {
        string connect = "Server=localhost;Database=PhotocenterDB;Uid=root;password=Vbifcfif;charset=utf8";
        public MySqlConnection MyConn; // Коннектор к БД
        public MySqlCommand MyComm; // Для отправки запроса
        public MySqlDataAdapter adapter; // Предоставляет выборку из запроса.
        private BindingSource bindingSource;
        DataTable dt;
        public TasksForm()
        {
            InitializeComponent();
            MyConn = new MySqlConnection(connect);
            MyConn.Open();
        }
        ~TasksForm()
        {
            MyConn.Close();
        }

        private void fillTableInRequest(string querry) // Метод для отправки запроса и заполнения таблицы. (Без изменений)
        {
            try
            {
                adapter = new MySqlDataAdapter(querry, connect); // Предоставляет выборку из запроса.
                DataTable table = new DataTable(); // Временная таблица для заполнения нашей.
                adapter.Fill(table); // Заполнение временной таблицы.
                bindingSource = new BindingSource();
                bindingSource.DataSource = table;
                dataGridView1.DataSource = bindingSource; // Перенесение временной таблицы в основную.
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void TasksForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Owner.Show();
        }

    }
}
