using System;
using System.Collections.Generic;

namespace DesktopApp.Models
{
    public partial class Donor
    {
        public Donor()
        {
            Donacija = new HashSet<Donacija>();
            Kontakt = new HashSet<Kontakt>();
        }

        public int DonorId { get; set; }
        public string KrvnaGrupaDonor { get; set; }
        public string Pol { get; set; }
        public DateTime? DatumPoslednjegDoniranja { get; set; }
        public DateTime? DatumNajranijegSledecegDoniranja { get; set; }
        public string TipPoslednjegDoniranja { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string RegistarskiBroj { get; set; }
        public DateTime? DatumRodjenja { get; set; }
        public string Lozinka { get; set; }

        public virtual ICollection<Donacija> Donacija { get; set; }
        public virtual ICollection<Kontakt> Kontakt { get; set; }
    }
}
