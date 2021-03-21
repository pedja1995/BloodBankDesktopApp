using System;
using System.Collections.Generic;

namespace DesktopApp.Models
{
    public partial class Isporuka
    {
        public Isporuka()
        {
            DozaKrvi = new HashSet<DozaKrvi>();
        }

        public int IsporukaId { get; set; }
        public DateTime? DatumIsporuke { get; set; }
        public int? ZahtjevId { get; set; }

        public virtual Zahtjev Zahtjev { get; set; }
        public virtual ICollection<DozaKrvi> DozaKrvi { get; set; }
    }
}
