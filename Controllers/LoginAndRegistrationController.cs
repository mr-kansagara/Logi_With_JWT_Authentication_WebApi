using Login_With_JWT_Authentication.Database;
using Login_With_JWT_Authentication.Model.LoginAndRegistration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Login_With_JWT_Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAndRegistrationController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        private readonly IConfiguration configuration;

        public LoginAndRegistrationController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            this.databaseContext = databaseContext;
            this.configuration = configuration;
        }
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] Login login)
        {
            IActionResult response = Unauthorized();
            var userName = databaseContext.Registrations.Where(name => name.Username == login.UserName).FirstOrDefault();

            var password = databaseContext.Registrations.Where(pass => pass.Password == login.Password).FirstOrDefault();

            if (userName != null && password != null)
            {
                var tokenString = GenerateJWTWebToken(login);
                response = Ok(new { token = tokenString });
            }
            else
            {
                return BadRequest("Username and/or password do not match.");
            }

            return response;
        }

        private string GenerateJWTWebToken(Login login)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["JWT:Issuer"],
                configuration["JWT:Audience"],
                null,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Route("Registration")]
        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] Registration registration)
        {
            if (registration != null)
            {
                if (ModelState.IsValid)
                {
                    if (IsUserNameUnique(registration.Username))
                    {
                        Registration user = new Registration()
                        {
                            Id = Guid.NewGuid(),
                            Username = registration.Username,
                            Email = registration.Email,
                            DateOfBirth = registration.DateOfBirth,
                            Password = registration.Password,
                            ConfirmPassword = registration.ConfirmPassword
                        };
                        await databaseContext.Registrations.AddAsync(user);
                        await databaseContext.SaveChangesAsync();
                        return Ok(registration);
                    }
                    else
                    {
                        return BadRequest("userName already Exist");
                    }

                }
                else
                {
                    return BadRequest(ModelState);
                }
            }

            return BadRequest();
        }

        private bool IsUserNameUnique(string username)
        {
            var existingUser = databaseContext.Registrations.FirstOrDefault(u => u.Username == username);
            // If existingUser is null, the username is unique; otherwise, it's not unique
            return existingUser == null;
        }
    }
}

