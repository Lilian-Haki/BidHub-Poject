using BidHub_Poject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

[Route("api/[controller]")]
[ApiController]
public class ProductDocumentsController : ControllerBase
{
    private readonly BidHubDbContext _context;

    public ProductDocumentsController(BidHubDbContext context)
    {
        _context = context;
    }

    // GET: api/ProductDocuments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDocuments>>> GetProductDocuments()
    {
        return await _context.ProductDocuments.ToListAsync();
    }

    // GET: api/ProductDocuments/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDocuments>> GetProductDocument(int id)
    {
        var document = await _context.ProductDocuments.FindAsync(id);
        if (document == null)
        {
            return NotFound();
        }
        return document;
    }

    // PUT: api/ProductDocuments/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProductDocument(int id, ProductDocuments document)
    {
        if (id != document.DocumentId)
        {
            return BadRequest();
        }

        _context.Entry(document).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ProductDocuments.Any(e => e.DocumentId == id))
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

    // POST: api/ProductDocuments
    [HttpPost]
    public async Task<ActionResult<ProductDocuments>> PostProductDocument(ProductDocuments document)
    {
        _context.ProductDocuments.Add(document);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProductDocument", new { id = document.DocumentId }, document);
    }

    // DELETE: api/ProductDocuments/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductDocument(int id)
    {
        var document = await _context.ProductDocuments.FindAsync(id);
        if (document == null)
        {
            return NotFound();
        }

        _context.ProductDocuments.Remove(document);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
