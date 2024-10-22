using BidHub_Poject.DTO;
using BidHub_Poject.Models;
using Microsoft.AspNetCore.Mvc;

namespace BidHub_Poject.Controllers
{
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
    }
}
