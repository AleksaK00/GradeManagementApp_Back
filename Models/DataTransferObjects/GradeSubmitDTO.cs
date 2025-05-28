using System.ComponentModel.DataAnnotations;

namespace GradeManagementApp_Back.Models.DataTransferObjects
{
    public class GradeSubmitDTO
    {
        public int? Id { get; set; }
        [Required]
        public int SkolskaGodina { get; set; }
        [Required]
        public int Razred { get; set; }
        [Required]
        public int Program { get; set; }
    }
}
