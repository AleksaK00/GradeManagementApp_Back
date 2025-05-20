using GradeManagementApp_Back.Models;
using GradeManagementApp_Back.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradeManagementApp_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SifrarnikStavkaController : ControllerBase
    {

        private readonly SifrarnikStavkaRepository sifrarnikStavkaRepository;

        public SifrarnikStavkaController()
        {
            sifrarnikStavkaRepository = new SifrarnikStavkaRepository();
        }

        //API poziv za hvatanje stavki iz sifrarnika tipa zadatog u parametru url-a
        [HttpGet("{tip}")]
        public async Task<IActionResult> GetAllSifrarnikStavkeTipa(string tip)
        {
            List<CodebookItemBO> listaPrograma = new List<CodebookItemBO>();
            try
            {
                listaPrograma = await sifrarnikStavkaRepository.GetAllStavkeTipa(tip.Replace("_", " "));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
            return Ok(listaPrograma);
        }
    }
}
