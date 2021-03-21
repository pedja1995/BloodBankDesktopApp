using System;
using System.Collections.Generic;

namespace DesktopApp.Models
{
    public partial class Radnik
    {
        public Radnik()
        {
            Kontakt = new HashSet<Kontakt>();
        }

        public int RadnikId { get; set; }
        public string RadnoMjesto { get; set; }
        public DateTime? DatumZaposlenja { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public DateTime? DatumRodjenja { get; set; }
        public short Admin { get; set; }


        public virtual ICollection<Kontakt> Kontakt { get; set; }
    }
}
