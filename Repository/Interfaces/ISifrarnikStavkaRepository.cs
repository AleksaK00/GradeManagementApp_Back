using GradeManagementApp_Back.Models;

namespace GradeManagementApp_Back.Repository.Interfaces
{
    public interface ISifrarnikStavkaRepository
    {
        Task<List<CodebookItemBO>> GetAllStavkeTipa(string tip);
    }
}
