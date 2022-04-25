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
    public partial class Form1 : Form
    {
        string connect = "Server=localhost;Database=PhotocenterDB;Uid=root;password=Vbifcfif;charset=utf8";
        public MySqlConnection MyConn; // Коннектор к БД
        public MySqlCommand MyComm; // Для отправки запроса
        public MySqlDataAdapter adapter; // Предоставляет выборку из запроса.
        private BindingSource bindingSource;
        DataTable dt;

        public Form1()
        {
            InitializeComponent();
            MyConn = new MySqlConnection(connect);
            MyConn.Open();
        }

        ~Form1()
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
        private void fillTableWithChanges(string querry)
        {
            try
            {
                adapter = new MySqlDataAdapter(querry, connect); // Метод для отправки запроса из 1 таблицы. (С возможностью изменений)
                MySqlCommandBuilder myCommandBuilder = new MySqlCommandBuilder(adapter as MySqlDataAdapter);
                adapter.InsertCommand = myCommandBuilder.GetInsertCommand();
                adapter.UpdateCommand = myCommandBuilder.GetUpdateCommand();
                adapter.DeleteCommand = myCommandBuilder.GetDeleteCommand();
                dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void photocenterButton_Click(object sender, EventArgs e)
        {
            fillTableWithChanges("select * from photocenter");
        }

        private void supplyButton_Click(object sender, EventArgs e)
        {
            fillTableWithChanges("select * from suplpy");
        }

        private void positionButton_Click(object sender, EventArgs e)
        {
            fillTableWithChanges("select * from position");
        }

        private void clientButton_Click(object sender, EventArgs e)
        {
            fillTableWithChanges("select * from client");
        }

        private void saleGoogsButton_Click(object sender, EventArgs e)
        {
            fillTableWithChanges("select * from SaleGoods");
        }

        private void orderButton_Click(object sender, EventArgs e)
        {
            fillTableWithChanges("select * from `Order`");
        }

        private void serviceButton_Click(object sender, EventArgs e)
        {
            fillTableWithChanges("select * from service");
        }

        private void shopButton_Click(object sender, EventArgs e)
        {
            fillTableWithChanges("select * from shop");
        }

        private void kioskButton_Click(object sender, EventArgs e)
        {
            fillTableWithChanges("select * from kiosk");
        }

        private void filialButton_Click(object sender, EventArgs e)
        {
            fillTableWithChanges("select * from filial");
        }

        private void supplierButton_Click(object sender, EventArgs e)
        {
            fillTableWithChanges("select * from supplier");
        }
    }
}
