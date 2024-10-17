using BidHub_Poject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

[Route("api/[controller]")]
[ApiController]
public class ProductPhotosController : ControllerBase
{
    private readonly BidHubDbContext _context;

    public ProductPhotosController(BidHubDbContext context)
    {
        _context = context;
    }

    // GET: api/ProductPhotos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductPhotos>>> GetProductPhotos()
    {
        return await _context.ProductPhotos.ToListAsync();
    }

    // GET: api/ProductPhotos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductPhotos>> GetProductPhoto(int id)
    {
        var photo = await _context.ProductPhotos.FindAsync(id);
        if (photo == null)
        {
            return NotFound();
        }
        return photo;
    }

    // PUT: api/ProductPhotos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProductPhoto(int id, ProductPhotos photo)
    {
        if (id != photo.PhotoId)
        {
            return BadRequest();
        }

        _context.Entry(photo).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ProductPhotos.Any(e => e.PhotoId == id))
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

    // POST: api/ProductPhotos
    [HttpPost]
    public async Task<ActionResult<ProductPhotos>> PostProductPhoto(ProductPhotos photo)
    {
        _context.ProductPhotos.Add(photo);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProductPhoto", new { id = photo.PhotoId }, photo);
    }

    // DELETE: api/ProductPhotos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductPhoto(int id)
    {
        var photo = await _context.ProductPhotos.FindAsync(id);
        if (photo == null)
        {
            return NotFound();
        }

        _context.ProductPhotos.Remove(photo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
