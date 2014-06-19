using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

using System.Net;
using System.Net.Sockets;

namespace Omok02
{
    public class Server
    {
        static byte[] buf = new byte[1024];
        static NetworkStream ns;
        public static Stone lastStone;
        public static Board board = new Board();
        public static bool isServer = true;
        public static int win;
        static Socket server;
        static Socket client;

        public static void StartServer()
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 3333);
            server.Bind(ip);

            server.Listen(15);

            client = server.Accept();
            ns = new NetworkStream(client);

            Thread ctThread = new Thread(doOmok);
            ctThread.Start();
        }

        private static void doOmok()
        {
            int len;
            while ((len = ns.Read(buf, 0, buf.Length)) != 0)
            {
                string msg = Encoding.UTF8.GetString(buf, 0, len);
                string[] s = msg.Split(' ');
                int[] stn = s.Select(x => int.Parse(x)).ToArray();
                Stone newStone = new Stone(stn[0], stn[1], stn[2]);
                board.Insert(newStone);
                lastStone = newStone;
                isServer = true;
            }

            ns.Close();
            client.Close();
        }

        public static void Send(string message)
        {
            string[] s = message.Split(' ');
            int[] stn = s.Select(x => int.Parse(x)).ToArray();
            byte[] send = Encoding.UTF8.GetBytes(message);
            ns.Write(send, 0, send.Length);
            Stone newStone = new Stone(stn[0], stn[1], stn[2]);
            board.Insert(newStone);
            lastStone = newStone;
            isServer = false;
        }

        public static void Stop()
        {
            ns.Close();
            client.Close();
        }
    }
}
