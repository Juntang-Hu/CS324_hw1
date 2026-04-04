using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 視窗二CS324_hw1
{
    public partial class Form1 : Form
    {
        bool isRate = true;
        int house_p = 0;
        double down_payment = 0;
        double interest_rate = 0;
        int loan_term = 0;
        int grace_period = 0;

        public Form1()
        {
            InitializeComponent();

            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label12.Text = "";
            label13.Text = "";
            label14.Text = "";
            label16.Text = "";
            label20.Text = "";
            label24.Text = "";
            label30.Text = "";
            TextBox[] tb = { textBox1, textBox2, textBox3, textBox4 };
            for (int i = 0; i < 4; i++)
                tb[i].BackColor = Color.White;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)

        {
            TextBox[] tb = { textBox1, textBox2, textBox3, textBox4 };
            for (int i = 0; i < 4; i++)
                tb[i].BackColor = Color.White;
   
            bool isNoValue = false;
            for(int i = 0; i < 4; i++)
            {
                if (tb[i].Text == "")
                {
                    label30.Text = "輸入不完整";
                    tb[i].BackColor = Color.Pink;
                    isNoValue=true;
                }
            }
            if (isNoValue)
                return;

            if (isRate)
            {
                if (double.Parse(textBox2.Text) > 100)
                {
                    textBox2.BackColor = Color.Pink;
                    label30.Text = "自備款比例有誤";
                    return;
                }
            }
            else
            {
                if (int.Parse(textBox1.Text) < double.Parse(textBox2.Text))
                {
                    textBox1.BackColor = Color.Pink;
                    label30.Text = "房屋總價低於自備款";
                    return;
                }
            }


            house_p = int.Parse(textBox1.Text); //房屋總價
            if (isRate)
                down_payment = house_p *(1- (double.Parse(textBox2.Text)/100)); //自備款比例
            else
                down_payment = double.Parse(textBox2.Text);
            interest_rate = double.Parse(textBox4.Text)/100;  //貸款年利率
            loan_term = int.Parse(textBox3.Text); //貸款年限
            if (textBox5.Text == "")
                grace_period = 0;
            else
                grace_period = int.Parse(textBox5.Text);  //寬限期

            double principal = house_p - down_payment;
            label12.Text = principal.ToString("N2");  //貸款總金額

            double r = interest_rate / 12;
            int n = loan_term * 12;
            int g = grace_period * 12;
            if (grace_period == 0) //無寬限期的算法
            {
                double _1rn = Math.Pow((1 + r), n);
                double monthly_payment = principal * r * _1rn / (_1rn - 1);
                label13.Text = Math.Round(monthly_payment, 2).ToString("N2"); //每月應繳金額
                label14.Text = Math.Round((principal * r), 2).ToString("N2"); //首期利息
                label16.Text = Math.Round((monthly_payment - (principal * r)), 2).ToString("N2"); //首期本金
                label24.Text = Math.Round((monthly_payment * n), 2).ToString("N2");  //總還款金額
                label20.Text = Math.Round((monthly_payment * n)-principal, 2).ToString("N2");  //總利息支出
            }
            else
            {
                double monthly_payment_s1 = principal * r;
                double _1rn = Math.Pow((1 + r), n-g);
                double monthly_payment_s2 = principal * r * _1rn / (_1rn - 1);
                label13.Text = String.Concat(Math.Round(monthly_payment_s1, 2).ToString(), "/", Math.Round(monthly_payment_s2, 2).ToString()); //每月應繳金額
                label14.Text = Math.Round((principal * r), 2).ToString("N2"); //首期利息
                label16.Text = String.Concat("0/", Math.Round((monthly_payment_s2 - (principal * r)), 2)); //首期本金
                label24.Text = Math.Round(((monthly_payment_s1 * g) + (monthly_payment_s2 * (n - g))), 2).ToString("N2");  //總還款金額
                label20.Text = Math.Round(((monthly_payment_s1 * g) + (monthly_payment_s2 * (n - g))) - principal, 2).ToString("N2");  //總利息支出

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            if (isRate == true)
            {
                isRate = false;
                label5.Text = "元";
                button2.Text = "切換為比例";
            }
            else
            {
                isRate = true;
                label5.Text = "%";
                button2.Text = "切換為金額";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == '.')
                return;
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // 如果不是數字也不是控制鍵，就攔截掉 (Handled = true)
                e.Handled = true;
            }
        }
    }
}
