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
using System.Xml.Serialization;
using System.Xml;

namespace Phone
{
    public partial class Form1 : Form
    {
        PhoneBook pb;
        public Form1()
        {
            InitializeComponent();
            pb = new PhoneBook();
           // pb.Add("우재우", "없음", "1학기솔로");
           // pb.Add("김중일", "없음", "교수");

            saveFileDialog1.Filter = "저장파일(*.xml)|*.xml|전부(*.*)|*.*";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                XmlSerializer xs = new XmlSerializer(typeof(PhoneBook));
                xs.Serialize(sw, pb);
                sw.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                XmlSerializer xs = new XmlSerializer(typeof(PhoneBook));
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                XmlReader reader = XmlReader.Create(sr);
                pb = (PhoneBook)xs.Deserialize(reader);
                sr.Close();

                textBox1.Text = pb.GetAllData();
                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "편집 중";
        }
    }
}
