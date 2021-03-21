using System;
using System.Collections.Generic;

namespace DesktopApp.Models
{
    public partial class Kontakt
    {
        public int KontaktId { get; set; }
        public string BrojTelefona { get; set; }
        public string Email { get; set; }
        public int? LokacijaId { get; set; }
        public int? ZdravstvenaUstanovaId { get; set; }
        public int? RadnikId { get; set; }
        public int? DonorId { get; set; }

        public virtual Donor Donor { get; set; }
        public virtual Lokacija Lokacija { get; set; }
        public virtual Radnik Radnik { get; set; }
        public virtual ZdravstvenaUstanova ZdravstvenaUstanova { get; set; }
    }
}
