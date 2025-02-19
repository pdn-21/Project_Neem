using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Data;
using System.Linq;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RightsTypeController : ControllerBase
    {
        private readonly AppDbContext _context;
        public RightsTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RightsType
        [HttpGet]
        public async Task<IActionResult> GetAllRightsTypes()
        {
            var rightsTypes = await _context.RightsTypes.ToListAsync();
            return Ok(rightsTypes);
        }

        // GET: api/RightsType/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRightsTypeById(int id)
        {
            var rightsType = await _context.RightsTypes.FirstOrDefaultAsync(r => r.TypeId == id);
            if (rightsType == null)
            {
                return NotFound();
            }
            return Ok(rightsType);
        }

        // POST: api/RightsType
        [HttpPost]
        public async Task<IActionResult> CreateRightsType([FromBody] RightsType rightsType)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            rightsType.CreatedAt = DateTime.UtcNow;
            _context.RightsTypes.Add(rightsType);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRightsTypeById), new { id = rightsType.TypeId}, rightsType);
        }

        // PUT: api/RightsType/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRightsType(int id, [FromBody] RightsType rightsType)
        {
            if (id != rightsType.TypeId) return BadRequest();

            rightsType.UpdatedAt = DateTime.UtcNow;
            _context.Entry(rightsType).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.RightsTypes.Any(r => r.TypeId == id))
                {
                    return NotFound();
                } else throw;
            }
            return NoContent();
        }

        // DELETE: api/RightsType/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRightsType(int id)
        {
            var rightsType = await _context.RightsTypes.FindAsync(id);
            if (rightsType == null) return NotFound();

            _context.RightsTypes.Remove(rightsType);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}