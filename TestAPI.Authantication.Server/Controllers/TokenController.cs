using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TestAPI.Authantication.Server.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestAPI.Authantication.Server.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration _configuration;
        public TokenController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult Post([FromForm]TokenRequest request)
        {
            try
            {
                // get audience details form database (name and password)
                // better soution is to send username. password and audience to DB then get status
                if (request.audience != "audience from database")
                {
                    return BadRequest("No valid audince found");
                }

                //validate username password for audience
                if (request.username == "test" && request.password == "test")
                {
                    //fill the claim array, that will avilable for audience
                    var claims = new[] { new Claim(ClaimTypes.Name, request.username) };

                    //use audience password (base64 encoded) , store it in DB, together with audience
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));


                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: _configuration["issuer"],
                        audience: request.audience,
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30), // better put this into audience table
                        signingCredentials: creds);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    });
                }

                return BadRequest("Could not verify username and password");
            }
            catch(Exception ex)
            {
                //log error and return status code
                return StatusCode(500);
            }
            }
            
        
    }
}
