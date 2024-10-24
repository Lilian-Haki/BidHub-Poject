using BidHub_Poject.DTO;
using BidHub_Poject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BidHub_Poject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BViewingController : Controller
    {
        private readonly BidHubDbContext _context;

        public BViewingController(BidHubDbContext context)
        {
            _context = context;
        }


        [HttpPost("viewing-date")]
        public async Task<IActionResult> BViewing([FromBody] BViewingDTO bidViewing)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bidview = new BViewing
            {
                Date = bidViewing.Date,
                Email = bidViewing.Email,
                ProductId = bidViewing.ProductId // This should now exist in the Products table


            };
            _context.BViewings.Add(bidview);
            await _context.SaveChangesAsync();

            return Ok();

        }
        // GET: api/Auctioneers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BViewingDTO>>> GetBViewing()
        {
            var bidViewing = await _context.BViewings
                .Select(a => new BViewingDTO
                {
                    Date = a.Date,
                    Email = a.Email,
                    ProductId = a.ProductId
                })
                .ToListAsync();

            return Ok(bidViewing);
        }

        // GET: api/Auctioneers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BViewingDTO>> GetBViewing(int id)
        {
            var bidViewingg = await _context.BViewings.FindAsync(id);

            if (bidViewingg == null)
            {
                return NotFound();
            }

            var bViewinng = new BViewingDTO
            {

                Date = bidViewingg.Date,
                Email = bidViewingg.Email,
                ProductId = bidViewingg.ProductId
            };

            return Ok(bViewinng);
        }

        // PUT: api/Auctioneers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBViewing(int id, BViewingDTO bidViewingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bidViewing = await _context.BViewings.FindAsync(id);
            if (bidViewing == null)
            {
                return NotFound();
            }

            bidViewing.Date = bidViewingDTO.Date;
            bidViewing.Email = bidViewingDTO.Email;
            bidViewing.ProductId = bidViewingDTO.ProductId;

            _context.Entry(bidViewing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.BViewings.Any(e => e.ViewingId == id))
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

        // DELETE: api/Auctioneers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBViewing(int id)
        {
            var bidViewing = await _context.BViewings.FindAsync(id);
            if (bidViewing == null)
            {
                return NotFound();
            }

            _context.BViewings.Remove(bidViewing);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
