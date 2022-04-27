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
        public MySqlCommand cmd; // Для отправки запроса
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

        private void task5Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton11.Checked)
                {
                    if (radioButton13.Checked)
                    {
                        fillTableInRequest($"select sum(PhotoCount) as `Photo count` from `order` left join kiosk on kiosk_idKiosk = idKiosk left join filial on filial_idfilial = idfilial left join photocenter on photocenter_idphotocenter = idphotocenter where Urgency = 'Срочный' and idPhotocenter = {int.Parse(textBox1.Text)} and `Date` between '{dateTimePicker8.Value.ToString("yyyyMMdd")}' and '{dateTimePicker7.Value.ToString("yyyyMMdd")}' and OrderType in ('Печать', 'Печать и проявка');");
                    }
                    else if (radioButton14.Checked)
                    {
                        fillTableInRequest($"select sum(PhotoCount) as `Photo count` from `order` left join kiosk on kiosk_idKiosk = idKiosk left join filial on filial_idfilial = idfilial left join photocenter on photocenter_idphotocenter = idphotocenter where Urgency = 'Срочный' and idFilial = {int.Parse(textBox1.Text)} and `Date` between '{dateTimePicker8.Value.ToString("yyyyMMdd")}' and '{dateTimePicker7.Value.ToString("yyyyMMdd")}' and OrderType in ('Печать', 'Печать и проявка');");
                    }
                    else if (radioButton15.Checked)
                    {
                        fillTableInRequest($"select sum(PhotoCount) as `Photo count` from `order` left join kiosk on kiosk_idKiosk = idKiosk left join filial on filial_idfilial = idfilial left join photocenter on photocenter_idphotocenter = idphotocenter where Urgency = 'Срочный' and idKiosk = {int.Parse(textBox1.Text)} and `Date` between '{dateTimePicker8.Value.ToString("yyyyMMdd")}' and '{dateTimePicker7.Value.ToString("yyyyMMdd")}' and OrderType in ('Печать', 'Печать и проявка');");
                    }
                }
                else if (radioButton12.Checked)
                {
                    if (radioButton13.Checked)
                    {
                        fillTableInRequest($"select sum(PhotoCount) as `Photo count` from `order` left join kiosk on kiosk_idKiosk = idKiosk left join filial on filial_idfilial = idfilial left join photocenter on photocenter_idphotocenter = idphotocenter where Urgency = 'Несрочный' and idPhotocenter = {int.Parse(textBox1.Text)} and `Date` between '{dateTimePicker8.Value.ToString("yyyyMMdd")}' and '{dateTimePicker7.Value.ToString("yyyyMMdd")}' and OrderType in ('Печать', 'Печать и проявка');");
                    }
                    else if (radioButton14.Checked)
                    {
                        fillTableInRequest($"select sum(PhotoCount) as `Photo count` from `order` left join kiosk on kiosk_idKiosk = idKiosk left join filial on filial_idfilial = idfilial left join photocenter on photocenter_idphotocenter = idphotocenter where Urgency = 'Несрочный' and idFilial = {int.Parse(textBox1.Text)} and `Date` between '{dateTimePicker8.Value.ToString("yyyyMMdd")}' and '{dateTimePicker7.Value.ToString("yyyyMMdd")}' and OrderType in ('Печать', 'Печать и проявка');");
                    }
                    else if (radioButton15.Checked)
                    {
                        fillTableInRequest($"select sum(PhotoCount) as `Photo count` from `order` left join kiosk on kiosk_idKiosk = idKiosk left join filial on filial_idfilial = idfilial left join photocenter on photocenter_idphotocenter = idphotocenter where Urgency = 'Несрочный' and idKiosk = {int.Parse(textBox1.Text)} and `Date` between '{dateTimePicker8.Value.ToString("yyyyMMdd")}' and '{dateTimePicker7.Value.ToString("yyyyMMdd")}' and OrderType in ('Печать', 'Печать и проявка');");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Введите id в текстовое поле");
            }
        }

        private void task6Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton19.Checked)
                {
                    if (radioButton16.Checked)
                    {
                        fillTableInRequest($"select sum(PhotoCount) as `Photo count` from `order` left join kiosk on kiosk_idKiosk = idKiosk left join filial on filial_idfilial = idfilial left join photocenter on photocenter_idphotocenter = idphotocenter where Urgency = 'Срочный' and idPhotocenter = {int.Parse(textBox2.Text)} and `Date` between '{dateTimePicker10.Value.ToString("yyyyMMdd")}' and '{dateTimePicker9.Value.ToString("yyyyMMdd")}' and OrderType in ('Проявка', 'Печать и проявка');");
                    }
                    else if (radioButton18.Checked)
                    {
                        fillTableInRequest($"select sum(PhotoCount) as `Photo count` from `order` left join kiosk on kiosk_idKiosk = idKiosk left join filial on filial_idfilial = idfilial left join photocenter on photocenter_idphotocenter = idphotocenter where Urgency = 'Срочный' and idFilial = {int.Parse(textBox2.Text)} and `Date` between '{dateTimePicker10.Value.ToString("yyyyMMdd")}' and '{dateTimePicker9.Value.ToString("yyyyMMdd")}' and OrderType in ('Проявка', 'Печать и проявка');");
                    }
                    else if (radioButton17.Checked)
                    {
                        fillTableInRequest($"select sum(PhotoCount) as `Photo count` from `order` left join kiosk on kiosk_idKiosk = idKiosk left join filial on filial_idfilial = idfilial left join photocenter on photocenter_idphotocenter = idphotocenter where Urgency = 'Срочный' and idKiosk = {int.Parse(textBox2.Text)} and `Date` between '{dateTimePicker10.Value.ToString("yyyyMMdd")}' and '{dateTimePicker9.Value.ToString("yyyyMMdd")}' and OrderType in ('Проявка', 'Печать и проявка');");
                    }
                }
                else if (radioButton20.Checked)
                {
                    if (radioButton16.Checked)
                    {
                        fillTableInRequest($"select sum(PhotoCount) as `Photo count` from `order` left join kiosk on kiosk_idKiosk = idKiosk left join filial on filial_idfilial = idfilial left join photocenter on photocenter_idphotocenter = idphotocenter where Urgency = 'Несрочный' and idPhotocenter = {int.Parse(textBox2.Text)} and `Date` between '{dateTimePicker10.Value.ToString("yyyyMMdd")}' and '{dateTimePicker9.Value.ToString("yyyyMMdd")}' and OrderType in ('Проявка', 'Печать и проявка');");
                    }
                    else if (radioButton18.Checked)
                    {
                        fillTableInRequest($"select sum(PhotoCount) as `Photo count` from `order` left join kiosk on kiosk_idKiosk = idKiosk left join filial on filial_idfilial = idfilial left join photocenter on photocenter_idphotocenter = idphotocenter where Urgency = 'Несрочный' and idFilial = {int.Parse(textBox2.Text)} and `Date` between '{dateTimePicker10.Value.ToString("yyyyMMdd")}' and '{dateTimePicker9.Value.ToString("yyyyMMdd")}' and OrderType in ('Проявка', 'Печать и проявка');");
                    }
                    else if (radioButton17.Checked)
                    {
                        fillTableInRequest($"select sum(PhotoCount) as `Photo count` from `order` left join kiosk on kiosk_idKiosk = idKiosk left join filial on filial_idfilial = idfilial left join photocenter on photocenter_idphotocenter = idphotocenter where Urgency = 'Несрочный' and idKiosk = {int.Parse(textBox2.Text)} and `Date` between '{dateTimePicker10.Value.ToString("yyyyMMdd")}' and '{dateTimePicker9.Value.ToString("yyyyMMdd")}' and OrderType in ('Проявка', 'Печать и проявка');");
                    }
                }
            }
            catch
            {

                MessageBox.Show("Введите id в текстовое поле");
            }
        }

        private void task7_1Button_Click(object sender, EventArgs e)
        {
            fillTableInRequest("select * from supplier");
        }

        private void task7_2Button_Click(object sender, EventArgs e)
        {
            try
            {
                fillTableInRequest($"select idSupplier, sl.Name, Adress, Sells, Count, `Date` from supplier sl left join suplpy sp" +
                    $" on supplier_idsupplier = idsupplier where `date`" +
                    $" between '{dateTimePicker11.Value.ToString("yyyyMMdd")}' and '{dateTimePicker12.Value.ToString("yyyyMMdd")}'" +
                    $" and `Count` <= {int.Parse(textBox4.Text)} and Sells = '{textBox3.Text}'");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void task8_1Button_Click(object sender, EventArgs e)
        {
            fillTableInRequest("select * from client");
        }

        private void task8_2Button_Click(object sender, EventArgs e)
        {
            fillTableInRequest($"select idClient, FIO, `Status`, DiscountCard, idOrder from `client` left join `order` on client_idclient = idclient where DiscountCard = 1 and Kiosk_Filial_idFilial = {textBox5.Text} and PhotoCount <= {textBox6.Text} ");
        }

        private void task9_1Button_Click(object sender, EventArgs e)
        {
            fillTableInRequest("select sum(Price) as Proceeds from salegoods;");
        }

        private void task9_2Button_Click(object sender, EventArgs e)
        {
            fillTableInRequest($"select sum(Price) as Proceeds from salegoods where Kiosk_Filial_idFilial = {int.Parse(textBox7.Text)} and `Date` between '{dateTimePicker13.Value.ToString("yyyyMMdd")}' and '{dateTimePicker14.Value.ToString("yyyyMMdd")}';");
        }

        private void task10_1Button_Click(object sender, EventArgs e)
        {
            fillTableInRequest("select idSaleGoods, Kiosk_Filial_idFilial as `idFilial`, ProductName, s.Count, Price, s.`Date`, sp.Sells as `Company` from salegoods s left join suplpy on Kiosk_Filial_idFilial = Filial_idFilial left join supplier sp on idsupplier = supplier_idsupplier where s.`Count` >= 10;");
        }

        private void task10_2Button_Click(object sender, EventArgs e)
        {
            fillTableInRequest($"select idSaleGoods, Kiosk_Filial_idFilial as `idFilial`, ProductName, s.Count, Price, s.`Date`, sp.Sells as `Company` from salegoods s left join suplpy on Kiosk_Filial_idFilial = Filial_idFilial left join supplier sp on idsupplier = supplier_idsupplier where s.`Count` >= 10 and Kiosk_Filial_idFilial = {int.Parse(textBox8.Text)};");
        }

        private void task11_2Button_Click(object sender, EventArgs e)
        {
            fillTableInRequest($"select ProductName, sum(`Count`) as `Volume of sales` from salegoods where Kiosk_Filial_idFilial = {int.Parse(textBox9.Text)} and `Date` between '{dateTimePicker15.Value.ToString("yyyyMMdd")}' and '{dateTimePicker16.Value.ToString("yyyyMMdd")}' group by ProductName;");
        }

        private void task11_1Button_Click(object sender, EventArgs e)
        {
            fillTableInRequest("select ProductName, sum(`Count`) as `Volume of sales` from salegoods group by ProductName;");
        }

        private void task12_1Button_Click(object sender, EventArgs e)
        {
            fillTableInRequest("select * from position");
        }

        private void TasksForm_Load(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("select distinct Name from position", MyConn);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetValue(0));
                }
            }
            reader.Close();
        }

        private void task12_2Button_Click(object sender, EventArgs e)
        {
            fillTableInRequest($"select * from position where name = '{comboBox1.SelectedItem}'");
        }
    }
}
