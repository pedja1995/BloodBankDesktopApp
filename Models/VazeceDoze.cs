using System;
using System.Collections.Generic;

namespace DesktopApp.Models
{
    public partial class VazeceDoze
    {
        public int DozaKrviId { get; set; }

        public virtual DozaKrvi DozaKrvi { get; set; }
    }
}
