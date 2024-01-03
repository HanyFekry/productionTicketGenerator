using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class SD
    {
        public const string AutomotiveUser = "Automotive User";
        public const string QualityUser = "Quality User";
        public const string QualityAdmin = "Quality Admin";

        public const string SearchItemSeparator = "$$$";
        public enum Qstatus { Submitted = 1, Approved = 2, Rejected = 3, Solved = 4, ReAllocated = 5 }
        public const string Error401Page = @"/Errors/Error401.aspx";
        public const string DrumUOM = "Meter";
        public const string CoilUOM = "Meter";
        public enum ProductivitySource { Production = 1, Quality = 2 }
    }
}
