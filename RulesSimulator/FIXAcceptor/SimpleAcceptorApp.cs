using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Data.SqlClient;
using QuickFix;
using QuickFix.Fields;
using RulesSimulator.FIXAcceptor;
using RulesSimulator.Models;

namespace BrokerSimulator.FIXAcceptor
{
    /// <summary>
    /// Just a simple server that will let you connect to it and ignore any
    /// application-level messages you send to it.
    /// Note that this app is *NOT* a message cracker.
    /// </summary>

    public class SimpleAcceptorApp : QuickFix.MessageCracker, QuickFix.IApplication
    {
        #region QuickFix.Application Methods
        /* public Rules SomeMethod(string symbol)
         {
             var con = ConfigurationManager.ConnectionStrings["DbCon"].ToString();

             Person matchingPerson = new Person();
             using (SqlConnection myConnection = new SqlConnection(con))
             {
                 string oString = "Select * from Employees where FirstName=@fName";
                 SqlCommand oCmd = new SqlCommand(oString, myConnection);
                 oCmd.Parameters.AddWithValue("@Fname", fName);
                 myConnection.Open();
                 using (SqlDataReader oReader = oCmd.ExecuteReader())
                 {
                     while (oReader.Read())
                     {
                         matchingPerson.firstName = oReader["FirstName"].ToString();
                         matchingPerson.lastName = oReader["LastName"].ToString();
                     }

                     myConnection.Close();
                 }
             }
             return matchingPerson;
         }*/
        static public bool IsMatching(QuickFix.FIX44.NewOrderSingle order)
        {
            string connectionString = "server=TN1PFE-008\\SQLEXPRESS; database= RulesDB; Integrated Security=true; TrustServerCertificate=True"; // Replace with your actual connection string
            string query = "SELECT * FROM RulesDB.dbo.Rules";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Rules rule = new Rules
                        {
                            ID = reader.GetInt32(0),
                            symbol = reader.GetString(1),
                            Description = reader.GetString(2),
                            MinPrice = reader.GetDecimal(3),
                            MaxPrice = reader.GetDecimal(4),
                            MinQty = reader.GetDecimal(5),
                            MaxQty = reader.GetDecimal(6),
                            ruleTypeID = reader.GetInt32(7)
                        };

                        Console.WriteLine($"Id: {rule.ID}, Symbol: {rule.symbol},Description: {rule.Description}, RuleTypeID: {rule.ruleTypeID}, " +
                                          $"MinPrice: {rule.MinPrice}, MaxPrice: {rule.MaxPrice}, MinQty: {rule.MinQty}, MaxQty: {rule.MaxQty}");
                        if (rule.symbol == order.GetString(QuickFix.Fields.Tags.Symbol))
                        {
                            Console.WriteLine("Rule Matched !!");
                            Console.WriteLine("the symbol is " + rule.symbol +"===>"+ rule.Description);
                            return true;
                        }
                        else { Console.WriteLine("Not matcheed"); }
                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return false;
            }
        }
        public void Reject_Security(string x)
        {
            if (x == "IBM")
            {
                Console.WriteLine("");
            }
        }
        public void FromApp(Message message, SessionID sessionID)
        {
            Console.WriteLine("IN:  " + message);
            string fixMessage = message.ToString();

            // The FIX message fields are separated by '\x01' character
            string[] fields = fixMessage.Split('\x01');

            // Iterate through the fields to find the Symbol (tag 55)
            foreach (string field in fields)
            {
                // Each field is in the format Tag=Value
                string[] tagValuePair = field.Split('=');

                // Check if we have a valid tag-value pair
                if (tagValuePair.Length == 2)
                {
                    string tag = tagValuePair[0];
                    string value = tagValuePair[1];

                    // Check if the tag is 55 (Symbol)
                    if (tag == "55")
                    {
                        Console.WriteLine("Symbol: " + value);
                        break;
                       
                    }
                    
                }
            }
            Crack(message, sessionID);

            /*// Send JSON to API
            string result = "";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "text/plain";
                result = client.UploadString("http://localhost:5268/api/Rules/endpoint", "POST", message.ToString());
            }*/

        }

        public void ToApp(Message message, SessionID sessionID)
        {
            Console.WriteLine("OUT: " + message);
            
            /* string result = "";
             using (var client = new WebClient())
             {
                 client.Headers[HttpRequestHeader.ContentType] = "text/plain";
                 result = client.UploadString("http://localhost:5268/api/Rules/endpoint", "POST", message.ToString());
             }*/
        }

        public void FromAdmin(Message message, SessionID sessionID) 
        {
            Console.WriteLine("IN:  " + message);
        }

        public void ToAdmin(Message message, SessionID sessionID)
        {
            Console.WriteLine("OUT:  " + message);
        }

