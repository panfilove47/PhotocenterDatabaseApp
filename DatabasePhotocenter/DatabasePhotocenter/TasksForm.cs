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

        private void task1Button_Click(object sender, EventArgs e)
        {
            fillTableInRequest("select f.idFilial, f.Photocenter_idPhotocenter, f.Name as FilialName, f.Adress as FilialAdress," +
                " f.EquipmentType, k.idKiosk, k.Filial_idFilial, k.Name as KioskName, k.Adress as KioskAdress, p.idPhotocenter," +
                " p.Name as PhotocenterName, p.Adress as PhotocenterAdress, (select count(*) from filial f2) as `Filial Count`," +
                " (select count(*) from kiosk k2) as `Kiosk Count`, (select count(*) from photocenter p2) as `Photocenter Count` from" +
                " filial f left join kiosk k on filial_idfilial = idfilial left join photocenter p on" +
                " photocenter_idphotocenter = idphotocenter;");
        }

        private void task2Button_Click(object sender, EventArgs e)
        {
            fillTableInRequest($"select idOrder, Client_idClient as idClient, Photocenter_idPhotocenter as idPhotocenter, idFilial," +
                $" Kiosk_idKiosk as idKiosk, OrderType, PhotoCount, PhotoCount, `Format`, PaperType, `Date`, PaidDevelopment, Urgency, Price," +
                $" (select count(idOrder) from `Order` where `Date` Between '{dateTimePicker1.Value.ToString("yyyyMMdd")}' and '{dateTimePicker2.Value.ToString("yyyyMMdd")}') as OrdersCount from `order`, filial, photocenter where" +
                $" Photocenter_idPhotocenter = idPhotocenter and Kiosk_Filial_idFilial = idFilial" +
                $" and `Date` Between '{dateTimePicker1.Value.ToString("yyyyMMdd")}' and '{dateTimePicker2.Value.ToString("yyyyMMdd")}'");
        }

        private void task3Button_Click(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                if (radioButton1.Checked)
                {
                    fillTableInRequest($"select idOrder, OrderType, PhotoCount, `Format`, PaperType, `Date`, PaidDevelopment, Urgency, Price, idFilial, idKiosk, (select count(*) from `order` o2 where o2.OrderType = 'Печать' and o2.Urgency = 'Срочный' and `Date` between '{dateTimePicker3.Value.ToString("yyyyMMdd")}' and '{dateTimePicker4.Value.ToString("yyyyMMdd")}') as CountOrders from filial f, kiosk k, `order` o where filial_idfilial = idfilial and Kiosk_Filial_idFilial = idFilial and OrderType = 'Печать' and Urgency = 'Срочный' and `Date` between '{dateTimePicker3.Value.ToString("yyyyMMdd")}' and '{dateTimePicker4.Value.ToString("yyyyMMdd")}';");
                }
                else if (radioButton4.Checked)
                {
                    fillTableInRequest($"select idOrder, OrderType, PhotoCount, `Format`, PaperType, `Date`, PaidDevelopment, Urgency, Price, idFilial, idKiosk, (select count(*) from `order` o2 where o2.OrderType = 'Проявка' and o2.Urgency = 'Срочный' and `Date` between '{dateTimePicker3.Value.ToString("yyyyMMdd")}' and '{dateTimePicker4.Value.ToString("yyyyMMdd")}') as CountOrders from filial f, kiosk k, `order` o where filial_idfilial = idfilial and Kiosk_Filial_idFilial = idFilial and OrderType = 'Проявка' and Urgency = 'Срочный' and `Date` between '{dateTimePicker3.Value.ToString("yyyyMMdd")}' and '{dateTimePicker4.Value.ToString("yyyyMMdd")}';");
                }
                else if (radioButton5.Checked)
                {
                    fillTableInRequest($"select idOrder, OrderType, PhotoCount, `Format`, PaperType, `Date`, PaidDevelopment, Urgency, Price, idFilial, idKiosk, (select count(*) from `order` o2 where o2.OrderType = 'Печать и проявка' and o2.Urgency = 'Срочный' and `Date` between '{dateTimePicker3.Value.ToString("yyyyMMdd")}' and '{dateTimePicker4.Value.ToString("yyyyMMdd")}') as CountOrders from filial f, kiosk k, `order` o where filial_idfilial = idfilial and Kiosk_Filial_idFilial = idFilial and OrderType = 'Печать и проявка' and Urgency = 'Срочный' and `Date` between '{dateTimePicker3.Value.ToString("yyyyMMdd")}' and '{dateTimePicker4.Value.ToString("yyyyMMdd")}';");
                }
            }
            else if (radioButton2.Checked)
            {
                if (radioButton1.Checked)
                {
                    fillTableInRequest($"select idOrder, OrderType, PhotoCount, `Format`, PaperType, `Date`, PaidDevelopment, Urgency, Price, idFilial, idKiosk, (select count(*) from `order` o2 where o2.OrderType = 'Печать' and o2.Urgency = 'Несрочный' and `Date` between '{dateTimePicker3.Value.ToString("yyyyMMdd")}' and '{dateTimePicker4.Value.ToString("yyyyMMdd")}') as CountOrders from filial f, kiosk k, `order` o where filial_idfilial = idfilial and Kiosk_Filial_idFilial = idFilial and OrderType = 'Печать' and Urgency = 'Несрочный' and `Date` between '{dateTimePicker3.Value.ToString("yyyyMMdd")}' and '{dateTimePicker4.Value.ToString("yyyyMMdd")}';");
                }
                else if (radioButton4.Checked)
                {
                    fillTableInRequest($"select idOrder, OrderType, PhotoCount, `Format`, PaperType, `Date`, PaidDevelopment, Urgency, Price, idFilial, idKiosk, (select count(*) from `order` o2 where o2.OrderType = 'Проявка' and o2.Urgency = 'Несрочный' and `Date` between '{dateTimePicker3.Value.ToString("yyyyMMdd")}' and '{dateTimePicker4.Value.ToString("yyyyMMdd")}') as CountOrders from filial f, kiosk k, `order` o where filial_idfilial = idfilial and Kiosk_Filial_idFilial = idFilial and OrderType = 'Проявка' and Urgency = 'Несрочный' and `Date` between '{dateTimePicker3.Value.ToString("yyyyMMdd")}' and '{dateTimePicker4.Value.ToString("yyyyMMdd")}';");
                }
                else if (radioButton5.Checked)
                {
                    fillTableInRequest($"select idOrder, OrderType, PhotoCount, `Format`, PaperType, `Date`, PaidDevelopment, Urgency, Price, idFilial, idKiosk, (select count(*) from `order` o2 where o2.OrderType = 'Печать и проявка' and o2.Urgency = 'Несрочный' and `Date` between '{dateTimePicker3.Value.ToString("yyyyMMdd")}' and '{dateTimePicker4.Value.ToString("yyyyMMdd")}') as CountOrders from filial f, kiosk k, `order` o where filial_idfilial = idfilial and Kiosk_Filial_idFilial = idFilial and OrderType = 'Печать и проявка' and Urgency = 'Несрочный' and `Date` between '{dateTimePicker3.Value.ToString("yyyyMMdd")}' and '{dateTimePicker4.Value.ToString("yyyyMMdd")}';");
                }
            }
        }

        private void task4Button_Click(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                if (radioButton8.Checked)
                {
                    fillTableInRequest($"select idOrder, OrderType, PhotoCount, `Format`, PaperType, `Date`, PaidDevelopment, Urgency, Price, idFilial, idKiosk, (select sum(Price) from `order` o2 where o2.OrderType = 'Печать' and o2.Urgency = 'Срочный' and `Date` between '{dateTimePicker6.Value.ToString("yyyyMMdd")}' and '{dateTimePicker5.Value.ToString("yyyyMMdd")}') as `Amount of proceeds` from filial f, kiosk k, `order` o where filial_idfilial = idfilial and Kiosk_Filial_idFilial = idFilial and OrderType = 'Печать' and Urgency = 'Срочный' and `Date` between '{dateTimePicker6.Value.ToString("yyyyMMdd")}' and '{dateTimePicker5.Value.ToString("yyyyMMdd")}';");
                }
                else if (radioButton7.Checked)
                {
                    fillTableInRequest($"select idOrder, OrderType, PhotoCount, `Format`, PaperType, `Date`, PaidDevelopment, Urgency, Price, idFilial, idKiosk, (select sum(Price) from `order` o2 where o2.OrderType = 'Проявка' and o2.Urgency = 'Срочный' and `Date` between '{dateTimePicker6.Value.ToString("yyyyMMdd")}' and '{dateTimePicker5.Value.ToString("yyyyMMdd")}') as `Amount of proceeds` from filial f, kiosk k, `order` o where filial_idfilial = idfilial and Kiosk_Filial_idFilial = idFilial and OrderType = 'Проявка' and Urgency = 'Срочный' and `Date` between '{dateTimePicker6.Value.ToString("yyyyMMdd")}' and '{dateTimePicker5.Value.ToString("yyyyMMdd")}';");
                }
                else if (radioButton6.Checked)
                {
                    fillTableInRequest($"select idOrder, OrderType, PhotoCount, `Format`, PaperType, `Date`, PaidDevelopment, Urgency, Price, idFilial, idKiosk, (select sum(Price) from `order` o2 where o2.OrderType = 'Печать и проявка' and o2.Urgency = 'Срочный' and `Date` between '{dateTimePicker6.Value.ToString("yyyyMMdd")}' and '{dateTimePicker5.Value.ToString("yyyyMMdd")}') as `Amount of proceeds` from filial f, kiosk k, `order` o where filial_idfilial = idfilial and Kiosk_Filial_idFilial = idFilial and OrderType = 'Печать и проявка' and Urgency = 'Срочный' and `Date` between '{dateTimePicker6.Value.ToString("yyyyMMdd")}' and '{dateTimePicker5.Value.ToString("yyyyMMdd")}';");
                }
            }
            else if (radioButton10.Checked)
            {
                if (radioButton8.Checked)
                {
                    fillTableInRequest($"select idOrder, OrderType, PhotoCount, `Format`, PaperType, `Date`, PaidDevelopment, Urgency, Price, idFilial, idKiosk, (select sum(Price) from `order` o2 where o2.OrderType = 'Печать' and o2.Urgency = 'Несрочный' and `Date` between '{dateTimePicker6.Value.ToString("yyyyMMdd")}' and '{dateTimePicker5.Value.ToString("yyyyMMdd")}') as `Amount of proceeds` from filial f, kiosk k, `order` o where filial_idfilial = idfilial and Kiosk_Filial_idFilial = idFilial and OrderType = 'Печать' and Urgency = 'Несрочный' and `Date` between '{dateTimePicker6.Value.ToString("yyyyMMdd")}' and '{dateTimePicker5.Value.ToString("yyyyMMdd")}';");
                }
                else if (radioButton7.Checked)
                {
                    fillTableInRequest($"select idOrder, OrderType, PhotoCount, `Format`, PaperType, `Date`, PaidDevelopment, Urgency, Price, idFilial, idKiosk, (select sum(Price) from `order` o2 where o2.OrderType = 'Проявка' and o2.Urgency = 'Несрочный' and `Date` between '{dateTimePicker6.Value.ToString("yyyyMMdd")}' and '{dateTimePicker5.Value.ToString("yyyyMMdd")}') as `Amount of proceeds` from filial f, kiosk k, `order` o where filial_idfilial = idfilial and Kiosk_Filial_idFilial = idFilial and OrderType = 'Проявка' and Urgency = 'Несрочный' and `Date` between '{dateTimePicker6.Value.ToString("yyyyMMdd")}' and '{dateTimePicker5.Value.ToString("yyyyMMdd")}';");
                }
                else if (radioButton6.Checked)
                {
                    fillTableInRequest($"select idOrder, OrderType, PhotoCount, `Format`, PaperType, `Date`, PaidDevelopment, Urgency, Price, idFilial, idKiosk, (select sum(Price) from `order` o2 where o2.OrderType = 'Печать и проявка' and o2.Urgency = 'Несрочный' and `Date` between '{dateTimePicker6.Value.ToString("yyyyMMdd")}' and '{dateTimePicker5.Value.ToString("yyyyMMdd")}') as `Amount of proceeds` from filial f, kiosk k, `order` o where filial_idfilial = idfilial and Kiosk_Filial_idFilial = idFilial and OrderType = 'Печать и проявка' and Urgency = 'Несрочный' and `Date` between '{dateTimePicker6.Value.ToString("yyyyMMdd")}' and '{dateTimePicker5.Value.ToString("yyyyMMdd")}';");
                }
            }
        }
    }
}
