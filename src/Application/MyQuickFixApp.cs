using System;
using QuickFix;
using QuickFix.Fields;

namespace Application
{
    public class MyQuickFixApp : MessageCracker, IApplication
    {
        public void OnMessage(QuickFix.FIX44.NewOrderSingle msg, SessionID sessionID)
        {
            Console.WriteLine($"Session: {sessionID} - {msg.ToString()}");
        }

        public void OnMessage(QuickFix.FIX44.Heartbeat msg, SessionID sessionID)
        {
            Console.WriteLine($"Session: {sessionID} - {msg.ToString()}");
        }

        public void FromApp(Message msg, SessionID sessionID)
        {
            Console.WriteLine($"Session: {sessionID} - {msg.ToString()}");
            Crack(msg, sessionID);
        }
        public void OnCreate(SessionID sessionID) { Console.WriteLine($"Session: {sessionID}"); }
        public void OnLogout(SessionID sessionID) { Console.WriteLine($"Session: {sessionID}"); }
        public void OnLogon(SessionID sessionID) { Console.WriteLine($"Session: {sessionID}"); }
        public void FromAdmin(Message msg, SessionID sessionID) { Console.WriteLine($"Session: {sessionID} - {msg.ToString()}"); }
        public void ToAdmin(Message msg, SessionID sessionID) { Console.WriteLine($"Session: {sessionID} - {msg.ToString()}"); }
        public void ToApp(Message msg, SessionID sessionID) { Console.WriteLine($"Session: {sessionID} - {msg.ToString()}"); }

        public void EnviarParaTodos(string linha)
        {

        }

        public void SendNewOrder(string sender, string target)
        {
            var order = new QuickFix.FIX44.NewOrderSingle(
                new ClOrdID("1234"),
                new Symbol("AAPL"),
                new Side(Side.BUY),
                new TransactTime(DateTime.Now),
                new OrdType(OrdType.LIMIT));

            order.Header.SetField(new SenderCompID(sender));
            order.Header.SetField(new TargetCompID(target));

            order.Price = new Price(new decimal(22.4));
            order.Account = new Account("18861112");

            Session.SendToTarget(order);
        }
    }
}
