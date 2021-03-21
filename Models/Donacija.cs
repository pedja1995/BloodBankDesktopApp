using System;
using System.Collections.Generic;

namespace DesktopApp.Models
{
    public partial class Donacija
    {
        public Donacija()
        {
            DozaKrvi = new HashSet<DozaKrvi>();
            LjekarskiPregled = new HashSet<LjekarskiPregled>();
        }

        public int DonacijaId { get; set; }
        public string TipDonacije { get; set; }
        public short? DoniranaKolicinaMl { get; set; }
        public DateTime? DatumDoniranja { get; set; }
        public int? DonorId { get; set; }

        public virtual Donor Donor { get; set; }
        public virtual ICollection<DozaKrvi> DozaKrvi { get; set; }
        public virtual ICollection<LjekarskiPregled> LjekarskiPregled { get; set; }
    }
}
