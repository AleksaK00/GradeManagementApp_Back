using GradeManagementApp_Back.Models;
using GradeManagementApp_Back.Models.DataTransferObjects;
using GradeManagementApp_Back.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradeManagementApp_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdeljenjeController : ControllerBase
    {
        private readonly OdeljenjeRepository odeljenjeRepository;

        public OdeljenjeController()
        {
            odeljenjeRepository = new OdeljenjeRepository();
        }

        //Metoda za hvatanje svih odeljenja iz baze
        [HttpGet]
        public async Task<IActionResult> GetAllClasses()
        {
            List<ClassDTO> listaOdeljenja = await odeljenjeRepository.GetAllClasses();
            if (listaOdeljenja == null || listaOdeljenja.Count == 0)
            {
                return NotFound("Nema podataka o odeljenjima.");
            }
            else
            {
                return Ok(listaOdeljenja);
            }
        }

        //Metoda za dodavanje novog odeljenja u bazu
        [HttpPost("dodaj")]
        public async Task<IActionResult> AddClass([FromBody] ClassSubmitDTO novoOdeljenje)
        {
            if (novoOdeljenje == null)
            {
                return BadRequest(new { message = "Podaci o odeljenju nisu validni." });
            }
            else
            {
                try
                {
                    bool uspeh = await odeljenjeRepository.AddClass(novoOdeljenje);
                    if (!uspeh)
                    {
                        return BadRequest(new { message = "Neuspešno dodavanje u bazu." });
                    }
                    else
                    {
                        return Ok(new { message = "Odeljenje uspešno dodato!" });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
                }
            }
        }
    }
}
