using BidHub_Poject.DTO;
using BidHub_Poject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
    [HttpPost]
    public async Task<IActionResult> CreateCompany([FromBody] CompaniesDTO companiesDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var company = new Company
        {
            CompanyName = companiesDTO.CompanyName,
            Company_url = companiesDTO.Company_url,
            Location = companiesDTO.Location,
            Status = companiesDTO.Status
            // Assuming the user, company, and product IDs are provided in the request
            //Auctioneers = new List<Auctioneers>()
        };

        _context.Companies.Add(company);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCompany), new { id = company.CompanyId }, company);

    }

    // GET: api/Auctioneers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompaniesDTO>>> GetCompany()
    {
            var company = await _context.Companies
                .Select(a => new CompaniesDTO
                {
                    CompanyName = a.CompanyName,
                    Company_url = a.Company_url,
                    Location = a.Location,
                    Status = a.Status
                   
                })
                .ToListAsync();

            return Ok(company);
     }

    // GET: api/Auctioneers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CompaniesDTO>> GetCompany(int id)
    {
        var company = await _context.Companies.FindAsync(id);

        if (company == null)
        {
            return NotFound();
        }

        var companyDTO = new CompaniesDTO
        {

            CompanyName = company.CompanyName,
            Company_url = company.Company_url,
            Location = company.Location,
            Status = company.Status
        };

        return Ok(companyDTO);
    }

    // PUT: api/Auctioneers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompany(int id, CompaniesDTO companyDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var company = await _context.Companies.FindAsync(id);
        if (company == null)
        {
            return NotFound();
        }

        company.CompanyName = companyDTO.CompanyName;
        company.Company_url = companyDTO.Company_url;
        company.Location = companyDTO.Location;
        company.Status = companyDTO.Status;

        _context.Entry(company).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Companies.Any(e => e.CompanyId == id))
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
    public async Task<IActionResult> DeleteCompany(int id)
    {
        var company = await _context.Companies.FindAsync(id);
        if (company == null)
        {
            return NotFound();
        }

        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();

        return NoContent();
    }


}
