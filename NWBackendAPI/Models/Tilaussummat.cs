using System;
using System.Collections.Generic;

namespace NWBackendAPI.Models
{
    public partial class Tilaussummat
    {
        public int OrderId { get; set; }
        public decimal? Summa { get; set; }
    }
}
