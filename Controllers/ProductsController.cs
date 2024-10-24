using BidHub_Poject.DTO;
using BidHub_Poject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly BidHubDbContext _context;

    public ProductsController(BidHubDbContext context)
    {
        _context = context;
    }


    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductsDTO productDTO)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var product = new Products
        {
            ProductName = productDTO.ProductName,
            ReasonForAuction = productDTO.ReasonForAuction,
            OwnersName = productDTO.OwnersName,
            OwnerPhoneNo = productDTO.OwnerPhoneNo,
            ReservePrice = productDTO.ReservePrice,
            Location = productDTO.Location,
            // Map other properties
            //Documents = new List<ProductDocuments>()
            //ProductPhoto = ProductPhotos // Initialize empty collections
        };

        //   If you have uploaded photos / documents, handle them here
        //   For example, if using multipart/ form - data
        //Or handle separate endpoints for uploading photos/ documents

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
    }

    // GET: api/Auctioneers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductsDTO>>> GetProduct()
    {
        var product = await _context.Products
            .Select(a => new ProductsDTO
            {
                ProductName = a.ProductName,
                ReasonForAuction = a.ReasonForAuction,
                OwnersName = a.OwnersName,
                OwnerPhoneNo = a.OwnerPhoneNo,
                ReservePrice = a.ReservePrice,
                Location = a.Location
            })
            .ToListAsync();

        return Ok(product);
    }

    // GET: api/Auctioneers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductsDTO>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        var productDTO = new ProductsDTO
        {

            ProductName = product.ProductName,
            ReasonForAuction = product.ReasonForAuction,
            OwnersName = product.OwnersName,
            OwnerPhoneNo = product.OwnerPhoneNo,
            ReservePrice = product.ReservePrice,
            Location = product.Location
        };

        return Ok(productDTO);
    }

    // PUT: api/Auctioneers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, ProductsDTO productDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var products = await _context.Products.FindAsync(id);
        if (products == null)
        {
            return NotFound();
        }

        products.ProductName = productDTO.ProductName;
        products.ReasonForAuction = productDTO.ReasonForAuction;
        products.OwnersName = productDTO.OwnersName;
        products.OwnerPhoneNo = productDTO.OwnerPhoneNo;
        products.ReservePrice = productDTO.ReservePrice;
        products.Location = productDTO.Location;

        _context.Entry(products).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Products.Any(e => e.ProductId == id))
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
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var products = await _context.Products.FindAsync(id);
        if (products == null)
        {
            return NotFound();
        }

        _context.Products.Remove(products);
        await _context.SaveChangesAsync();

        return NoContent();
    }




}
