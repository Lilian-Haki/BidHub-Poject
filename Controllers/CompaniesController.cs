//using BidHub_Poject.DTO;
//using BidHub_Poject.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;

//[Route("api/[controller]")]
//[ApiController]
//public class CompaniesController : ControllerBase
//{
//    private readonly BidHubDbContext _context;

//    public CompaniesController(BidHubDbContext context)
//    {
//        _context = context;
//    }


//    // POST: api/Companies
//    [HttpPost("company")]
//    public async Task<ActionResult<CompaniesDTO>> PostAuctioneer(CompaniesDTO companiesDTO)
//    {
//        if (!ModelState.IsValid)
//        {
//            return BadRequest(ModelState);
//        }

//        var company = new Company
//        {
//            CompanyName = companiesDTO.CompanyName,
//            Location = companiesDTO.Location,
//            Company_url = companiesDTO.Company_url,
//            // Assuming the user, company, and product IDs are provided in the request
//            //Auctioneers = new List<Auctioneers>()
//        };

//        _context.Companies.Add(company);
//        await _context.SaveChangesAsync();

//        return CreatedAtAction("GetCompany", new { id = company.CompanyId }, company);
//    }

//    // GET: api/Auctioneers
//    [HttpGet]
//    public async Task<ActionResult<IEnumerable<BidDatesDTO>>> GetBidDates()
//    {
//        var biddates = await _context.BidDates
//            .Select(a => new BidDatesDTO
//            {
//                StartDate = a.StartDate,
//                EndDate = a.EndDate,
//                ProductId = a.ProductId
//            })
//            .ToListAsync();

//        return Ok(biddates);
//    }

//    // GET: api/Auctioneers/5
//    [HttpGet("{id}")]
//    public async Task<ActionResult<BidDatesDTO>> GetBidDates(int id)
//    {
//        var biddates = await _context.BidDates.FindAsync(id);

//        if (biddates == null)
//        {
//            return NotFound();
//        }

//        var bidDatesDTO = new BidDatesDTO
//        {

//            StartDate = biddates.StartDate,
//            EndDate = biddates.EndDate,
//            ProductId = biddates.ProductId
//        };

//        return Ok(bidDatesDTO);
//    }

//    // PUT: api/Auctioneers/5
//    [HttpPut("{id}")]
//    public async Task<IActionResult> PutAuctioneer(int id, BidDatesDTO bidDatesDTO)
//    {
//        if (!ModelState.IsValid)
//        {
//            return BadRequest(ModelState);
//        }

//        var biddates = await _context.BidDates.FindAsync(id);
//        if (biddates == null)
//        {
//            return NotFound();
//        }

//        biddates.StartDate = bidDatesDTO.StartDate;
//        biddates.EndDate = bidDatesDTO.EndDate;
//        biddates.ProductId = bidDatesDTO.ProductId;

//        _context.Entry(biddates).State = EntityState.Modified;

//        try
//        {
//            await _context.SaveChangesAsync();
//        }
//        catch (DbUpdateConcurrencyException)
//        {
//            if (!_context.BidDates.Any(e => e.BidDateId == id))
//            {
//                return NotFound();
//            }
//            else
//            {
//                throw;
//            }
//        }

//        return NoContent();
//    }

//    // DELETE: api/Auctioneers/5
//    [HttpDelete("{id}")]
//    public async Task<IActionResult> DeleteAuctioneer(int id)
//    {
//        var Biddates = await _context.BidDates.FindAsync(id);
//        if (Biddates == null)
//        {
//            return NotFound();
//        }

//        _context.BidDates.Remove(Biddates);
//        await _context.SaveChangesAsync();

//        return NoContent();
//    }


//}
