using System;
using System.IO;
using Application;
using QuickFix;

namespace FixProject.Server
{
    class Program
    {
        static MyQuickFixApp myApp;

        static void Main(string[] args)
        {
            SessionSettings settings = new SessionSettings(args[0]);

            myApp = new MyQuickFixApp(settings);
            IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
            ILogFactory logFactory = new FileLogFactory(settings);
            ThreadedSocketAcceptor acceptor = new ThreadedSocketAcceptor(
                myApp,
                storeFactory,
                settings,
                logFactory);


            acceptor.Start();
            while (true)
            {
                string comando = Console.ReadLine();

                switch (comando)
                {

                    default:
                        break;
                }

                System.Threading.Thread.Sleep(1000);
            }
            acceptor.Stop();
        }
    }
}
