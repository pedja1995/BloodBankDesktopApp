using System;
using System.Collections.Generic;

namespace DesktopApp.Models
{
    public partial class Magacin
    {
        public Magacin()
        {
            DozaKrvi = new HashSet<DozaKrvi>();
            Potraznja = new HashSet<Potraznja>();
        }

        public int MagacinId { get; set; }
        public string KrvnaGrupaMagacin { get; set; }
        public int? BrojDozaKrvi { get; set; }
        public int? BrojDozaPlazme { get; set; }
        public int? BrojDozaTrombocita { get; set; }
        public int? BrojDozaEritrocita { get; set; }
        public int? LokacijaId { get; set; }

        public virtual Lokacija Lokacija { get; set; }
        public virtual ICollection<DozaKrvi> DozaKrvi { get; set; }
        public virtual ICollection<Potraznja> Potraznja { get; set; }
    }
}
