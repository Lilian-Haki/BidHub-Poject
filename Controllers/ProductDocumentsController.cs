﻿using BidHub_Poject.DTO;
using BidHub_Poject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BidHub_Poject.Controllers
{
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
        public IActionResult Upload(IFormFile file, ProductDocDTO productdoc,string fileName)
        {
            // Validate file extension
            List<string> validExtensions = new List<string> { ".pdf", ".ppt" };
            string extension = Path.GetExtension(file.FileName);
            if (!validExtensions.Contains(extension))
            {
                return BadRequest($"Extension is not valid ({string.Join(", ", validExtensions)})");
            }

            if (file == null || string.IsNullOrEmpty(fileName))
            {
                return BadRequest("File or file name is missing.");
            }

            // Validate file size
            long size = file.Length;
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
                    file.CopyTo(stream);
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
    }
}
