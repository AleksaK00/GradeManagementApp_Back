using GradeManagementApp_Back.Models;
using GradeManagementApp_Back.Models.DataTransferObjects;
using GradeManagementApp_Back.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeManagementApp_Back.Repository
{
    public class OdeljenjeRepository : IOdeljenjeRepository
    {
        private readonly GradeManagementAppContext _context = new();

        //Metoda za hvatanje svih odeljenja iz baze
        public async Task<List<ClassDTO>> GetAllClasses()
        {
            return await _context.Classes
                .Include(c => c.Grade).ThenInclude(g => g.Razred)
                .Include(c => c.JezikNastave)
                .Include(c => c.VrstaOdeljenja)
                .Select(odeljenjeIzBaze => new ClassDTO()
                {
                    Id = odeljenjeIzBaze.Id,
                    OdeljenskiStaresina = odeljenjeIzBaze.OdeljenskiStaresina,
                    IzdvojenoOdeljenje = odeljenjeIzBaze.IzdvojenoOdeljenje,
                    Naziv = odeljenjeIzBaze.Grade.Razred.Naziv + " - " + odeljenjeIzBaze.Naziv,
                    JezikNastave = new CodebookItemBO()
                    {
                        Id = odeljenjeIzBaze.JezikNastave.Id,
                        Naziv = odeljenjeIzBaze.JezikNastave.Naziv
                    },
                    VrstaOdeljenja = new CodebookItemBO()
                    {
                        Id = odeljenjeIzBaze.VrstaOdeljenja.Id,
                        Naziv = odeljenjeIzBaze.VrstaOdeljenja.Naziv
                    },
                    UkupanBrojUcenika = odeljenjeIzBaze.UkupanBrojUcenika
                }).ToListAsync();
        }

        //Metoda za dodavanje novog odeljenja u bazu
        public async Task<bool> AddClass(ClassSubmitDTO novoOdeljenje)
        {
            Class odeljenjeZaBazu = new Class()
            {
                GradeId = (int)novoOdeljenje.RazredId,
                Naziv = novoOdeljenje.NazivOdeljenja,
                VrstaOdeljenjaId = novoOdeljenje.VrstaOdeljenja,
                KombinovanoOdeljenje = novoOdeljenje.KombinovanoOdeljenje,
                CelodnevnaNastava = novoOdeljenje.CelodnevnaNastava,
                IzdvojenoOdeljenje = novoOdeljenje.IzdvojenoOdeljenje,
                NazivIzdvojeneSkole = novoOdeljenje.NazivIzdvojeneSkole,
                OdeljenskiStaresina = novoOdeljenje.OdeljenskiStaresina,
                Smena = novoOdeljenje.Smena,
                JezikNastaveId = novoOdeljenje.NastavniJezik,
                DvojezicnoOdeljenje = novoOdeljenje.DvoJezicnoOdeljenje,
                PrviStraniJezikId = (novoOdeljenje.PrviStraniJezik == 0) ? null : novoOdeljenje.PrviStraniJezik,
                UkupanBrojUcenika = novoOdeljenje.UkupnoUcenika,
                BrojUcenika = novoOdeljenje.BrojUcenika,
                BrojUcenica = novoOdeljenje.BrojUcenica,
                DatumUnosa = DateTime.Now
            };

            await _context.Classes.AddAsync(odeljenjeZaBazu);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
