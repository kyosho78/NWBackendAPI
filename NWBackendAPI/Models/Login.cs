using System;
using System.Collections.Generic;

namespace NWBackendAPI.Models
{
    public partial class Login
    {
        public int LoginId { get; set; }
        public string UserName { get; set; } = null!;
        public string PassWord { get; set; } = null!;
        public string? LoginErrorMessage { get; set; }
        public DateTime? RegisterationDate { get; set; }
    }
}
