using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

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
            Console.WriteLine("GGGGGGG");
            Console.WriteLine("Msg Recived");

            Session.SendToTarget(order, sessionID);
            //ToApp(null, sessionID);

        }
        
        #endregion
    }
}