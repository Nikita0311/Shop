using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace exam
{
    public partial class Menu : Form
    {
        private int limitPage = 0;
        private int statePage = 0;
        private int a = 0;
        private int b = 6;

        static private List<PictureBox> pictureList = new List<PictureBox>(); // фото товара
        static private List<Label> titleList = new List<Label>(); // название товара
        static private List<Label> costList = new List<Label>(); // стоимость товара
        static private List<Label> descriptionList = new List<Label>(); // описание товара
        static private List<Label> discountList = new List<Label>(); // скидка товара
        static private List<object[]> data = new List<object[]>();

        public Menu()
        {
            InitializeComponent();
        }

        #region Переход по страницам
        private void Next()
        {
            int num = 0;
            if (statePage != (int)limitPage/6)
            {
                statePage++;
                label13.Text = $"Страница №{statePage}";
                for (int i = a; i < b; i++) 
                {
                    //pictureList[num].Image = Image.FromFile($@"D:\projects\exam\exam\{data[i][1].ToString().Remove(0,1)}");
                    titleList[num].Text = $"{data[i][0]}";
                    descriptionList[num].Text = $"{data[i][1]}";
                    costList[num].Text = $"{double.Parse(data[i][3].ToString()) * (double.Parse("0," + (100 - double.Parse(data[i][4].ToString())).ToString()))}";
                    discountList[num].Text = $"{data[i][3]}";
                    num++;
                }
                a += 6; b += 6; 
            }
        }

        private void Back()
        {
            int num = 0;
            if (statePage != limitPage)
            {
                statePage--;
                label13.Text = $"Страница №{statePage}";
                for (int i = a; i < b; i++) 
                {
                    titleList[num].Text = $"{data[i][0]}";
                    descriptionList[num].Text = $"{data[i][1]}";
                    costList[num].Text = $"{double.Parse(data[i][3].ToString()) * (double.Parse("0," + (100 - double.Parse(data[i][4].ToString())).ToString()))}";
                    discountList[num].Text = $"{data[i][3]}";
                    num++;
                }
                a -= 6; b -= 6;
            }
        }
        #endregion

        private void Menu_Load(object sender, EventArgs e)
        {
            try
            {
                DB.sqlConnection.Open();

                int i = 0;

                DB.sqlCommand = new SqlCommand("SELECT * FROM Product;", DB.sqlConnection);
                SqlDataReader sqlDataReader = DB.sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    data.Add(new object[5]);
                    data[i][0] = sqlDataReader.GetValue(1); // ProductName
                    data[i][1] = sqlDataReader.GetValue(2); // ProductDescription
                    data[i][2] = sqlDataReader.GetValue(4); // ProductPhoto
                    data[i][3] = sqlDataReader.GetValue(7); // ProductCost
                    data[i][4] = sqlDataReader.GetValue(8); // ProdyctDiscount
                    i++;
                }

                sqlDataReader.Close();

                DB.sqlCommand = new SqlCommand("SELECT COUNT(ProductArticle) FROM Product", DB.sqlConnection);
                sqlDataReader = DB.sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                    limitPage = int.Parse(sqlDataReader.GetValue(0).ToString());

                sqlDataReader.Close();

                #region
                pictureList.Add(pictureBox1);
                pictureList.Add(pictureBox2);
                pictureList.Add(pictureBox3);
                pictureList.Add(pictureBox4);
                pictureList.Add(pictureBox5);
                pictureList.Add(pictureBox6);

                titleList.Add(label1);
                titleList.Add(label2);
                titleList.Add(label3);
                titleList.Add(label4);
                titleList.Add(label5);
                titleList.Add(label6);

                descriptionList.Add(label14);
                descriptionList.Add(label15);
                descriptionList.Add(label16);
                descriptionList.Add(label17);
                descriptionList.Add(label18);
                descriptionList.Add(label19);

                costList.Add(label7);
                costList.Add(label8);
                costList.Add(label9);
                costList.Add(label10);
                costList.Add(label11);
                costList.Add(label12);

                discountList.Add(label20);
                discountList.Add(label21);
                discountList.Add(label22);
                discountList.Add(label23);
                discountList.Add(label24);
                discountList.Add(label25);
                #endregion

                Next();
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message);
            };
        }

        #region Переход по страницам
        private void button2_Click(object sender, EventArgs e)
        {
            Next();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Back();
        }
        #endregion

        // Search
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int i = 0;
            statePage = 0;
            data.Clear();

            DB.sqlCommand = new SqlCommand($"SELECT * FROM Product WHERE ProductName LIKE %{textBox1.Text}%;", DB.sqlConnection);
            SqlDataReader sqlDataReader = DB.sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                data.Add(new object[4]);
                data[i][0] = sqlDataReader.GetValue(0);
                data[i][1] = sqlDataReader.GetValue(1);
                data[i][2] = sqlDataReader.GetValue(2);
                data[i][3] = sqlDataReader.GetValue(3);
                i++;
            }

            sqlDataReader.Close();
        }
    }
}
