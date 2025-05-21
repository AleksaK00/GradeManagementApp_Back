using GradeManagementApp_Back.Models;
using GradeManagementApp_Back.Models.DataTransferObjects;
using GradeManagementApp_Back.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GradeManagementApp_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RazredController : ControllerBase
    {
        RazredRepository razredRepository;

        public RazredController() {
            razredRepository = new RazredRepository();
        }

        //API poziv sa hvatanje svih razreda i osnovnim inofmacijama o odeljenjima u njima
        [HttpGet]
        public async Task<IActionResult> GetAllGrades()
        {
            List<GradeDTO> listaRazreda = new List<GradeDTO>();
            try
            {
                listaRazreda = await razredRepository.GetAllGrades();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(listaRazreda);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRazredById(int id)
        {
            GradeBO? razred = await razredRepository.GetGradeById(id);
            if (razred == null)
            {
                return NotFound(new { message = "Razred ne postoji" });
            }
            else
            {
                return Ok(razred);
            }
        }

    //API poziv za dodavanje novog razreda
    [HttpPost("dodaj")]
        public async Task<IActionResult> AddGrade([FromBody] GradeSubmitDTO noviRazred)
        {
            try
            {
                bool uspeh = await razredRepository.addGrade(noviRazred);
                if (uspeh)
                {
                    return Ok(new { message = "Uspešno je dodat razred" });
                }
                else
                {
                    return BadRequest(new { message = "Razred već postoji" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        //API poziv za izmenu razreda
        [HttpPut("izmeni/{id}")]
        public async Task<IActionResult> EditGrade(int id, [FromBody] GradeSubmitDTO izmenjeniRazred)
        {
            try
            {
                string porukaUspeha = await razredRepository.EditGrade(id, izmenjeniRazred);

                if (porukaUspeha == "Ne postoji")
                {
                    return NotFound(new { message = "Razred ne postoji" });
                }
                else if (porukaUspeha == "Vec postoji")
                {
                    return BadRequest(new { message = "Razred već postoji" });
                }
                else if (porukaUspeha == "Ne promenjen")
                {
                    return BadRequest(new { message = "Razred već ima unete informacije" });
                }
                return Ok(new { message = "Uspešno je izmenjen razred" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        
    }
}
