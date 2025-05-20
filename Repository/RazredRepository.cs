using GradeManagementApp_Back.Models;
using GradeManagementApp_Back.Models.DataTransferObjects;
using GradeManagementApp_Back.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeManagementApp_Back.Repository
{
    public class RazredRepository : IRazredRepository
    {
        private readonly GradeManagementAppContext _context = new();

        public RazredRepository() { }

        //Metoda za hvatanje svih razreda iz baze
        public async Task<List<GradeDTO>> GetAllGrades()
        {
            List<GradeDTO> listaRazreda = await _context.Grades
                .Include(g => g.Program)
                .Include(g => g.SkolskaGodina)
                .Include(g => g.Razred)
                .Select(razredIzBaze => new GradeDTO
                {
                    Razred = new GradeBO
                    {
                        Id = razredIzBaze.Id,
                        Program = new CodebookItemBO
                        {
                            Id = razredIzBaze.Program.Id,
                            Naziv = razredIzBaze.Program.Naziv
                        },
                        RazredSifrarnik = new CodebookItemBO
                        {
                            Id = razredIzBaze.Razred.Id,
                            Naziv = razredIzBaze.Razred.Naziv
                        },
                        SkolskaGodina = new CodebookItemBO
                        {
                            Id = razredIzBaze.SkolskaGodina.Id,
                            Naziv = razredIzBaze.SkolskaGodina.Naziv
                        }
                    },
                    UkupnoUcenika = razredIzBaze.Classes.Sum(c => c.UkupanBrojUcenika),
                    BrojOdeljenja = razredIzBaze.Classes.Count()
                })
                .ToListAsync();

            return listaRazreda;
        }
    }
}
