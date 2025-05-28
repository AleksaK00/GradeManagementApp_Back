using GradeManagementApp_Back.Models;
using GradeManagementApp_Back.Models.DataTransferObjects;

namespace GradeManagementApp_Back.Repository.Interfaces
{
    public interface IOdeljenjeRepository
    {
        Task<List<ClassDTO>> GetAllClasses();
        Task<ClassBO?> GetClassById(int id);   
        Task<bool> AddClass(ClassSubmitDTO novoOdeljenje);
        Task<string> UpdateClass(int id, ClassSubmitDTO izmenjenoOdeljenje);
        Task<bool> DeleteClass(int id);
        Task<MemoryStream> CreateExcelFileClass();
    }
}
