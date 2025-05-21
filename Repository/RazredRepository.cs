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
    }
}
