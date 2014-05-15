using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab03
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = openToolStripMenuItem.Text;
            string result = "";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    result += line + "\r\n";
                }
                sr.Close();
            }
            textBox1.Text = result;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = saveAsToolStripMenuItem.Text;
            
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false);
                sw.WriteLine(textBox1.Text);
                sw.Close();
            }
        }

    }
}
