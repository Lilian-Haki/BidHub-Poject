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

    // Example Get method
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _context.Products
                                    //.Include(p => p.ProductPhoto)
                                    //.Include(p => p.Documents)
                                    .FirstOrDefaultAsync(p => p.ProductId == id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }
   
   

   

}
