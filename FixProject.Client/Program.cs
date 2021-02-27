using System;
using Application;
using QuickFix;
using QuickFix.Transport;

namespace FixProject.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            SessionSettings settings = new SessionSettings(args[0]);
            IApplication myApp = new MyQuickFixApp();
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

                switch (comando)
                {
                    default:
                        break;
                }
                System.Threading.Thread.Sleep(1000);
            }
            initiator.Stop();
        }
    }
}
