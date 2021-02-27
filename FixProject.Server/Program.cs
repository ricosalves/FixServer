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

            myApp = new MyQuickFixApp();
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
                    case "enviar":
                        EnviarMensagensPorArquivo();
                        break;

                    default:
                        break;
                }

                System.Threading.Thread.Sleep(1000);
            }
            acceptor.Stop();
        }

        private static void EnviarMensagensPorArquivo()
        {
            Console.WriteLine("Favor informar o caminho do arquivo");
            var caminho = Console.ReadLine();

            var arquivoTexto = File.ReadAllText(caminho);

            foreach (var linha in arquivoTexto)
            {
                myApp.EnviarParaTodos(linha);
            }
        }
    }
}
