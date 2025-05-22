using GradeManagementApp_Back.Models;
using GradeManagementApp_Back.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradeManagementApp_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserRepository userRepository;

        public LoginController()
        {
            userRepository = new UserRepository();
        }

        //Metoda za prijavu korisnika
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserBO korisnik)
        {
            if (korisnik == null || string.IsNullOrEmpty(korisnik.Email) || string.IsNullOrEmpty(korisnik.Sifra))
            {
                return BadRequest(new { message = "Email i lozinka su obavezni" });
            }

            UserBO? korisnikIzBaze = await userRepository.LoginUser(korisnik.Email, korisnik.Sifra);
            if (korisnikIzBaze == null)
            {
                return Unauthorized(new { message = "Pogrešan email ili lozinka" });
            }
            else
            {
                return Ok(new { message = korisnikIzBaze.Email });
            }
        }
    }
}
