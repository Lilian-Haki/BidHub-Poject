using BidHub_Poject.DTO;
using BidHub_Poject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BidHub_Poject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidDatesController : Controller
    {
        private readonly BidHubDbContext _context;

        public BidDatesController(BidHubDbContext context)
        {
            _context = context;
        }
       

        [HttpPost("date-range")]
        public async Task<IActionResult> BidDates([FromBody] BidDatesDTO bidDates)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var biddates = new BidDates
            {
                StartDate = bidDates.StartDate,
                EndDate = bidDates.EndDate,
                ProductId = bidDates.ProductId // This should now exist in the Products table


            };
            _context.BidDates.Add(biddates);
            await _context.SaveChangesAsync();

            return Ok();

            //return CreatedAtAction(nameof(GetProduct), new { id = biddates.BidDateId }, biddates);
        }
        // GET: api/Auctioneers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BidDatesDTO>>> GetBidDates()
        {
            var biddates = await _context.BidDates
                .Select(a => new BidDatesDTO
                {
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    ProductId = a.ProductId
                })
                .ToListAsync();

            return Ok(biddates);
        }

        // GET: api/Auctioneers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BidDatesDTO>> GetBidDates(int id)
        {
            var biddates = await _context.BidDates.FindAsync(id);

            if (biddates == null)
            {
                return NotFound();
            }

            var bidDatesDTO = new BidDatesDTO
            {

                StartDate = biddates.StartDate,
                EndDate = biddates.EndDate,
                ProductId = biddates.ProductId
            };

            return Ok(bidDatesDTO);
        }

        // PUT: api/Auctioneers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuctioneer(int id, BidDatesDTO bidDatesDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var biddates = await _context.BidDates.FindAsync(id);
            if (biddates == null)
            {
                return NotFound();
            }

            biddates.StartDate = bidDatesDTO.StartDate;
            biddates.EndDate = bidDatesDTO.EndDate;
            biddates.ProductId = bidDatesDTO.ProductId;

            _context.Entry(biddates).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.BidDates.Any(e => e.BidDateId == id))
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
        public async Task<IActionResult> DeleteAuctioneer(int id)
        {
            var Biddates = await _context.BidDates.FindAsync(id);
            if (Biddates == null)
            {
                return NotFound();
            }

            _context.BidDates.Remove(Biddates);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

