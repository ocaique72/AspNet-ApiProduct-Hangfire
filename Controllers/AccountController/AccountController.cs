using apiDesafio.Models;
using apiDesafio.Services;
using apiDesafio.ViewModel;
using Blog.Attributes;
using desafio.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SecureIdentity.Password;

namespace apiDesafio.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {

        //injecao dependencia pt1
        private readonly TokenServices _tokenService;

        //injecao dependencia pt2
        public AccountController(TokenServices tokenService)
        {
            _tokenService = tokenService;
        }

        //injcao de dependecia subtitui o fromService
        [ApiKey]
        [HttpPost("v1/accounts")]
        public async Task<IActionResult> PostRegisterAsync(
            [FromBody] RegisterViewModel model,
            [FromServices] AppDbContext context,
            [FromQuery] string api_key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Slug = model.Email.Replace("@", "-").Replace(".", "-"),

            };
            //gerar password
            var password = PasswordGenerator.Generate(20, true, false);
            user.PasswordHash = PasswordHasher.Hash(password);

            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return Ok(new { user = user.Email, password });
            }
            catch(Exception ex) {
                return BadRequest(ex);
            }
            
        }
        [HttpPost("v1/accounts/login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginViewModel model,
            [FromServices] TokenServices tokenServices,
            [FromServices] AppDbContext context)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //autenticando login
            var user = await context
                .Users
                .AsNoTracking()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
                return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

            if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
                return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

            try
            {
                var token = tokenServices.GenerateToken(user);
                return Ok(new ResultViewModel<string>(token, null));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha no servidor"));
            }
        }

        //[HttpGet("v1/user")]
        //public IActionResult GetUser() => Ok(User.Identity.Name);

        //[Authorize(Roles = "admin")]
        //[HttpGet("v1/admin")]
        //public IActionResult GetAdmin() => Ok(User.Identity.Name);
    }
}
