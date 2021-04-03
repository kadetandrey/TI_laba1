using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba1TI
{
    public partial class Main : Form
    {
        string methodOfCrypto = "rl";
        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            methodOfCrypto = "pl";
            label3.Text = "Железнодорожная изгородь";
            label4.Visible = true;
            textBox3.Visible = true;

            label5.Visible = false;
            textBox4.Visible = false;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            switch (methodOfCrypto)
            {
                case "pl":
                    textBox2.Text = RailFence.Decrypt(textBox1.Text, Int32.Parse(textBox3.Text));
                    break;
                case "cz":
                    textBox2.Text = Cesar.Decrypt(textBox1.Text, Int32.Parse(textBox3.Text));
                    break;
                case "st":
                    textBox2.Text = Column.Decrypt(textBox1.Text, textBox4.Text);
                    break;
                case "tr":
                    textBox2.Text = Turning.Decrypt(textBox1.Text, textBox4.Text);
                    break;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (methodOfCrypto != "st" && methodOfCrypto != "tr")
            {
                bool tr = false;
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Введите текст который хотите зашифровать");
                }
                else
                {
                    if (textBox3.Text == "")
                    {

                        MessageBox.Show("Введите ключ");
                    }
                    else
                    {
                        try
                        {
                            Int32.Parse(textBox3.Text);
                            if (Int32.Parse(textBox3.Text) < 2)
                            {
                                MessageBox.Show("Введите ключ целое положительное число");
                                tr = true;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Введите ключ целое положительное число");
                            tr = true;
                        }

                        if (!tr)
                        {
                            switch (methodOfCrypto)
                            {
                                case "pl":
                                    textBox2.Text = RailFence.Encrypt(textBox1.Text, Int32.Parse(textBox3.Text));
                                    break;
                                case "cz":
                                    textBox2.Text = Cesar.Encrypt(textBox1.Text, Int32.Parse(textBox3.Text));
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                switch (methodOfCrypto)
                {
                    case "st":
                    textBox2.Text = Column.Encrypt(textBox1.Text, textBox4.Text);
                break;
                case "tr":
                    textBox2.Text = Turning.Encrypt(textBox1.Text, textBox4.Text);
                break;
            }
           
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            methodOfCrypto = "cz";
            label3.Text = "Цезарь";
            label4.Visible = true;
            textBox3.Visible = true;

            label5.Visible = false;
            textBox4.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            methodOfCrypto = "st";
            label3.Text = "Столбцовый";
            label5.Visible = true;
            textBox4.Visible = true;

            label4.Visible = false;
            textBox3.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            methodOfCrypto = "tr";
            label3.Text = "Переворачивающаяся решетка";
            label5.Visible = true;
            textBox4.Visible = true;

            label4.Visible = false;
            textBox3.Visible = false;
        }
    }
}
