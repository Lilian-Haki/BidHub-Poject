using BidHub_Poject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BidHub_Poject.DTO;
//using NETCore.MailKit.Core;
using static BidHub_Poject.Models.EmailConfiguration;
using BidHub_Poject.Services;
using System.Net.Mail;
using System.Net;
using NETCore.MailKit;



namespace BidHub_Poject.Controllers
{
    public class RolesController : Controller
    {
        private readonly BidHubDbContext _userContext;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IConfiguration _configuration;
       

        //public RolesController(BidHubDbContext context)
        //{
        //    _context = context;
        //}

        //public class RolesContext /*: IdentityDbContext<Register, IdentityRole<Guid>, Guid>*/
        //{
        //    public RolesContext(DbContextOptions<BidHubDbContext> options) : base(options)
        //    {
        //    }
        //    protected override void OnModelCreating(ModelBuilder builder)
        //    {
        //        base.OnModelCreating(builder);
        //        SeedRoles(builder);
        //    }

        //    private void SeedRoles(ModelBuilder builder)
        //    {
        //        builder.Entity<IdentityRole>().HasData
        //                        (
        //                        new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
        //                        new IdentityRole() { Name = "Bidder", ConcurrencyStamp = "2", NormalizedName = "Bidder" },
        //                        new IdentityRole() { Name = "Auctioneer", ConcurrencyStamp = "3", NormalizedName = "Auctioneer" }

        //                        );
        //    }
        //}

        public RolesController( UserManager<Users> userManager, SignInManager<Users> signInManager, IConfiguration configuration, BidHubDbContext context)//initialized and injected via constructor:
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _userContext = context;

        }


        //Start

        [HttpPost("register-bidder")]
        public async Task<IActionResult> RegisterBidder([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new Users
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                PhysicalAddress = model.PhysicalAddress,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var bidder = new Bidders
            {
                UserId = user.UserId,
                CompanyUrl = model.CompanyUrl
            };

            _userContext.Bidders.Add(bidder);
            await _userContext.SaveChangesAsync();

            var otp = GenerateOTP();

            var otpEntry = new UserOtp
            {
                UserId = user.UserId,
                OtpCode = otp,
                ExpiryTime = DateTime.Now.AddMinutes(15)
            };

            _userContext.UserOtps.Add(otpEntry);
            await _userContext.SaveChangesAsync();

            // Send OTP via email
            var emailSent = await SendOtpByEmail(user.Email, otp);
            if (!emailSent)
            {
                return StatusCode(500, new { message = "Failed to send OTP email" });
            }

            return Ok(new { message = "Bidder Registered successfully" });
        }

        [HttpPost("register-auctioneer")]
        public async Task<IActionResult> RegisterAuctioneer([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Step 1: Find the company by name or create a new one if it doesn't exist
            var company = await _userContext.Companies
                .FirstOrDefaultAsync(c => c.CompanyName == model.CompanyName);

            if (company == null)
            {
                company = new Company
                {
                    CompanyName = model.CompanyName
                };
                _userContext.Companies.Add(company);
                await _userContext.SaveChangesAsync();
            }

            var user = new Users
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                PhysicalAddress = model.PhysicalAddress,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var auctioneer = new Auctioneers
            {
                UserId = user.UserId,
                Role = model.Role,
                StaffNo = model.StaffNo,
                CompanyId = company.CompanyId
            };
            _userContext.Auctioneers.Add(auctioneer);
            await _userContext.SaveChangesAsync();

            return Ok("Auctioneer registered successfully");

        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(string email, string otp)
        {

            //var user = await _userContext.Users.SingleOrDefaultAsync(u => u.Email == email);
            var user = await _userContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }
            var otpEntry2 = await _userContext.UserOtps.Where(i => i.UserId == user.UserId && i.OtpCode == otp).FirstOrDefaultAsync();

            var otpEntry = await _userContext.UserOtps
                .Where(o => o.UserId == user.UserId && o.OtpCode == otp)
                .OrderByDescending(o => o.ExpiryTime)
                .FirstOrDefaultAsync();

            if (otpEntry == null || otpEntry.ExpiryTime < DateTime.Now)
            {
                return BadRequest(new { message = "Invalid or expired OTP" });
            }

            // OTP is valid, mark user as verified
            user.IsVerified = true;
            var updateResult = await _userManager.UpdateAsync(user);

            return Ok(new { message = "User verified successfully" });
        }

        // Utility method to generate a 6-digit OTP
        private string GenerateOTP()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        // Utility method to send OTP via email
        private async Task<bool> SendOtpByEmail(string email, string otp)
        {
            try
            {
                var fromAddress = new MailAddress(_configuration["SmtpSettings:UserName"], "Bidhub");
                var toAddress = new MailAddress(email);
                string fromPassword = _configuration["SmtpSettings:Password"];
                string subject = "OTP Code";
                string body = $"Your OTP code is: {otp}";

                var smtp = new SmtpClient
                {
                    Host = _configuration["SmtpSettings:Host"],
                    Port = int.Parse(_configuration["SmtpSettings:Port"]),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    await smtp.SendMailAsync(message);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO userDto)
        {
            var user = await _userContext.Users.SingleOrDefaultAsync(u => u.UserName == userDto.UserName);
            if (user == null)
                return Unauthorized();


            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        private string GenerateJwtToken(Users user)
        {
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Email, user.Email)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //End
    }
    }
