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
    }
}
