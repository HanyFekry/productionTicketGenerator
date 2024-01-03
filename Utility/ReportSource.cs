using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Utility
{
    public class ReportSource
    {
        public ReportSource(byte[] barcode)
        {
            BarcodeImage = barcode;
        }
        public byte[] BarcodeImage { get; set; }
    }
    
}