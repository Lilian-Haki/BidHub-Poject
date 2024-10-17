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
    [HttpPost("{productId}/photos")]
    public async Task<IActionResult> UploadPhoto(int productId, IFormFile file)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
            return NotFound();

        // Save the file to your storage (e.g., file system, cloud storage)
        var filePath = await SaveFileAsync(file);

        var photo = new ProductPhotos
        {
            PhotoUrl = filePath,
            PhotoId = productId
        };

        _context.ProductPhotos.Add(photo);
        await _context.SaveChangesAsync();

        return Ok(photo);
    }

    //[HttpPost("{productId}/documents")]
    //public async Task<IActionResult> UploadDocument(int productId, IFormFile file)
    //{
    //    var product = await _context.Products.FindAsync(productId);
    //    if (product == null)
    //        return NotFound();

    //    // Save the file to your storage
    //    var filePath = await SaveFileAsync(file);

    //    var document = new Document
    //    {
    //        FilePath = filePath,
    //        ProductId = productId
    //    };

    //    _context.Documents.Add(document);
    //    await _context.SaveChangesAsync();

    //    return Ok(document);
    //}

    private async Task<string> SaveFileAsync(IFormFile file)
    {
        // Implement your file saving logic here
        // For example, save to wwwroot/uploads and return the relative path
        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
        if (!Directory.Exists(uploads))
            Directory.CreateDirectory(uploads);

        var filePath = Path.Combine(uploads, file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return $"/uploads/{file.FileName}";
    }

}
