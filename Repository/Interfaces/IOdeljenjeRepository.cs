using GradeManagementApp_Back.Models;
using GradeManagementApp_Back.Models.DataTransferObjects;

namespace GradeManagementApp_Back.Repository.Interfaces
{
    public interface IOdeljenjeRepository
    {
        Task<List<ClassDTO>> GetAllClasses();
        Task<bool> AddClass(ClassSubmitDTO novoOdeljenje);
    }
}
