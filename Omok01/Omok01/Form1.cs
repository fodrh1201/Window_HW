using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Omok01
{
    public partial class Form1 : Form
    {
        bool isBlack = true;
        int posX;
        int posY;
        int x = 0;
        int y = 0;
        int iX = 0;
        int iY = 0;
        int startX;
        int startY;
        int finX;
        int finY;
        int constSx;
        int constSy;
        int constFx;
        int constFy;

        int[] dx = new int[4] { -1, -1, 0, 1 };
        int[] dy = new int[4] { 0, 1, 1, 1 };

        int[,] board = new int[21, 21];
        bool[,] judgeBoard = new bool[21, 21];

        LinkedList<Term> stoneList = new LinkedList<Term>();

        Bitmap screen;
        Graphics g4;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 100;
            timer1.Enabled = true;
            textBox3.KeyDown += new KeyEventHandler(InputByKey);
        }

        private void InputByKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Term stone = new Term();

                string[] input = textBox3.Text.Split(' ');
                iY = Convert.ToInt32(input[0]) - 1;
                iX = Convert.ToInt32(input[1]) - 1;

                x = 50 + 30 * iX - 15;
                y = 50 + 30 * iY - 15;

                if (board[iY + 1, iX + 1] < 0)
                {
                    textBox3.Text = "";
                    return;
                }

                if (isBlack)
                {
                    stone.x = x;
                    stone.y = y;
                    stone.color = -1;
                    stone.arrX = iY;
                    stone.arrY = iX;
                    isBlack = false;
                    textBox2.Text = String.Format("{0}, {1}, Black", iY + 1, iX + 1);
                }
                else
                {
                    stone.x = x;
                    stone.y = y;
                    stone.color = -2;
                    stone.arrX = iY;
                    stone.arrY = iX;
                    isBlack = true;
                    textBox2.Text = String.Format("{0}, {1}, White", iY, iX);
                }
                stoneList.AddLast(stone);
                board[iY + 1, iX + 1] = stone.color;
                if (Judge(iY, iX) == 1)
                {
                    textBox1.Text = "흑 승리";
                    constSx = startX;
                    constSy = startY;
                    constFx = finX;
                    constFy = finY;
                }
                else if (Judge(iY, iX) == -1)
                {
                    textBox1.Text = "백 승리";
                    constSx = startX;
                    constSy = startY;
                    constFx = finX;
                    constFy = finY;
                }

                textBox3.Text = "";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Draw()
        {
            screen = new Bitmap(640, 640);
            g4 = Graphics.FromImage(screen);
            g = this.CreateGraphics();
            g4.FillRectangle(Brushes.NavajoWhite, 0, 0, 640, 640);
            Pen p = new Pen(Color.Black, 2);

            for (int i = 0; i < 19; i++)
            {
                g4.DrawLine(p, 50, 50 + 30 * i, 590, 50 + 30 * i);
            }
            for (int j = 0; j < 19; j++)
            {
                g4.DrawLine(p, 50 + 30 * j, 50, 50 + 30 * j, 590);
            }

            DrawSemiCircle();
            DrawCircle();
            DrawLine();
            g.DrawImage(screen, 0, 0);

            p.Dispose();
            g.Dispose();
            g4.Dispose();
            screen.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Draw();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            posX = e.X;
            posY = e.Y;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Term stone = new Term();

            if (board[iY+1, iX+1] < 0)
                return;

            if (isBlack)
            {
                stone.x = x;
                stone.y = y;
                stone.color = -1;
                stone.arrX = iY;
                stone.arrY = iX;
                isBlack = false;
                textBox2.Text = String.Format("{0}, {1}, Black", iY, iX);
            }
            else
            {
                stone.x = x;
                stone.y = y;
                stone.color = -2;
                stone.arrX = iY;
                stone.arrY = iX;
                isBlack = true;
                textBox2.Text = String.Format("{0}, {1}, White", iY, iX);
            }
            stoneList.AddLast(stone);
            board[iY + 1, iX + 1] = stone.color;

            if (Judge(iY, iX) == 1)
            {
                textBox1.Text = "흑 승리";
                constSx = startX;
                constSy = startY;
                constFx = finX;
                constFy = finY;
            }
            else if (Judge(iY, iX) == -1)
            {
                textBox1.Text = "백 승리";
                constSx = startX;
                constSy = startY;
                constFx = finX;
                constFy = finY;
            }


        }

        private void ChangePosition(ref int x, ref int y)
        {
            iX = (posX - 50) / 30;
            iY = (posY - 50) / 30;

            if ((posX - 50) % 30 > 15)
            {
                if (iX < 0)
                    iX = 0;
                else if (iX >= 18)
                    iX = 18;
                else
                    iX = iX + 1;
            }
            else
            {
                if (iX < 0)
                    iX = 0;
                else if (iX >= 18)
                    iX = 18;
            }

            if ((posY - 50) % 30 > 15)
            {
                if (iY < 0)
                    iY = 0;
                else if (iY >= 18)
                    iY = 18;
                else
                    iY = iY + 1;
            }
            else
            {
                if (iY < 0)
                    iY = 0;
                else if (iY >= 18)
                    iY = 18;
            }

            x = 50 + 30 * iX - 15;
            y = 50 + 30 * iY - 15;

        }

        private void DrawCircle()
        {
            Pen p = new Pen(Color.Yellow, 2);

            for (int i = 0; i < stoneList.Count; i++)
            {
                if (stoneList.ElementAt(i).color == -1)
                {
                    Brush b = Brushes.Black;
                    g4.FillEllipse(b, stoneList.ElementAt(i).x, stoneList.ElementAt(i).y, 30, 30);
                }
                else
                {
                    Brush b = Brushes.White;
                    g4.FillEllipse(b, stoneList.ElementAt(i).x, stoneList.ElementAt(i).y, 30, 30);
                }

                if (i == stoneList.Count - 1)
                {
                    g4.DrawRectangle(p, stoneList.ElementAt(i).x, stoneList.ElementAt(i).y, 30, 30);
                }
            }

           

            p.Dispose();
        }

        private void DrawSemiCircle()
        {
            if (isBlack)
            {
                SolidBrush b = new SolidBrush(Color.FromArgb(180, 100, 100, 100));
                ChangePosition(ref x, ref y);
                g4.FillEllipse(b, x, y, 30, 30);
            }
            else
            {
                
                SolidBrush b = new SolidBrush(Color.FromArgb(180, 255, 255, 255));
                ChangePosition(ref x, ref y);
                g4.FillEllipse(b, x, y, 30, 30);
            }
        }

        private void DrawLine()
        {
            Pen p = new Pen(Color.Green, 2);
            g4.DrawLine(p, 50 + 30 * constSy - 30, 50 + 30 * constSx - 30, 50 + 30 * constFy - 30, 50 + 30 * constFx - 30);
            p.Dispose();
         
        }

        private int Judge(int iY, int iX)
        {
            int count = 0;
            int countForThree = 0;
            int countForFour = 0;
            int judge;
            int color = board[iY + 1, iX + 1];
            int k = 1;
            int[] test = new int[4];
            

            if (isBlack)
                judge = -2;
            else
                judge = -1;

            for (int i = 0; i < 4; i++)
            {
                while (color == judge)
                {
                    int currentX = iY + 1 + (k - 1) * dx[i];
                    int currentY = iX + 1 + (k - 1) * dy[i];

                    for (int l = 0; l < 4; l++)
                    {
                        if (l != i)
                        {
                            if (color == board[currentX + dx[l], currentY + dy[l]])
                            {
                                if (judgeBoard[currentX + dx[l], currentY + dy[l]])
                                {
                                    Judge(currentX + dx[l], currentY + dy[l]);
                                    judgeBoard[currentX + dx[l], currentY + dy[l]] = false;
                                }
                            }
                        }
                    }

                    for (int l = 0; l < 4; l++)
                    {
                        if (l != i)
                        {
                            if (color == board[currentX - dx[l], currentY - dy[l]])
                            {
                                if (judgeBoard[currentX - dx[l], currentY - dy[l]])
                                {
                                    Judge(currentX - dx[l], currentY - dy[l]);
                                    judgeBoard[currentX - dx[l], currentY - dy[l]] = false;
                                }
                            }
                        }
                    }

                    judgeBoard = new bool[21, 21];

                    color = board[currentX + dx[i], currentY + dy[i]];
                    count++;
                    k++;
                }

                int tempSX = iY + 1 + (k - 2) * dx[i];
                int tempSY = iX + 1 + (k - 2) * dy[i];
                
                k = 2;
                color = board[iY + 1 - dx[i], iX + 1 - dy[i]];

                while (color == judge)
                {
                    int currentX = iY + 1 - (k - 1) * dx[i];
                    int currentY = iX + 1 - (k - 1) * dy[i];


                    for (int l = 0; l < 4; l++)
                    {
                        if (l != i)
                        {
                            if (color == board[currentX - dx[l], currentY - dy[l]])
                                Judge(currentX - dx[l], currentY - dy[l]);
                        }
                    }

                    color = board[currentX - dx[i], currentY - dy[i]];
                    count++;
                    k++;
                }

                int tempFX = iY + 1 - (k - 2) * dx[i];
                int tempFY = iX + 1 - (k - 2) * dy[i];

                color = board[iY + 1, iX + 1];
                k = 1;

                if (count == 3)
                    countForThree++;

                if (count == 4)
                    countForFour++;

                if (count == 5)
                {
                    startX = tempSX;
                    startY = tempSY;
                    finX = tempFX;
                    finY = tempFY;
                    count = 0;
                    if (isBlack == false)
                        return 1;
                    else
                        return -1;
                }
                count = 0;
            }
            if (countForFour >= 2 || countForThree >= 2)
            {
                if (isBlack == false)
                    return -1;
                else
                    return 0;
            }

            return 0;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            board = new int[21, 21];
            stoneList = new LinkedList<Term>();
            isBlack = true;
            textBox1.Text = "";
            constSx = 0;
            constSy = 0;
            constFx = 0;
            constFy = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

    }

    public class Term
    {
        public int x;
        public int y;
        public int color;
        public int arrX;
        public int arrY;
    }
}
