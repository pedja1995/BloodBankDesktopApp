using System;
using System.Collections.Generic;

namespace DesktopApp.Models
{
    public partial class Zahtjev
    {
        public Zahtjev()
        {
            Isporuka = new HashSet<Isporuka>();
        }

        public int ZahtjevId { get; set; }
        public string KrvnaGrupaZahtjev { get; set; }
        public string TipKrvnogDerivata { get; set; }
        public short? ZahtjevanaKolicina { get; set; }
        public DateTime? DatumPodnosenjaZahtjeva { get; set; }
        public short? ZahtjevPrihvacen { get; set; }
        public string Napomena { get; set; }
        public int? ZdravstvenaUstanovaId { get; set; }

        public virtual ZdravstvenaUstanova ZdravstvenaUstanova { get; set; }
        public virtual ICollection<Isporuka> Isporuka { get; set; }
    }
}
