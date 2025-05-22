using GradeManagementApp_Back.Models;

namespace GradeManagementApp_Back.Repository.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserBO?> LoginUser(string email, string sifra);
    }
}
