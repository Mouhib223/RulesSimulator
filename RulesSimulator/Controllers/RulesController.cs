using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RulesSimulator.Models;
using System.Data;

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
