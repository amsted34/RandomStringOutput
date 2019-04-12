using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            

            while(true)
            { 
            Console.WriteLine("please enter the number of random strings you wish");
            var input = Console.ReadLine();
                StartRandom(input); 
            }


           

        }

        public static void StartRandom(string input)
        {
            UdpClient socket = new UdpClient();
            IPEndPoint IEP = new IPEndPoint(IPAddress.None, 27000);
            for (int i = 0; i < Convert.ToInt32(input); i++)
            {
                var random = GetUniqueKey(15);
                var data = Encoding.ASCII.GetBytes(random.ToString());
                socket.Send(data, data.Length, IEP);
                Console.WriteLine(random);
                Thread.Sleep(50);
            }
            socket.Close(); 
        }

        public static string GetUniqueKey(int size)
        {
            char[] chars =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890".ToCharArray();
            byte[] data = new byte[size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
