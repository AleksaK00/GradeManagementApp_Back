using GradeManagementApp_Back.Models;
using GradeManagementApp_Back.Models.DataTransferObjects;

namespace GradeManagementApp_Back.Repository.Interfaces
{
    public interface IRazredRepository
    {
        Task<List<GradeDTO>> GetAllGrades();
        Task<GradeBO?> GetGradeById(int id);
        Task<bool> addGrade(GradeSubmitDTO noviRazred);
        Task<string> EditGrade(int id, GradeSubmitDTO izmenjeniRazred);
    }
}
