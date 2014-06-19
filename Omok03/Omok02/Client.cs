using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.Net;
using System.Net.Sockets;

namespace Omok02
{
    public class Client
    {
        public NetworkStream ns;
        public Board b = new Board();
        Socket s;
        byte[] buf = new byte[1024];
        public Stone lastStone;
        public string msg;

        public void Send(string input)
        {
            byte[] data = Encoding.UTF8.GetBytes(input);
            ns.Write(data, 0, data.Length);
            string[] s = input.Split(' ');
            int[] stn = s.Select(x => int.Parse(x)).ToArray();
            Stone newStone = new Stone(stn[0], stn[1], stn[2]);
            b.Insert(newStone);
            lastStone = newStone;
            Server.isServer = true;
        }

        public void StartReceive()
        {
            Thread ctThread = new Thread(Receive);
            ctThread.Start();
        }

        private void Receive()
        {
            int len;
            while ((len = ns.Read(buf, 0, buf.Length)) != 0)
            {
                string ret = Encoding.UTF8.GetString(buf, 0, len);
                string[] s = ret.Split(' ');
                int[] stn = s.Select(x => int.Parse(x)).ToArray();
                Stone newStone = new Stone(stn[0], stn[1], stn[2]);
                b.Insert(newStone);
                lastStone = newStone;
                Server.isServer = false;
            }

            ns.Close();
        }

        public void Disconnect()
        {
            ns.Close();
            s.Close();
        }

        public string Connect(String address, int port)
        {
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(address), port);

            try
            {
                s.Connect(ip);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            ns = new NetworkStream(s);
            return "Success";
        }

    }
}
