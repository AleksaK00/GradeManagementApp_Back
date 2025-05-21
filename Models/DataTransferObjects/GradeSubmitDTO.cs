namespace GradeManagementApp_Back.Models.DataTransferObjects
{
    public class GradeSubmitDTO
    {
        public int? Id { get; set; }
        public int SkolskaGodina { get; set; }
        public int Razred { get; set; }
        public int Program { get; set; }
    }
}
