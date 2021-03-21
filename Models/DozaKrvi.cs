using System;
using System.Collections.Generic;

namespace DesktopApp.Models
{
    public partial class DozaKrvi
    {
        public int DozaKrviId { get; set; }
        public string SerijskaOznaka { get; set; }
        public short? KolicinaKrvnogDerivataMl { get; set; }
        public string TipKrvnogDerivata { get; set; }
        public string KrvnaGrupaDoza { get; set; }
        public DateTime? DatumIstekaRoka { get; set; }
        public short? IstekaoRok { get; set; }
        public int? DonacijaId { get; set; }
        public int? MagacinId { get; set; }
        public int? IsporukaId { get; set; }

        public virtual Donacija Donacija { get; set; }
        public virtual Isporuka Isporuka { get; set; }
        public virtual Magacin Magacin { get; set; }
        public virtual VazeceDoze VazeceDoze { get; set; }
    }
}
