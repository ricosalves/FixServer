using System;
using Application;
using QuickFix;
using QuickFix.Transport;

namespace FixProject.Client
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
            SocketInitiator initiator = new SocketInitiator(
                myApp,
                storeFactory,
                settings,
                logFactory);

            initiator.Start();
            while (true)
            {
                string comando = Console.ReadLine();

                switch (comando.ToLower())
                {
                    case "neworder":
                        myApp.SendNewOrder("Client", "Server");
                        break;
                    default:
                        break;
                }
                System.Threading.Thread.Sleep(1000);
            }
            initiator.Stop();
        }
    }
}
