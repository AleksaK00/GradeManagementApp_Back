using GradeManagementApp_Back.Models;
using GradeManagementApp_Back.Models.DataTransferObjects;
using GradeManagementApp_Back.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

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

        //Metoda za uzimanje excel fajla odeljenja
        public async Task<MemoryStream> CreateExcelFileClass()
        {
            List<Class> odeljenjaIzBaze = await _context.Classes
                .Include(c => c.Grade).ThenInclude(g => g.Razred)
                .Include(c => c.Grade).ThenInclude(g => g.SkolskaGodina)
                .Include(c => c.Grade).ThenInclude(g => g.Program)
                .Include(c => c.JezikNastave)
                .Include(c => c.VrstaOdeljenja)
                .Include(c => c.PrviStraniJezik).ToListAsync();

            //Kreiranje Excel fajla
            ExcelPackage.License.SetNonCommercialPersonal("Aleksa");
            var excelFile = new ExcelPackage();
            var worksheet = excelFile.Workbook.Worksheets.Add("Odeljenja");

            //Zaglavlje kolona
            worksheet.Cells[1, 1].Value = "ID";
            worksheet.Cells[1, 2].Value = "Razred";
            worksheet.Cells[1, 3].Value = "Školska Godina";
            worksheet.Cells[1, 4].Value = "Program";
            worksheet.Cells[1, 5].Value = "Naziv Odeljenja";
            worksheet.Cells[1, 6].Value = "Vrsta Odeljenja";
            worksheet.Cells[1, 7].Value = "Kombinovano Odeljenje";
            worksheet.Cells[1, 8].Value = "Celodnevna Nastava";
            worksheet.Cells[1, 9].Value = "Izdvojeno Odeljenje";
            worksheet.Cells[1, 10].Value = "Naziv Izdvojene Škole";
            worksheet.Cells[1, 11].Value = "Odeljenski Starešina";
            worksheet.Cells[1, 12].Value = "Smena";
            worksheet.Cells[1, 13].Value = "Nastavni Jezik";
            worksheet.Cells[1, 14].Value = "Dvojezično Odeljenje";
            worksheet.Cells[1, 15].Value = "Prvi Strani Jezik";
            worksheet.Cells[1, 16].Value = "Ukupno Učenika";
            worksheet.Cells[1, 17].Value = "Broj Učenika";
            worksheet.Cells[1, 18].Value = "Broj Učenica";

            //Popunjavanje podacima
            int red = 2;
            foreach (Class trenutnoOdeljenje in odeljenjaIzBaze)
            {
                worksheet.Cells[red, 1].Value = trenutnoOdeljenje.Id;
                worksheet.Cells[red, 2].Value = trenutnoOdeljenje.Grade.Razred.Naziv;
                worksheet.Cells[red, 3].Value = trenutnoOdeljenje.Grade.SkolskaGodina.Naziv;
                worksheet.Cells[red, 4].Value = trenutnoOdeljenje.Grade.Program.Naziv;
                worksheet.Cells[red, 5].Value = trenutnoOdeljenje.Grade.Razred.Naziv + "-" + trenutnoOdeljenje.Naziv;
                worksheet.Cells[red, 6].Value = trenutnoOdeljenje.VrstaOdeljenja.Naziv;
                worksheet.Cells[red, 7].Value = trenutnoOdeljenje.KombinovanoOdeljenje ? "Da" : "Ne";
                worksheet.Cells[red, 8].Value = trenutnoOdeljenje.CelodnevnaNastava ? "Da" : "Ne";
                worksheet.Cells[red, 9].Value = trenutnoOdeljenje.IzdvojenoOdeljenje ? "Da" : "Ne";
                worksheet.Cells[red, 10].Value = (trenutnoOdeljenje.NazivIzdvojeneSkole == null) ? "" : trenutnoOdeljenje.NazivIzdvojeneSkole;
                worksheet.Cells[red, 11].Value = trenutnoOdeljenje.OdeljenskiStaresina; 
                worksheet.Cells[red, 12].Value = trenutnoOdeljenje.Smena;
                worksheet.Cells[red, 13].Value = trenutnoOdeljenje.JezikNastave.Naziv;
                worksheet.Cells[red, 14].Value = trenutnoOdeljenje.DvojezicnoOdeljenje ? "Da" : "Ne";
                worksheet.Cells[red, 15].Value = (trenutnoOdeljenje.PrviStraniJezik == null) ? "" : trenutnoOdeljenje.PrviStraniJezik.Naziv;
                worksheet.Cells[red, 16].Value = trenutnoOdeljenje.UkupanBrojUcenika;
                worksheet.Cells[red, 17].Value = trenutnoOdeljenje.BrojUcenika;
                worksheet.Cells[red, 18].Value = trenutnoOdeljenje.BrojUcenica;

                red++;
            }

            return new MemoryStream(await excelFile.GetAsByteArrayAsync());
        }
    }
}
