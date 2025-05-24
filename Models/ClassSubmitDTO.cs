using System.ComponentModel.DataAnnotations;

namespace GradeManagementApp_Back.Models
{
    public class ClassSubmitDTO
    {
        public int? Id { get; set; } = null;

        public int? RazredId { get; set; } = null;

        public string? SkolskaGodina { get; set; }

        public string? Razred { get; set; }

        [Required, StringLength(50, MinimumLength = 1)]
        public string NazivOdeljenja { get; set; }

        [Required]
        public int VrstaOdeljenja { get; set; }

        public string? Program { get; set; }

        [Required]
        public bool KombinovanoOdeljenje { get; set; }

        [Required]
        public bool CelodnevnaNastava { get; set; }

        [Required]
        public bool IzdvojenoOdeljenje { get; set; }

        [StringLength(50)]
        public string? NazivIzdvojeneSkole { get; set; } = null;

        [Required, StringLength(60, MinimumLength = 1)]
        public string OdeljenskiStaresina { get; set; }

        [Required, StringLength(10, MinimumLength = 1)]
        public string Smena { get; set; }

        [Required]
        public int NastavniJezik { get; set; }

        [Required]
        public bool DvoJezicnoOdeljenje { get; set; }

        public int? PrviStraniJezik { get; set; } = null;

        [Required, Range(1, int.MaxValue)]
        public int UkupnoUcenika { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int BrojUcenika { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int BrojUcenica { get; set; }
    }
}
