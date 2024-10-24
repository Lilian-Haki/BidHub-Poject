using BidHub_Poject.DTO;
using BidHub_Poject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml.Linq;

namespace BidHub_Poject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDocumentsController : Controller
    {
        private readonly BidHubDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductDocumentsController(BidHubDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("documents")] // api/main/uploadFile
        public IActionResult Upload( ProductDocDTO productdoc, string fileName)
        {
            // Validate file extension
            List<string> validExtensions = new List<string> { ".pdf", ".ppt" };
            string extension = Path.GetExtension(productdoc.DocumentUrl.FileName);
            if (!validExtensions.Contains(extension))
            {
                return BadRequest($"Extension is not valid ({string.Join(", ", validExtensions)})");
            }

            if (productdoc.DocumentUrl == null || string.IsNullOrEmpty(fileName))
            {
                return BadRequest("File or file name is missing.");
            }

            // Validate file size
            long size = productdoc.DocumentUrl.Length;
            if (size > (5 * 1024 * 1024))
            {
                return BadRequest("Maximum size can be 5MB");
            }

            // Ensure WebRootPath is not null
            if (string.IsNullOrEmpty(_webHostEnvironment.WebRootPath))
            {
                return StatusCode(500, "Web root path is not configured.");
            }

            string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads/documents");

            try
            {
                // Create directory if it doesn't exist
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                string filePath = Path.Combine(uploadDir, fileName);

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    productdoc.DocumentUrl.CopyTo(stream);
                }

                // Generate the URL for the uploaded file
                string fileUrl = $"{Request.Scheme}://{Request.Host}/Uploads/documents/{fileName}";

                // Save the file information to the database
                var productDoc = new ProductDocuments
                {
                    DocumentType = fileName,
                    DocumentUrl = fileUrl,
                    ProductId = productdoc.ProductId // This should now exist in the Products table

                };
                _context.ProductDocuments.Add(productDoc);
                _context.SaveChanges();

                return Ok(new { fileName, fileUrl });
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while uploading the file.");
            }
        }
        // GET: api/Auctioneers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductRtnDocDTO>>> GetProductDoc()
        {
            var productdoc = await _context.ProductDocuments
                .Select(a => new ProductRtnDocDTO
                {
                    ProductId = a.ProductId,
                    DocumentUrl = a.DocumentUrl
                })
                .ToListAsync();

            return Ok(productdoc);
        }

        // GET: api/Auctioneers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductRtnDocDTO>> GetProductDoc(int id)
        {
            var productdoc = await _context.ProductDocuments.FindAsync(id);

            if (productdoc == null)
            {
                return NotFound();
            }

            var ProductrtnDocDTO = new ProductRtnDocDTO
            {
                DocumentType = productdoc.DocumentType,
                DocumentUrl = productdoc.DocumentUrl,
                ProductId = productdoc.ProductId
            };

            return Ok(ProductrtnDocDTO);
        }

        // PUT: api/Auctioneers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuctioneer(int id, ProductDocDTO productDocDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productdoc = await _context.ProductDocuments.FindAsync(id);
            if (productdoc == null)
            {
                return NotFound();
            }

            productdoc.ProductId = productDocDTO.ProductId;

            _context.Entry(productdoc).State = EntityState.Modified;

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

        // DELETE: api/Auctioneers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductDoc(int id)
        {
            var productdoc = await _context.ProductDocuments.FindAsync(id);
            if (productdoc == null)
            {
                return NotFound();
            }

            _context.ProductDocuments.Remove(productdoc);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
