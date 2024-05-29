using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RulesSimulator.FIXAcceptor
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private static QuickFix.Message _message;

        // POST api/message
        [HttpPost]
        public IActionResult PostMessage([FromBody] QuickFix.Message message)
        {
            _message = message;
            return Ok();
        }

        // GET api/message
        [HttpGet]
        public ActionResult<QuickFix.Message> GetMessage()
        {
            return _message;
        }
    }
}
