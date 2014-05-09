using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW140509_Timer
{
    public partial class Form1 : Form
    {
        bool isClick = true;
        
        int m;
        int s;
        int ms;

        public Form1()
        {
            InitializeComponent();
            textBox1.KeyDown += new KeyEventHandler (input);
        }

        private void input(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string[] minSec = textBox1.Text.Split(' ');
                m = Convert.ToInt32(minSec[0]);
                s = Convert.ToInt32(minSec[1]);
                ms = 6000 * m + 100 * s;
                textBox1.Text = "";
                label4.Text = Time(ms % 100);
                label3.Text = Time(s % 60) + ":";
                label2.Text = Time(m) + ":";

            }
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
        private string Time(int num)
        {
            if (num / 10 != 0)
            {
                return Convert.ToString(num);
            }
            else
            {
                return "0" + Convert.ToString(num);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (radioButton1.Checked)
            {
                label2.Text = Time(DateTime.Now.Hour) + ":";
                label3.Text = Time(DateTime.Now.Minute) + ":";
                label4.Text = Time(DateTime.Now.Second);
            }
            else if (radioButton2.Checked)
            {
                ms += 1;
                s = ms / 100;
                m = s / 60;
                label4.Text = Time(ms % 100);
                label3.Text = Time(s % 60) +":";
                label2.Text = Time(m) +":";
            }
            else if (radioButton3.Checked)
            {
                ms -= 1;
                s = ms / 100;
                m = s / 60;
                label4.Text = Time(ms % 100);
                label3.Text = Time(s % 60) + ":";
                label2.Text = Time(m) + ":";

                if (ms == 0)
                {
                    timer1.Stop();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                m = 0;
                ms = 0;
                s = 0;
            }
            else if (radioButton3.Checked)
            {
                string[] minSec = textBox1.Text.Split(' ');
                m = Convert.ToInt32(minSec[0]);
                s = Convert.ToInt32(minSec[1]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1_Tick(sender, e);
            timer1.Interval = 10;

            if (isClick)
            {
                timer1.Start();
                isClick = false;
            }
            else
            {
                timer1.Stop();
                isClick = true;
            }
           
            
                
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            m = 0;
            ms = 0;
            s = 0;
            label4.Text = Time(ms % 100);
            label3.Text = Time(s % 60) + ":";
            label2.Text = Time(m) + ":";
            timer1.Stop();
            isClick = true;
        }
    }
}
