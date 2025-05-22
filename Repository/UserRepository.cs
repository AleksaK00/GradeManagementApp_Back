using GradeManagementApp_Back.Models;
using GradeManagementApp_Back.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace GradeManagementApp_Back.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly GradeManagementAppContext _context = new();

        //Metoda za projavu korisnika
        public async Task<UserBO?> LoginUser(string email, string sifra)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(sifra));
            StringBuilder hexSifra = new StringBuilder();
            foreach (byte b in bytes)
            {
                hexSifra.Append(b.ToString("x2"));
            }

            User? korisnikIzBaze = await _context.Users.Where(k => k.Email == email && k.Lozinka == hexSifra.ToString()).FirstOrDefaultAsync();
            if (korisnikIzBaze == null)
            {
                return null;
            }
            else
            {
                UserBO korisnik = new UserBO()
                {
                    Id = korisnikIzBaze.Id,
                    Email = korisnikIzBaze.Email,
                };
                return korisnik;
            }
        }
    }
}
