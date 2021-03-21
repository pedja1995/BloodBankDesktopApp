using System;
using System.Collections.Generic;

namespace DesktopApp.Models
{
    public partial class Potraznja
    {
        public int PotraznjaId { get; set; }
        public string KrvnaGrupaPotraznja { get; set; }
        public int? MagacinId { get; set; }

        public virtual Magacin Magacin { get; set; }
    }
}