        public void OnCreate(SessionID sessionID) { }
        public void OnLogout(SessionID sessionID) { }
        public void OnLogon(SessionID sessionID) { }
        public void OnMessage(QuickFix.FIX44.NewOrderSingle order, SessionID sessionID)
        {
            if (IsMatching(order)) { Console.WriteLine("This Order is Matching a Rule !"); }
            else { Console.WriteLine("No Rule Match This Order"); }
            Console.WriteLine("Fix Message Recived : ");
            Console.WriteLine("Fix Message Recived : ");
            
            if (order.Header.IsSetField(QuickFix.Fields.Tags.SenderCompID))
            {
                string senderCompID = order.Header.GetString(QuickFix.Fields.Tags.SenderCompID);
                Console.WriteLine("SenderCompID: \t" + senderCompID );
            }

            // TargetCompID
            if (order.Header.IsSetField(QuickFix.Fields.Tags.TargetCompID))
            {
                string targetCompID = order.Header.GetString(QuickFix.Fields.Tags.TargetCompID);
                Console.WriteLine("TargetCompID: \t" + targetCompID);
            }

            // MsgType
            if (order.Header.IsSetField(QuickFix.Fields.Tags.MsgType))
            {
                string msgType = order.Header.GetString(QuickFix.Fields.Tags.MsgType);
                Console.WriteLine("MsgType: \t" + msgType);
                if(msgType == "D") { Console.WriteLine("D is a New Order - Single"); }
                if(msgType == "0") { Console.WriteLine("0 is a Heartbeat"); }
                if(msgType == "5") { Console.WriteLine("5 is a Logout"); }
                if(msgType == "A") { Console.WriteLine("A is a Logon"); }
                if(msgType == "F") { Console.WriteLine("D is an Order Cancel Request"); }
                if(msgType == "8") { Console.WriteLine("D is an Execution Report"); }
            }

            // MsgSeqNum
            if (order.Header.IsSetField(QuickFix.Fields.Tags.MsgSeqNum))
            {
                int msgSeqNum = order.Header.GetInt(QuickFix.Fields.Tags.MsgSeqNum);
                Console.WriteLine("MsgSeqNum: \t" + msgSeqNum);
            }

            // SendingTime
            if (order.Header.IsSetField(QuickFix.Fields.Tags.SendingTime))
            {
                DateTime sendingTime = order.Header.GetDateTime(QuickFix.Fields.Tags.SendingTime);
                Console.WriteLine("SendingTime: \t" + sendingTime);
            }
            
            if (order.IsSetField(QuickFix.Fields.Tags.ClOrdID))
            {
                string clOrdID = order.GetString(QuickFix.Fields.Tags.ClOrdID);
                Console.WriteLine("ClOrdID: \t" + clOrdID);
            }

            // Side
            if (order.IsSetField(QuickFix.Fields.Tags.Side))
            {
                char side = order.GetChar(QuickFix.Fields.Tags.Side);
                Console.WriteLine("Side: \t" + side);
            }

            // TransactTime
            if (order.IsSetField(QuickFix.Fields.Tags.TransactTime))
            {
                DateTime transactTime = order.GetDateTime(QuickFix.Fields.Tags.TransactTime);
                Console.WriteLine("TransactTime: \t" + transactTime);
            }

            // OrderQty
            if (order.IsSetField(QuickFix.Fields.Tags.OrderQty))
            {
                decimal orderQty = order.GetDecimal(QuickFix.Fields.Tags.OrderQty);
                Console.WriteLine("OrderQty: \t" + orderQty);
            }

            // OrdType
            if (order.IsSetField(QuickFix.Fields.Tags.OrdType))
            {
                char ordType = order.GetChar(QuickFix.Fields.Tags.OrdType);
                Console.WriteLine("OrdType: \t" + ordType);
            }

            // Price
            if (order.IsSetField(QuickFix.Fields.Tags.Price))
            {
                decimal price = order.GetDecimal(QuickFix.Fields.Tags.Price);
                Console.WriteLine("Price: \t" + price);
            }

            // Symbol
            if (order.IsSetField(QuickFix.Fields.Tags.Symbol))
            {
                string symbol = order.GetString(QuickFix.Fields.Tags.Symbol);
                Console.WriteLine("Symbol: \t" + symbol);
            }
            //Quantity
            /*if (order.IsSetField(QuickFix.Fields.Tags.Quantity))
            {
                string Quantity = order.GetString(QuickFix.Fields.Tags.Quantity);
                Console.WriteLine("Quantity: \t" + Quantity);
            }*/

            /*Console.WriteLine("GGGGGGG");*/

            Console.WriteLine("the symbol is " + order.Symbol + " : I will Execute Partially !");

            Session.SendToTarget(order, sessionID);
            //ToApp(null, sessionID);

        }
        
        #endregion
    }
}