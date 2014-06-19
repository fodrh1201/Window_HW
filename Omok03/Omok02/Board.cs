using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omok02
{
    public class Board
    {
        public Stone[,] board = new Stone[21, 21];
        public LinkedList<Stone> order = new LinkedList<Stone>();

        public int sCount = 0;

        int[] dx = new int[4] { 1, 1, 0, -1 };
        int[] dy = new int[4] { 0, 1, 1, 1 };

        public Board()
        {
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    Stone garbage = new Stone(0, i, j);
                    board[i, j] = garbage;
                }
            }
        }

        public void Insert(Stone s)
        {
            board[s.locX, s.locY] = s;
            order.AddLast(s);
            sCount++;
        }

        public void Delete()
        {
            Stone s = order.Last();
            board[s.locX, s.locY] = null;
            order.RemoveLast();
            sCount--;
        }

        public int JudgeWinner(Stone lastStone)
        {
            for (int i = 0; i < 4; i++)
            {
                if (CountStone(i, lastStone) == 5)
                {
                    return lastStone.color;
                }

                if (lastStone.color == 1)
                {
                    if (ThreeByThree(lastStone) || FourByFour(lastStone))
                        return -1;
                }
            }
            return 0;
        }

        public int CountStone(int dir, Stone lastStone)
        {
            int count = 0;
            int color = lastStone.color;
            int k = 1;
            int nextX;
            int nextY;
            
            while (color == lastStone.color)
            {
                count++;
                nextX = lastStone.locX + k * dx[dir];
                nextY = lastStone.locY + k * dy[dir];
                color = board[nextX, nextY].color;
                k++;
            }

            k = 1;
            color = lastStone.color;

            while (color == lastStone.color)
            {
                count++;
                nextX = lastStone.locX - k * dx[dir];
                nextY = lastStone.locY - k * dy[dir];
                color = board[nextX, nextY].color;
                k++;
            }
            count--;

            return count;
        }

        public bool AttachWithOther(int dir, Stone lastStone)
        {
            int color = lastStone.color;
            int k = 1;
            int nextX = 0;
            int nextY = 0;

            while (color == lastStone.color)
            {
                nextX = lastStone.locX + k * dx[dir];
                nextY = lastStone.locY + k * dy[dir];
                color = board[nextX, nextY].color;
                k++;
            }

            if (board[nextX, nextY].color == - lastStone.color)
                return true;

            k = 1;
            color = lastStone.color;

            while (color == lastStone.color)
            {
                nextX = lastStone.locX - k * dx[dir];
                nextY = lastStone.locY - k * dy[dir];
                color = board[nextX, nextY].color;
                k++;
            }

            if (board[nextX, nextY].color == - lastStone.color)
                return true;
            
            return false;
        }

        public bool ThreeByThree(Stone lastStone)
        {
            int count = 0;

            for (int i = 0; i < 4; i++)
            {
                if (CountStone(i, lastStone) == 3 && !AttachWithOther(i, lastStone))
                    count++;
            }
            
            if (count >= 2)
                return true;
            else
                return false;
        }

        public bool FourByFour(Stone lastStone)
        {
            int count = 0;

            for (int i = 0; i < 4; i++)
            {
                if (CountStone(i, lastStone) == 4 && !AttachWithOther(i, lastStone))
                    count++;
            }

            if (count >= 2)
                return true;
            else
                return false;
        }

        public int[,] Edges(int dir, Stone lastStone)
        {
            int[,] edges = new int[2, 2];

            int color = lastStone.color;
            int k = 1;
            int curX = 0;
            int curY = 0;

            while (color == lastStone.color)
            {
                curX = lastStone.locX + (k-1) * dx[dir];
                curY = lastStone.locY + (k-1) * dy[dir];
                color = board[curX+dx[dir], curY+dy[dir]].color;
                k++;
            }

            edges[0, 0] = curX;
            edges[0, 1] = curY;

            k = 1;
            color = lastStone.color;

            while (color == lastStone.color)
            {
                curX = lastStone.locX - (k - 1) * dx[dir];
                curY = lastStone.locY - (k - 1) * dy[dir];
                color = board[curX - dx[dir], curY - dy[dir]].color;
                k++;
            }

            edges[1, 0] = curX;
            edges[1, 1] = curY;

            return edges;
        }

        public int MaxDirection(Stone lastStone)
        {
            int result = 0;

            for (int i = 0; i < 4; i++)
            {
                if (CountStone(result, lastStone) < CountStone(i, lastStone))
                    result = i;
            }

            return result;
        }
    }
}
