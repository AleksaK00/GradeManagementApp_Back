namespace GradeManagementApp_Back.Models
{
    public class ClassBO
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public bool KombinovanoOdeljenje { get; set; }
        public bool CelodnevnaNastava { get; set; }
        public bool IzdvojenoOdeljenje { get; set; }
        public string NazivIzdvojeneSkole { get; set; }
        public string OdeljenskiStaresina { get; set; }
        public string Smena { get; set; }
        public bool DvojezicnoOdeljenje { get; set; }
        public int UkupanBrojUcenika { get; set; }
        public int BrojUcenika { get; set; }
        public int BrojUcenica { get; set; }
        public virtual GradeBO Grade { get; set; }
        public virtual CodebookItemBO JezikNastave { get; set; }
        public virtual CodebookItemBO PrviStraniJezik { get; set; }
        public virtual CodebookItemBO VrstaOdeljenja { get; set; }
    }
}
