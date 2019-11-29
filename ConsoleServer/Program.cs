using Mina.Core.Service;
using Mina.Filter.Codec;
using Mina.Filter.Codec.TextLine;
using Mina.Filter.Logging;
using Mina.Transport.Socket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IoAcceptor acceptor = new AsyncSocketAcceptor();

          //  acceptor.FilterChain.AddLast("logger", new LoggingFilter());
            acceptor.FilterChain.AddLast("codec", new ProtocolCodecFilter(new TextLineCodecFactory(Encoding.UTF8)));

            acceptor.ExceptionCaught += (o, e) => Console.WriteLine(e.Exception);

            acceptor.SessionIdle += (o, e) => Console.WriteLine("IDLE " + e.Session.GetIdleCount(e.IdleStatus));

            acceptor.MessageReceived += (o, e) =>
            {
                String str = e.Message.ToString();

                // "Quit" ? let's get out ...
                if (str.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
                {
                    e.Session.Close(true);
                    return;
                }

                // Send the current date back to the client
                e.Session.Write(DateTime.Now.ToString());
                Console.WriteLine("Message written...");
            };

            acceptor.Bind(new IPEndPoint(IPAddress.Loopback, 1234));

            Console.ReadLine();
        }
    }
}
