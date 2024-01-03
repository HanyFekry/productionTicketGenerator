using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class BarcodeData
    {
        public string item_code { get; set; }
        public string lot_number { get; set; }
        public string item_uom { get; set; }
        public DateTime production_date { get; set; }
        public string expiration_date { get { return "01/01/2030"; } set { } }
        public string trading_no { get; set; }
        public string trading_partner { get; set; }
        public double gweight { get; set; }
        public double nweight { get; set; }
        public string wooden_no { get; set; }
        public double quantity { get; set; }
        public string Volt { get; set; }
        public string Color { get; set; }
        public double? coil_length { get; set; }
        public int? pallet_id { get; set; }
        public string WO { get; set; }
        public string Size { get; set; }
        public string Desc { get; set; }
        public string CondType { get; set; }
        public string PalletNo { get; set; }
        public double? Width { get; set; } // WIDTH
        public float? Diameter { get; set; } // DIAMETER
        public int NoOfBoxes { get; set; }
        public byte[] BarcodeImage { get; set; }
    }
}
