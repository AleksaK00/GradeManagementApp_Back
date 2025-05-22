namespace GradeManagementApp_Back.Models.DataTransferObjects
{
    public class ClassDTO
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public bool IzdvojenoOdeljenje { get; set; }
        public string OdeljenskiStaresina { get; set; }
        public int UkupanBrojUcenika { get; set; }
        public virtual CodebookItemBO JezikNastave { get; set; }
        public virtual CodebookItemBO VrstaOdeljenja { get; set; }
    }
}
