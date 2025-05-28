using GradeManagementApp_Back.Models;
using GradeManagementApp_Back.Models.DataTransferObjects;
using GradeManagementApp_Back.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

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

        //Metoda za hvatanje razreda po id-u
        public async Task<GradeBO?> GetGradeById(int id)
        {
            return await _context.Grades.Where(g => g.Id == id).Include(g => g.Program)
                .Include(g => g.SkolskaGodina)
                .Include(g => g.Razred)
                .Select(razredIzBaze => new GradeBO
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
                }).FirstOrDefaultAsync();
        }

        //Metoda za dodavanje novog razreda u bazu
        public async Task<bool> addGrade(GradeSubmitDTO noviRazred)
        {
            bool postoji = await _context.Grades
                .AnyAsync(r => r.SkolskaGodina.Id == noviRazred.SkolskaGodina &&
                          r.Razred.Id == noviRazred.Razred &&
                          r.Program.Id == noviRazred.Program);

            if (!postoji)
            {
                Grade noviRazredUBazi = new()
                {
                    SkolskaGodinaId = noviRazred.SkolskaGodina,
                    RazredId = noviRazred.Razred,
                    ProgramId = noviRazred.Program,
                    DatumUnosa = DateTime.Now
                };
                _context.Grades.Add(noviRazredUBazi);
                await _context.SaveChangesAsync();
                return true;
            }
            else 
            {
                return false;
            }
        }

        //Metoda za izmenu razreda u bazi
        public async Task<string> EditGrade(int id, GradeSubmitDTO izmenjeniRazred)
        {
            var razredIzBaze = await _context.Grades.FindAsync(id);
            if (razredIzBaze != null)
            {
                //Provera da li je promenjen, ili da li pravi duplikat
                if(razredIzBaze.SkolskaGodinaId == izmenjeniRazred.SkolskaGodina &&
                   razredIzBaze.RazredId == izmenjeniRazred.Razred &&
                   razredIzBaze.ProgramId == izmenjeniRazred.Program)
                {
                    return "Ne promenjen";
                }
                if (await _context.Grades.AnyAsync(r => r.Id != id &&
                          r.SkolskaGodinaId == izmenjeniRazred.SkolskaGodina &&
                          r.RazredId == izmenjeniRazred.Razred &&
                          r.ProgramId == izmenjeniRazred.Program))
                {
                    return "Vec postoji";
                }

                //Izmena u bazi
                razredIzBaze.SkolskaGodinaId = izmenjeniRazred.SkolskaGodina;
                razredIzBaze.RazredId = izmenjeniRazred.Razred;
                razredIzBaze.ProgramId = izmenjeniRazred.Program;
                razredIzBaze.DatumIzmene = DateTime.Now;
                await _context.SaveChangesAsync();
                return "Uspeh";
            }
            else
            {
                return "Ne postoji";
            }
        }

        //Metoda za brisanje razreda iz baze
        public async Task<bool> DeleteGrade(int id)
        {
            Grade? razredIzBaze = await _context.Grades.FindAsync(id);
            if (razredIzBaze != null)
            {
                await _context.Classes.Where(odeljenje => odeljenje.GradeId == razredIzBaze.Id).ExecuteDeleteAsync();
                _context.Grades.Remove(razredIzBaze);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Metoda za hvatanje broja ucenika u skolskoj godini
        public async Task<BrojUcenikaDTO> GetBrojUcenikaSkolska(int idSkolskeGodine)
        {
            BrojUcenikaDTO? brojUcenika = await _context.Classes
                .Include(c => c.Grade)
                .Where(c => c.Grade.SkolskaGodinaId == idSkolskeGodine)
                .GroupBy(c => 1)
                .Select(sviRazredi => new BrojUcenikaDTO
                {
                    BrojUcenika = sviRazredi.Sum(c => c.BrojUcenika),
                    BrojUcenica = sviRazredi.Sum(c => c.BrojUcenica)
                }).FirstOrDefaultAsync();

            if (brojUcenika == null)
            {
                return new BrojUcenikaDTO { BrojUcenika = 0, BrojUcenica = 0 };
            }
            else
            {
                return brojUcenika;
            }
        }

        //Metoda za hvatanje broja ucenika u razredu
        public async Task<BrojUcenikaDTO> GetBrojUcenikaRazred(int idRazreda)
        {
            BrojUcenikaDTO? brojUcenika = await _context.Classes
                .Include(c => c.Grade)
                .Where(c => c.Grade.Id == idRazreda)
                .GroupBy(c => 1)
                .Select(sviRazredi => new BrojUcenikaDTO
                {
                    BrojUcenika = sviRazredi.Sum(c => c.BrojUcenika),
                    BrojUcenica = sviRazredi.Sum(c => c.BrojUcenica)
                }).FirstOrDefaultAsync();

            if (brojUcenika == null)
            {
                return new BrojUcenikaDTO { BrojUcenika = 0, BrojUcenica = 0 };
            }
            else
            {
                return brojUcenika;
            }
        }

        //Metoda za uzimanje excel fajla razreda
        public async Task<MemoryStream> CreateExcelFileGrade()
        {
            List<GradeDTO> razrediIzBaze = await this.GetAllGrades();

            //Kreiranje Excel fajla
            ExcelPackage.License.SetNonCommercialPersonal("Aleksa");
            var excelFile = new ExcelPackage();
            var worksheet = excelFile.Workbook.Worksheets.Add("Odeljenja");

            //Zaglavlje kolona
            worksheet.Cells[1, 1].Value = "ID";
            worksheet.Cells[1, 2].Value = "Razred";
            worksheet.Cells[1, 3].Value = "Školska Godina";
            worksheet.Cells[1, 4].Value = "Program";
            worksheet.Cells[1, 5].Value = "Ukupno učenika";
            worksheet.Cells[1, 6].Value = "Broj odeljenja";

            //Popunjavanje podacima
            int red = 2;
            foreach (GradeDTO trenutniRazred in razrediIzBaze)
            {
                worksheet.Cells[red, 1].Value = trenutniRazred.Razred.Id;
                worksheet.Cells[red, 2].Value = trenutniRazred.Razred.RazredSifrarnik.Naziv;
                worksheet.Cells[red, 3].Value = trenutniRazred.Razred.SkolskaGodina.Naziv;
                worksheet.Cells[red, 4].Value = trenutniRazred.Razred.Program.Naziv;
                worksheet.Cells[red, 5].Value = trenutniRazred.UkupnoUcenika;
                worksheet.Cells[red, 6].Value = trenutniRazred.BrojOdeljenja;   

                red++;
            }

            return new MemoryStream(await excelFile.GetAsByteArrayAsync());
        }
    }
}
