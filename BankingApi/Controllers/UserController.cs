using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApi.Authentication;
using BankingApi.DAL;
using BankingApi.Model;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using BankingApi.Services.Interfaces;
using BankingApi.Services.Implementation;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BankContext _dbcontext;
        private readonly JWTSettings _jwtsettings;
        private IUserService _userService;

        public UserController(BankContext DBcontext, IUserService UserService, IOptions<JWTSettings> options)
        {
            _dbcontext = DBcontext;
            _jwtsettings = options.Value;
            _userService = UserService;
        }
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] UserCredential user)
        {
            var _user = _dbcontext.User.FirstOrDefault(options => options.UserName == user.userName);
            if(_user == null)
            {
                return Unauthorized();
            }

            var _tokenhandler = new JwtSecurityTokenHandler();
            var _tokenkey = Encoding.UTF8.GetBytes(_jwtsettings.Seceretkey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
              Subject = new ClaimsIdentity(
                  new Claim[]
                  {
                      new Claim(ClaimTypes.Name,_user.UserName),
                  }
              ),
              Expires = DateTime.Now.AddDays(1),
              SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_tokenkey), SecurityAlgorithms.HmacSha256)
            };

            var token = _tokenhandler.CreateToken(tokenDescriptor);
            string finaltoken = _tokenhandler.WriteToken(token);
            return Ok(finaltoken);
        }

        [HttpPost]
        [Route("add_Customer")]
        public IActionResult RegisterNewAccount([FromBody] User userRequest)
        {
            if (!ModelState.IsValid) return BadRequest(userRequest);

            var AccountResponse = _userService.RegestireNewUser(userRequest);
            // return AccountResponse == null ? BadRequest() : Ok(AccountResponse);
            if (AccountResponse == null)
                return BadRequest();
            else
                return Ok(AccountResponse);
        }

    }
       
}
