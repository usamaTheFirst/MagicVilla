using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;

namespace MagicVilla.MagicApi.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private const string Secret = "mySecretKeyWithAtLeast32Characters";

        [HttpGet("GetToken")]
        public ActionResult GetToken()
        {
            var key = Encoding.UTF8.GetBytes(Secret); // Use a byte array for the key
            if (key.Length < 32) // Ensure key length is at least 32 bytes (256 bits)
            {
                return BadRequest("Invalid key length. Key must be at least 32 characters long.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "John Doe"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        [HttpGet("GetData")]
        public ActionResult GetData()
        {
            if (!Request.Headers.TryGetValue("Authorization", out var authHeaderValues))
            {
                return Unauthorized();
            }

            var token = authHeaderValues.ToString().Replace("Bearer ", ""); // Remove "Bearer " prefix if present
            var key = Encoding.UTF8.GetBytes(Secret);

            if (key.Length < 32) // Ensure key length is at least 32 bytes (256 bits)
            {
                return BadRequest("Invalid key length. Key must be at least 32 characters long.");
            }

            var handler = new JwtSecurityTokenHandler();

            try
            {
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    // Add other validations if required
                }, out _);
            }
            catch (SecurityTokenException)
            {
                return Unauthorized();
            }

            return Ok(new { Message = "Data retrieved successfully" });
        }
    }
}
