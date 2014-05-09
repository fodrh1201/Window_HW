using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace HW140509_Timer
{
    public partial class Form1 : Form
    {
        bool isClick = true;
       
        int m;
        int s;
        int ms;

        SoundPlayer sp;

        public Form1()
        {
            InitializeComponent();
            textBox1.KeyDown += new KeyEventHandler (input);
            sp = new SoundPlayer(Properties.Resources.bell);
        }

        private void input(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string strSec = textBox1.Text;
                s = Convert.ToInt32(strSec);
                ms = 100 * s;
                m = s / 60;
                textBox1.Text = "";
                label4.Text = Time(ms % 100);
                label3.Text = Time(s % 60) + ":";
                label2.Text = Time(m) + ":";

            }
            
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
                    sp.Play();
                }
            }
        }

        private void reset()
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

        private void Form1_Load(object sender, EventArgs e)
        {
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
            reset();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            reset();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            reset();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            reset();
        }
    }
}
