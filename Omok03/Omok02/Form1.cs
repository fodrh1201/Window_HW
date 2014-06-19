using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Omok02
{
    public partial class Form1 : Form
    {
        bool verAI = false;
        bool verP2P = true;
        bool verNetwork = false;

        Client client;

        Board b = new Board();
        Stone lastStone;

        bool isBlack = true;

        Bitmap screen;
        Graphics realG;
        Graphics outG;

        int mLocX, mLocY;
        int lineX, lineY;
        int locX, locY;

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 50;
            timer1.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox3.Text = "10.211.55.3";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawOmok();
            if (Server.isServer)
            {
                lastStone = Server.lastStone;
                if (client != null)
                    lastStone = client.lastStone;
            }
            else if (client != null)
            {
                lastStone = client.lastStone;
                textBox2.Text = String.Format("{0} {1} {2}", lastStone.color, lastStone.locX, lastStone.locY);
            }

            if (lastStone != null)
                Judge();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            mLocX = e.Y;
            mLocY = e.X;
        }

        private void Judge()
        {
            if (b.JudgeWinner(lastStone) == 1)
            {
                textBox1.Text = "흑 승리";
            }
            else if (b.JudgeWinner(lastStone) == -1)
                textBox1.Text = "백 승리";
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (verP2P)
            {
                if (b.board[lineX, lineY].color != 0)
                    return;

                int color;

                if (isBlack)
                {
                    color = 1;
                    isBlack = false;
                }
                else
                {
                    color = -1;
                    isBlack = true;
                }

                Stone s = new Stone(color, lineX, lineY);
                lastStone = s;
                b.Insert(s);
            }
            else if (verAI)
            {
                AI ai = new AI(b);

                if (b.board[lineX, lineY].color != 0)
                    return;

                Stone s = new Stone(1, lineX, lineY);
                lastStone = s;
                b.Insert(s);

                lastStone = ai.Stone();
                b.Insert(lastStone);
            }
            else if (verNetwork)
            {
                if (b.board[lineX, lineY].color != 0)
                    return;

               if (Server.isServer)
               {
                   if (client == null)
                   {
                       Stone s = new Stone(1, lineX, lineY);
                       Server.Send(String.Format("{0} {1} {2}", s.color, s.locX, s.locY));
                       lastStone = Server.lastStone;
                   }
               }
               else
               {
                   if (client != null)
                   {
                       Stone s = new Stone(-1, lineX, lineY);
                       client.Send(String.Format("{0} {1} {2}", s.color, s.locX, s.locY));
                       lastStone = client.lastStone;
                   }
               }
            }

        }

        private void DrawMain()
        {

        }

        private void DrawOmok()
        {
            screen = new Bitmap(640, 640);
            realG = Graphics.FromImage(screen);
            outG = this.CreateGraphics();

            DrawBoard();
            DrawSemiStone();
            DrawStoneList();
            DrawLine();

            outG.DrawImage(screen, 0, 0);

            screen.Dispose();
            realG.Dispose();
            outG.Dispose();
        }

        private void DrawBoard()
        {
            realG.FillRectangle(Brushes.NavajoWhite, 0, 0, 640, 640);
            Pen p = new Pen(Color.Black, 2);

            for (int i = 0; i < 19; i++)
            {
                realG.DrawLine(p, 50, 50 + 30 * i, 590, 50 + 30 * i);
                realG.DrawLine(p, 50 + 30 * i, 50, 50 + 30 * i, 590);
            }

            p.Dispose();
        }

        
        private void DrawSemiStone()
        {
            ChangePosition();
            SolidBrush b;

            if (isBlack)
                b = new SolidBrush(Color.FromArgb(180, 100, 100, 100));
            else
                b = new SolidBrush(Color.FromArgb(180, 255, 255, 255));

            realG.FillEllipse(b, locY, locX, 30, 30);
        }

        private void DrawStoneList()
        {
            foreach (Stone a in b.order)
                DrawOneStone(a);

            if (lastStone != null)
            {
                Pen p = new Pen(Color.Yellow, 2);

                int x = lastStone.locX * 30 + 5;
                int y = lastStone.locY * 30 + 5;

                realG.DrawRectangle(p, y, x, 30, 30);

                p.Dispose();
            }
        }

        private void DrawOneStone(Stone s)
        {
            int iX = s.locX;
            int iY = s.locY;
            int x = 5 + 30 * iX;
            int y = 5 + 30 * iY;

            Brush brush;

            if (s.color == 1)
                brush = Brushes.Black;
            else
                brush = Brushes.White;

            realG.FillEllipse(brush, y, x, 30, 30);
        }

        private void DrawLine()
        {
            if (lastStone == null)
                return;

            int dir = b.MaxDirection(lastStone);
            int x1 = 0, y1 = 0, x2 = 0, y2 = 0;

            if (b.CountStone(dir, lastStone) == 5)
            {
                x1 = b.Edges(dir, lastStone)[0, 0];
                y1 = b.Edges(dir, lastStone)[0, 1];
                x2 = b.Edges(dir, lastStone)[1, 0];
                y2 = b.Edges(dir, lastStone)[1, 1];
            }

            Pen p = new Pen(Color.Green, 2);

            realG.DrawLine(p, 20 + 30 * y1, 20 + 30 * x1, 20 + 30 * y2, 20 + 30 * x2);

            p.Dispose();
        }

        private void ChangePosition()
        {
            lineX = (mLocX - 50) / 30 + 1;
            lineY = (mLocY - 50) / 30 + 1;

            if ((mLocX - 50) % 30 > 15)
            {
                if (lineX < 1)
                    lineX = 1;
                else if (lineX >= 19)
                    lineX = 19;
                else
                    lineX = lineX + 1;
            }
            else
            {
                if (lineX < 1)
                    lineX = 1;
                else if (lineX > 19)
                    lineX = 19;
            }

            if ((mLocY - 50) % 30 > 15)
            {
                if (lineY < 1)
                    lineY = 1;
                else if (lineY >= 19)
                    lineY = 19;
                else
                    lineY = lineY + 1;
            }
            else
            {
                if (lineY < 1)
                    lineY = 1;
                else if (lineY >= 19)
                    lineY = 19;
            }
            locX = 5 + 30 * lineX;
            locY = 5 + 30 * lineY;
        }

        private void New()
        {
            b = new Board();
            lastStone = null;
            isBlack = true;
            textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            New();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            verAI = false;
            verP2P = true;
            verNetwork = false;
            New();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            verAI = true;
            verP2P = false;
            verNetwork = false;
            New();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Server.StartServer();
            b = Server.board;
            lastStone = Server.lastStone;
            isBlack = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            client = new Client();
            b = client.b;
            
            isBlack = false;
            string address = textBox2.Text;
            int port = 3333;
            string result = client.Connect(address, port);
            if (result == "Success")
                label1.Text = result;
            else
                label1.Text = result;
            client.StartReceive();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            verAI = false;
            verP2P = false;
            verNetwork = true;
            New();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (client == null)
            {
                Server.Stop();
            }
            else
            {
                client.Disconnect();
            }
        }

    }
}
