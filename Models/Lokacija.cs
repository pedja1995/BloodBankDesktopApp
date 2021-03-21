using System;
using System.Collections.Generic;

namespace DesktopApp.Models
{
    public partial class Lokacija
    {
        public Lokacija()
        {
            Kontakt = new HashSet<Kontakt>();
            Magacin = new HashSet<Magacin>();
        }

        public int LokacijaId { get; set; }
        public string Adresa { get; set; }
        public string Mjesto { get; set; }
        public string PostanskiBroj { get; set; }

        public virtual ICollection<Kontakt> Kontakt { get; set; }
        public virtual ICollection<Magacin> Magacin { get; set; }
    }
}
