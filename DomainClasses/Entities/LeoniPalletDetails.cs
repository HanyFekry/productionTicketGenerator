using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses
{
    public class LeoniPalletDetails
    {
        public string OPF { get; set; }
        public string WO { get; set; }
        public string Part { get; set; }
        public int PalletNo { get; set; }
        public double NWeight { get; set; }
        public double GWeight { get; set; }
        public int NoOfBox { get; set; }
        public DateTime ProductionDate { get; set; }
        public int PNoOfBox { get; set; }
        public int BoxLength { get; set; }
        public double Quantity { get; set; }
        [ForeignKey("WO")]
        public WoHeader WoHeader { get; set; }

    }
}
