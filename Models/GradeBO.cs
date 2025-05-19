namespace GradeManagementApp_Back.Models
{
    public class GradeBO
    {
        public int Id { get; set; }
        public virtual CodebookItemBO Program { get; set; }
        public virtual CodebookItemBO RazredSifrarnik { get; set; }
        public virtual CodebookItemBO SkolskaGodina { get; set; }
    }
}
