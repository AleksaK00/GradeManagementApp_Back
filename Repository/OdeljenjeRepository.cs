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

        //Metoda za hvatanje odeljenja po id-u
        public async Task<ClassBO?> GetClassById(int id)
        {
            return await _context.Classes.Where(c => c.Id == id)
                .Include(c => c.Grade).ThenInclude(g => g.Razred)
                .Include(c => c.Grade).ThenInclude(g => g.SkolskaGodina)
                .Include(c => c.Grade).ThenInclude(g => g.Program)
                .Include(c => c.JezikNastave)
                .Include(c => c.VrstaOdeljenja)
                .Include(c => c.PrviStraniJezik)
                .Select(odeljenjeIzBaze => new ClassBO()
                {
                    Id = odeljenjeIzBaze.Id,
                    Naziv = odeljenjeIzBaze.Naziv,
                    KombinovanoOdeljenje = odeljenjeIzBaze.KombinovanoOdeljenje,
                    CelodnevnaNastava = odeljenjeIzBaze.CelodnevnaNastava,
                    IzdvojenoOdeljenje = odeljenjeIzBaze.IzdvojenoOdeljenje,
                    NazivIzdvojeneSkole = odeljenjeIzBaze.NazivIzdvojeneSkole,
                    OdeljenskiStaresina = odeljenjeIzBaze.OdeljenskiStaresina,
                    Smena = odeljenjeIzBaze.Smena,
                    DvojezicnoOdeljenje = odeljenjeIzBaze.DvojezicnoOdeljenje,
                    UkupanBrojUcenika = odeljenjeIzBaze.UkupanBrojUcenika,
                    BrojUcenika = odeljenjeIzBaze.BrojUcenika,
                    BrojUcenica = odeljenjeIzBaze.BrojUcenica,
                    Grade = new GradeBO()
                    {
                        Id = odeljenjeIzBaze.Grade.Id,
                        RazredSifrarnik = new CodebookItemBO()
                        {
                            Id = odeljenjeIzBaze.Grade.Razred.Id,
                            Naziv = odeljenjeIzBaze.Grade.Razred.Naziv
                        },
                        Program = new CodebookItemBO()
                        {
                            Id = odeljenjeIzBaze.Grade.Program.Id,
                            Naziv = odeljenjeIzBaze.Grade.Program.Naziv
                        },
                        SkolskaGodina = new CodebookItemBO()
                        {
                            Id = odeljenjeIzBaze.Grade.SkolskaGodina.Id,
                            Naziv = odeljenjeIzBaze.Grade.SkolskaGodina.Naziv
                        }
                    },
                    JezikNastave = new CodebookItemBO()
                    {
                        Id = odeljenjeIzBaze.JezikNastave.Id,
                        Naziv = odeljenjeIzBaze.JezikNastave.Naziv
                    },
                    PrviStraniJezik = (odeljenjeIzBaze.PrviStraniJezik != null) ? new CodebookItemBO()
                    {
                        Id = odeljenjeIzBaze.PrviStraniJezik.Id,
                        Naziv = odeljenjeIzBaze.PrviStraniJezik.Naziv
                    } : null,
                    VrstaOdeljenja = new CodebookItemBO()
                    {
                        Id = odeljenjeIzBaze.VrstaOdeljenja.Id,
                        Naziv = odeljenjeIzBaze.VrstaOdeljenja.Naziv
                    }
                }).FirstOrDefaultAsync();
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

        //Metoda za izmenu odeljenja u bazi
        public async Task<string> UpdateClass(int id, ClassSubmitDTO izmenjenoOdeljenje)
        {
            Class? odeljenjeIzBaze = await _context.Classes.FirstOrDefaultAsync(c => c.Id == id);
            if (odeljenjeIzBaze == null)
            {
                return "Ne postoji";
            }
            if (izmenjenoOdeljenje.PrviStraniJezik == 0 && izmenjenoOdeljenje.DvoJezicnoOdeljenje == true)
            {
                return ("Ne izabrano");
            }

            //Provera da li je promenjen
            if (odeljenjeIzBaze.Naziv == izmenjenoOdeljenje.NazivOdeljenja &&
                odeljenjeIzBaze.VrstaOdeljenjaId == izmenjenoOdeljenje.VrstaOdeljenja &&
                odeljenjeIzBaze.KombinovanoOdeljenje == izmenjenoOdeljenje.KombinovanoOdeljenje &&
                odeljenjeIzBaze.CelodnevnaNastava == izmenjenoOdeljenje.CelodnevnaNastava &&
                odeljenjeIzBaze.IzdvojenoOdeljenje == izmenjenoOdeljenje.IzdvojenoOdeljenje &&
                odeljenjeIzBaze.NazivIzdvojeneSkole == izmenjenoOdeljenje.NazivIzdvojeneSkole &&
                odeljenjeIzBaze.OdeljenskiStaresina == izmenjenoOdeljenje.OdeljenskiStaresina &&
                odeljenjeIzBaze.Smena == izmenjenoOdeljenje.Smena &&
                odeljenjeIzBaze.JezikNastaveId == izmenjenoOdeljenje.NastavniJezik &&
                odeljenjeIzBaze.DvojezicnoOdeljenje == izmenjenoOdeljenje.DvoJezicnoOdeljenje &&
                odeljenjeIzBaze.PrviStraniJezikId == ((izmenjenoOdeljenje.PrviStraniJezik == 0) ? null : izmenjenoOdeljenje.PrviStraniJezik) &&
                odeljenjeIzBaze.UkupanBrojUcenika == izmenjenoOdeljenje.UkupnoUcenika &&
                odeljenjeIzBaze.BrojUcenika == izmenjenoOdeljenje.BrojUcenika &&
                odeljenjeIzBaze.BrojUcenica == izmenjenoOdeljenje.BrojUcenica)
            {
                return "Ne promenjen";
            }

            //Izvrsavanje promene
            odeljenjeIzBaze.Naziv = izmenjenoOdeljenje.NazivOdeljenja;
            odeljenjeIzBaze.VrstaOdeljenjaId = izmenjenoOdeljenje.VrstaOdeljenja;
            odeljenjeIzBaze.KombinovanoOdeljenje = izmenjenoOdeljenje.KombinovanoOdeljenje;
            odeljenjeIzBaze.CelodnevnaNastava = izmenjenoOdeljenje.CelodnevnaNastava;
            odeljenjeIzBaze.IzdvojenoOdeljenje = izmenjenoOdeljenje.IzdvojenoOdeljenje;
            odeljenjeIzBaze.NazivIzdvojeneSkole = izmenjenoOdeljenje.NazivIzdvojeneSkole;
            odeljenjeIzBaze.OdeljenskiStaresina = izmenjenoOdeljenje.OdeljenskiStaresina;
            odeljenjeIzBaze.Smena = izmenjenoOdeljenje.Smena;
            odeljenjeIzBaze.JezikNastaveId = izmenjenoOdeljenje.NastavniJezik;
            odeljenjeIzBaze.DvojezicnoOdeljenje = izmenjenoOdeljenje.DvoJezicnoOdeljenje;
            odeljenjeIzBaze.PrviStraniJezikId = (izmenjenoOdeljenje.PrviStraniJezik == 0) ? null : izmenjenoOdeljenje.PrviStraniJezik;
            odeljenjeIzBaze.UkupanBrojUcenika = izmenjenoOdeljenje.UkupnoUcenika;
            odeljenjeIzBaze.BrojUcenika = izmenjenoOdeljenje.BrojUcenika;
            odeljenjeIzBaze.BrojUcenica = izmenjenoOdeljenje.BrojUcenica;
            odeljenjeIzBaze.DatumIzmene = DateTime.Now;

            _context.Classes.Update(odeljenjeIzBaze);
            await _context.SaveChangesAsync();
            return "Uspeh";
        }

        //Metoda za brisanje odeljenja iz baze
        public async Task<bool> DeleteClass(int id)
        {
            Class? odeljenjeIzBaze = await _context.Classes.FindAsync(id);
            if (odeljenjeIzBaze == null)
            {
                return false;
            }
            _context.Classes.Remove(odeljenjeIzBaze);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
