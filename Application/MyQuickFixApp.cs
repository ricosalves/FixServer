using System;
using QuickFix;

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

        public void EnviarParaTodos(char linha)
        {
            throw new NotImplementedException();
        }
    }
}
