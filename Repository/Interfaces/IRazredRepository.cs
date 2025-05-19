using GradeManagementApp_Back.Models;
using GradeManagementApp_Back.Models.DataTransferObjects;

namespace GradeManagementApp_Back.Repository.Interfaces
{
    public interface IRazredRepository
    {
        Task<List<GradeDTO>> GetAllGrades();
    }
}
