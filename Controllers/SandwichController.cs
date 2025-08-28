using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPISandwich.Model;

namespace WebAPISandwich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SandwichController : ControllerBase
    {
        private readonly SandwichContext _context;

        public SandwichController(SandwichContext context)
        {
            _context = context;
        }

        // GET: api/Sandwich
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sandwich>>> GetSandswiches()
        {
            return await _context.Sandwiches.ToListAsync();
        }

        // GET: api/Sandwich/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sandwich>> GetSandwich(int id)
        {
            var sandwich = await _context.Sandwiches.FindAsync(id);

            if (sandwich == null)
            {
                return NotFound();
            }

            return sandwich;
        }

        // PUT: api/Sandwiche/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSandwich(int id, Sandwich sandwich)
        {
            if (id != sandwich.Id)
            {
                return BadRequest();
            }

            _context.Entry(sandwich).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SandwichExists(id))
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

        // POST: api/Sandwiche
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sandwich>> PostSandwich(Sandwich sandwich)
        {
            _context.Sandwiches.Add(sandwich);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSandwich", new { id = sandwich.Id }, sandwich);
        }

        // DELETE: api/Sandwiche/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSandwich(int id)
        {
            var sandwich = await _context.Sandwiches.FindAsync(id);
            if (sandwich == null)
            {
                return NotFound();
            }

            _context.Sandwiches.Remove(sandwich);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SandwichExists(int id)
        {
            return _context.Sandwiches.Any(e => e.Id == id);
        }
    }
}