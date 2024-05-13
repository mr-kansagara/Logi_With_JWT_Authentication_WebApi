using Login_With_JWT_Authentication.Database;
using Login_With_JWT_Authentication.Model.LoginAndRegistration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Login_With_JWT_Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAndRegistrationController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;

        public LoginAndRegistrationController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<IActionResult> Registration(Registration registration)
        {
            if(registration != null)
            {
                if(ModelState.IsValid)
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
                }else
                {
                    return BadRequest(ModelState);
                }
            }
            
            return BadRequest();
        }


        public async Task<IActionResult> Login(Login login) 
        {

        }

    }
}
