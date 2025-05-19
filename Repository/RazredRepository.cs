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
            var razrediIzBaze = await _context.Grades
                .Include(g => g.Program)
                .Include(g => g.SkolskaGodina)
                .Include(g => g.Razred)
                .ToListAsync();

            List<GradeDTO> listaRazreda = new List<GradeDTO>();
            foreach (var razred in razrediIzBaze)
            {
                GradeDTO noviRazred = new GradeDTO()
                {
                    Razred = new GradeBO
                    {
                        Id = razred.Id,
                        Program = new CodebookItemBO
                        {
                            Id = razred.Program.Id,
                            Naziv = razred.Program.Naziv
                        },
                        RazredSifrarnik = new CodebookItemBO
                        {
                            Id = razred.Razred.Id,
                            Naziv = razred.Razred.Naziv
                        },
                        SkolskaGodina = new CodebookItemBO
                        {
                            Id = razred.SkolskaGodina.Id,
                            Naziv = razred.SkolskaGodina.Naziv
                        }
                    },
                    UkupnoUcenika = await _context.Classes.Where(c => c.GradeId == razred.Id).Select(c => c.UkupanBrojUcenika).SumAsync(),
                    BrojOdeljenja = await _context.Classes.Where(c => c.GradeId == razred.Id).CountAsync(),
                };

                listaRazreda.Add(noviRazred);
            }

            return listaRazreda;
        }
    }
}
