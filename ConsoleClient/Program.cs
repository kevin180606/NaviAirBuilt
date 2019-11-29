using Mina.Core.Future;
using Mina.Core.Service;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.TextLine;
using Mina.Transport.Socket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleClient
{
   public class Program
    {
  
        static void Main(string[] args)
        {
            IoConnector connector = new AsyncSocketConnector();
            IoSession session;


            connector.FilterChain.AddLast("codec", new ProtocolCodecFilter(new TextLineCodecFactory(Encoding.UTF8)));
            connector.MessageReceived += Connector_MessageReceived;
            IConnectFuture future = connector.Connect(new IPEndPoint(IPAddress.Loopback, 1234)).Await();

            //   Sleep(10000);
            Thread.Sleep(10000);
            
            if (future.Connected)
            {
                session = future.Session;
                session.Write("hello world");
            }
            else
            {
                Console.WriteLine("not connented!");
            }


            Console.ReadLine();


            //   acceptor.FilterChain.AddLast("logger", new LoggingFilter());
            //    acceptor.FilterChain.AddLast("codec", new ProtocolCodecFilter(new TextLineCodecFactory(Encoding.UTF8)));

            //     acceptor.ExceptionCaught += (o, e) => Console.WriteLine(e.Exception);

            //   acceptor.SessionIdle += (o, e) => Console.WriteLine("IDLE " + e.Session.GetIdleCount(e.IdleStatus));

            //   acceptor.MessageReceived += (o, e) =>





        }

        private static void Connector_MessageReceived(object sender, IoSessionMessageEventArgs e)
        {
           // throw new NotImplementedException();
        }
    }
}
