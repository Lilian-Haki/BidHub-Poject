using BidHub_Poject.DTO;
using BidHub_Poject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

[Route("api/[controller]")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly BidHubDbContext _context;

    public CompaniesController(BidHubDbContext context)
    {
        _context = context;
    }


    // POST: api/Companies
    [HttpPost("company")]
    public async Task<ActionResult<CompaniesDTO>> PostAuctioneer(CompaniesDTO companiesDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var company = new Company
        {
            CompanyName = companiesDTO.CompanyName,
            Location = companiesDTO.Location,
            Company_url = companiesDTO.Company_url,
            // Assuming the user, company, and product IDs are provided in the request
            //Auctioneers = new List<Auctioneers>()
        };

        _context.Companies.Add(company);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCompany", new { id = company.CompanyId }, company);
    }

    // Get method
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompany(int id)
    {
        var company = await _context.Companies
                                    //.Include(p => p.ProductPhoto)
                                    //.Include(p => p.Documents)
                                    .FirstOrDefaultAsync(p => p.CompanyId == id);
        if (company == null)
            return NotFound();

        return Ok(company);
    }



}
