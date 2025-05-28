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

        //Metoda za hvatanje odeljenja po id-u
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassById(int id)
        {
            try
            {
                ClassBO? odeljenje = await odeljenjeRepository.GetClassById(id);
                if (odeljenje == null)
                {
                    return NotFound(new { message = "Odeljenje sa datim id-om nije pronadjeno" });
                }
                else
                {
                    return Ok(odeljenje);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
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

        //Metoda za izmenu podataka o odeljenju u bazi
        [HttpPut("izmeni/{id}")]
        public async Task<IActionResult> UpdateClass(int id, [FromBody] ClassSubmitDTO izmenjenoOdeljenje)
        {
            if (izmenjenoOdeljenje == null)
            {
                return BadRequest(new { message = "Podaci o odeljenju nisu validni." });
            }
            try
            {
                string poruka = await odeljenjeRepository.UpdateClass(id, izmenjenoOdeljenje);
                if (poruka == "Ne postoji")
                {
                    return NotFound(new { message = "Odeljenje sa datim id-om nije pronađeno." });
                }
                else if (poruka == "Ne promenjen")
                {
                    return BadRequest(new { message = "Uneti podaci su isti kao posojeći!" });
                }
                else if (poruka == "Ne izabrano")
                {
                    return BadRequest(new { message = "Nije izabran prvi strani jezik!" });
                }

                return Ok(new { message = "Odeljenje uspešno izmenjeno!" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        //Metoda za brisanje odeljenja iz baze
        [HttpDelete("obrisi/{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            try
            {
                bool uspeh = await odeljenjeRepository.DeleteClass(id);
                if (!uspeh)
                {
                    return NotFound(new { message = "Odeljenje sa datim id-om nije pronađeno." });
                }
                return Ok(new { message = "Odeljenje uspešno obrisano!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        //Metoda za preuzimanje excel fajla sa svim odeljenjima
        [HttpGet("excel")]
        public async Task<IActionResult> GetClassesExcel()
        {
            try
            {
                MemoryStream excelFile = await odeljenjeRepository.CreateExcelFileClass();
                if (excelFile == null)
                {
                    return NotFound(new { message = "Nema podataka za preuzimanje." });
                }
                return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Odeljenja.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
