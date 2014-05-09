using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW140509
{
    public partial class Form1 : Form
    {
        int count = 0;
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "입력하세요. \r\n";
            textBox2.KeyDown += new KeyEventHandler(input);
        }

        private void input(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (radioButton1.Checked)
                {
                    if (count == 0)
                    {
                        textBox1.Text = "고형진 : " + textBox2.Text + "\r\n";
                        count += 1;
                        radioButton2.Checked = true;
                    }
                    else
                    {
                        textBox1.Text = textBox1.Text + "고형진 : " + textBox2.Text + "\r\n";
                        radioButton2.Checked = true;
                    }
                }
                else if (radioButton2.Checked)
                {
                    textBox1.Text = textBox1.Text + "심심이 : " + textBox2.Text + "\r\n";
                    radioButton1.Checked = true;
                }
                textBox2.Text = "";
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox1.Text = textBox1.Text + "고형진 : " + textBox2.Text + "\r\n";
                radioButton2.Checked = true;
            }
            else if (radioButton2.Checked)
            {
                textBox1.Text = textBox1.Text + "심심이 : " + textBox2.Text + "\r\n";
                radioButton1.Checked = true;
            }
            textBox2.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Text = "입력하세요. \r\n";
            count = 0;
        }
    }
}
