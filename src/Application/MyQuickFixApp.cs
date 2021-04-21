using System;
using System.Threading;
using QuickFix;
using QuickFix.Fields;

namespace Application
{
    public class MyQuickFixApp : MessageCracker, IApplication
    {
        public MyQuickFixApp(SessionSettings settings)
        {
            Settings = settings;
        }

        public SessionSettings Settings { get; }

        public void OnMessage(QuickFix.FIX44.NewOrderSingle msg, SessionID sessionID)
        {
            Console.WriteLine($"Session: {sessionID} - {msg.ToString()}");
        }

        public void FromApp(Message msg, SessionID sessionID)
        {
            Console.WriteLine($"Session: {sessionID} - {msg.ToString()}");
            Crack(msg, sessionID);
        }
        public void ToApp(Message msg, SessionID sessionID)
        {
            Console.WriteLine(nameof(ToApp));
            Console.WriteLine($"Session: {sessionID} - {msg.ToString()}");
            Crack(msg, sessionID);
        }
        public void OnCreate(SessionID sessionID) { Console.WriteLine($"Session: {sessionID} - {sessionID.GetHashCode()}"); }
        public void OnLogout(SessionID sessionID) { Console.WriteLine($"Session: {sessionID}"); }
        public void OnLogon(SessionID sessionID) { Console.WriteLine($"Session: {sessionID}"); }
        public void FromAdmin(Message msg, SessionID sessionID)
        {
            if (msg is QuickFix.FIX44.Logon logon && Settings.Get(sessionID).Has("RawData"))
            {
                var rawData = Settings.Get(sessionID).GetString("RawData");
                if (!string.IsNullOrWhiteSpace(rawData) && rawData != logon.GetString(QuickFix.Fields.RawData.TAG))
                {
                    Session.SendToTarget(new QuickFix.FIX44.Reject() { SessionRejectReason = new SessionRejectReason(99), Text = new Text("Wrong password") }, new SessionID(sessionID.BeginString, sessionID.SenderCompID, sessionID.TargetCompID));
                }
            }
            Console.WriteLine($"[{nameof(FromAdmin)}] Session: {sessionID} - {msg.ToString()} - {Thread.CurrentThread.ManagedThreadId}:{Thread.CurrentThread.Name}");
        }
        public void ToAdmin(Message msg, SessionID sessionID)
        {
            if (msg is QuickFix.FIX44.Logon && Settings.Get(sessionID).Has("RawData"))
            {
                var rawData = Settings.Get(sessionID).GetString("RawData");
                if (!string.IsNullOrWhiteSpace(rawData))
                {
                    msg.SetField(new QuickFix.Fields.RawData(rawData));
                    msg.SetField(new QuickFix.Fields.RawDataLength(rawData.Length));
                }
            }
            Console.WriteLine($"[{nameof(ToAdmin)}] Session: {sessionID} - {msg.ToString()}");
        }

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
