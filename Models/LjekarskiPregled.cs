using System;
using System.Collections.Generic;

namespace DesktopApp.Models
{
    public partial class LjekarskiPregled
    {
        public int LjekarskiPregledId { get; set; }
        public decimal? Temperatura { get; set; }
        public string KrvniPritisak { get; set; }
        public string Puls { get; set; }
        public decimal? NivoHemoglobina { get; set; }
        public short? TezinaKg { get; set; }
        public string Napomena { get; set; }
        public int? DonacijaId { get; set; }

        public virtual Donacija Donacija { get; set; }
    }
}
