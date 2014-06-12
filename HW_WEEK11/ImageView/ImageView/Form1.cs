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

namespace ImageView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "XML|*.xml|JPG|*.jpg|모든파일|*.*";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = img;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            ImageData joongil = new ImageData();
            joongil.SetSize(bmp.Width, bmp.Height);
            for (int i = 0; i < joongil.height; i++)
            {
                for (int j = 0; j < joongil.width; j++)
                {
                    joongil.pixel[i * joongil.width + j] = bmp.GetPixel(j, i).ToArgb();
                }
            }

            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            XmlSerializer xs = new XmlSerializer(typeof(ImageData));
            StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false);
            xs.Serialize(sw, joongil);
            sw.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            ImageData img;
            XmlSerializer xs = new XmlSerializer(typeof(ImageData));
            StreamReader sr = new StreamReader(openFileDialog1.FileName);
            XmlReader reader = XmlReader.Create(sr);
            img = (ImageData)xs.Deserialize(reader);

            int width = img.width;
            int height = img.height;

            Bitmap bmp2 = new Bitmap(img.width, img.height);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Color col = Color.FromArgb(img.pixel[i*width+j]);
                    bmp2.SetPixel(j, i, col);
                }
            }
            pictureBox1.Image = bmp2;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
