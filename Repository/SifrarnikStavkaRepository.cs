using GradeManagementApp_Back.Models;
using GradeManagementApp_Back.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeManagementApp_Back.Repository
{
    public class SifrarnikStavkaRepository : ISifrarnikStavkaRepository
    {
        private readonly GradeManagementAppContext _context = new();

        //Metoda za hvatanje svih stavki datog tipa iz sifrarnika
        public async Task<List<CodebookItemBO>> GetAllStavkeTipa(string tip)
        {
            List<CodebookItemBO> listaStavki = await _context.Coodebookitems
                .Include(sifStavka => sifStavka.Coodebook)
                .Where(sifStavka => sifStavka.Coodebook.Naziv == tip)
                .Select(stavkaIzBaze => new CodebookItemBO
                {
                    Id = stavkaIzBaze.Id,
                    Naziv = stavkaIzBaze.Naziv
                }).ToListAsync();

            return listaStavki;
        }
    }
}
