using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RulesSimulator.Models;

namespace RulesSimulator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Rules1Controller : ControllerBase
    {
        private readonly RuleContext _context;

        public Rules1Controller(RuleContext context)
        {
            _context = context;
        }

        // GET: api/Rules1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rules>>> GetRules()
        {
            return await _context.Rules.ToListAsync();
        }

        // GET: api/Rules1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rules>> GetRules(int id)
        {
            var rules = await _context.Rules.FindAsync(id);

            if (rules == null)
            {
                return NotFound();
            }

            return rules;
        }

        // PUT: api/Rules1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRules(int id, Rules rules)
        {
            if (id != rules.ID)
            {
                return BadRequest();
            }

            _context.Entry(rules).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RulesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rules1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rules>> PostRules(Rules rules)
        {
            _context.Rules.Add(rules);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRules", new { id = rules.ID }, rules);
        }

        // DELETE: api/Rules1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRules(int id)
        {
            var rules = await _context.Rules.FindAsync(id);
            if (rules == null)
            {
                return NotFound();
            }

            _context.Rules.Remove(rules);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RulesExists(int id)
        {
            return _context.Rules.Any(e => e.ID == id);
        }
    }
}
