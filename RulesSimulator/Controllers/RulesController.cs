using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RulesSimulator.Models;
using System.Data;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Http;
using Azure.Core;
using Newtonsoft.Json.Linq;

using System.Net;





namespace RulesSimulator.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        private readonly RuleContext ruleContext;

        public RulesController(RuleContext ruleContext)
        {
            this.ruleContext = ruleContext;
            
        }

        [HttpGet]
        [Route("GetRules")]
        public List<Rules> GetRules()
        {

            return ruleContext.Rules.ToList();
        }
        [HttpGet]
        [Route("GetRule")]
        public Rules GetRule(int id) {
            return ruleContext.Rules.Where(x => x.ID == id ).FirstOrDefault();
        
        }

        [HttpPost]
        [Route("AddRules")]
        public string AddRule(Rules rules)
        {
            string response = string.Empty;
            ruleContext.Rules.Add(rules);
            ruleContext.SaveChanges();
            return "Rule Added";

        }
        /*private static List<string> messages = new List<string>();

        [HttpPost("endpoint")]
        public IActionResult Post([FromBody] string message)
        {
            // Log the message or do something with it
            Console.WriteLine("Received message: " + message);

            // Store the message
            messages.Add(message);

            // Return a success status code
            return Ok();
        }

        [HttpGet("endpoint")]
        public IActionResult Get()
        {
            // Return all messages
            return Ok(messages);
        }*/
    

    /*private static JObject jsonData = new JObject();
    [HttpPost("endpoint")]
    public IActionResult PostJsonFile([FromBody] JObject json)
    {
        jsonData = json;
        return Ok();
    }
    [HttpGet("endpoint")]
    public IActionResult GetJsonFile()
    {
        return Ok(jsonData);
    }

*/

    public static string ReadJsonFile(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                return json;
            }
        }
        /*[HttpGet("endpoint")]
        public IActionResult GetJsonFile()
        {
            //string path = @"C:\\Users\\MHlaili\\Desktop\\json\\fix.json";
            *//*string json = System.IO.File.ReadAllText(path);
            var token = JToken.Parse(json); // Parse the JSON string into a JToken

            if (token.Type == JTokenType.String)
            {
                // If the JToken is a string, parse it again into a JObject
                var jsonObject = JObject.Parse(token.ToString());
                string symbol = jsonObject["Body"]["55"].ToString(); // Access the 'symbol' field
                return Ok(symbol);
            }
            else
            {
                return BadRequest("The JSON content is not a string that represents a JSON object.");
            }*//*
            var stream = System.IO.File.OpenRead(path);
            return File(stream, "application/json");
        }*/


        /*[HttpGet("endpoint")]
        public HttpResponseMessage GetJsonFile()
        {
            string path = @"C:\Users\MHlaili\Desktop\json\fix.json";
            string json = ReadJsonFile(path);
            var jsonObject = JObject.Parse(json); // Parse the JSON string into a JObject
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
            return response;
        }*/

        /*[HttpGet("endpoint")]
        public HttpResponseMessage GetJsonFile()
        {
            string path = @"C:\Users\MHlaili\Desktop\json\fix.json";
            string json = ReadJsonFile(path);
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }*/

        /*[HttpGet("endpoint")]
        public IActionResult GetJsonFile()
        {
            string path = @"C:\Users\MHlaili\Desktop\json\fix.json";
            string json = ReadJsonFile(path);
            return Ok(json);
        }*/


        /*[HttpGet("endpoint")]
        public IActionResult ReceiveJsonData([FromQuery] string orderMessage)
        {
            try
            {
                // Log the received order message
                Console.WriteLine($"Received order message: {orderMessage}");

                // Return the order message in the response body
                return Ok(orderMessage);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a bad request response
                Console.WriteLine($"Error processing order message: {ex.Message}");
                return BadRequest("Error processing order message");
            }
        }*/
        /*[HttpPost("endpoint")]
        public IActionResult ReceiveJsonData([FromBody] dynamic jsonData)
        {
            // Log the received JSON data
            Console.WriteLine($"Received JSON data: {jsonData}");

            // You can also process the JSON data further as needed

            return Ok(); // Return 200 OK response
        }*/

        [HttpPut]
        [Route("UpdateRule")]
        public string UpdateRule(Rules rule)
        {
            ruleContext.Entry(rule).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            ruleContext.SaveChanges();

            return "Rule Updated";
        }
        [HttpDelete]
        [Route("DeleteRule")]
        public string DeleteRule(int id) { 
            Rules rule = ruleContext.Rules.Where(x => x.ID==id).FirstOrDefault();
            if (rule != null)
            {
                ruleContext.Rules.Remove(rule);
                ruleContext.SaveChanges();
                return "Rule Deleted";

            }
            else
            {
                return "No Rule Found !";

            }
            
        
        }

    }
   
}
