using System;
using System.Collections.Generic;

namespace DesktopApp.Models
{
    public partial class ZdravstvenaUstanova
    {
        public ZdravstvenaUstanova()
        {
            Kontakt = new HashSet<Kontakt>();
            Zahtjev = new HashSet<Zahtjev>();
        }

        public int ZdravstvenaUstanovaId { get; set; }
        public string Naziv { get; set; }
        public string VerifikacioniKod { get; set; }

        public virtual ICollection<Kontakt> Kontakt { get; set; }
        public virtual ICollection<Zahtjev> Zahtjev { get; set; }
    }
}
