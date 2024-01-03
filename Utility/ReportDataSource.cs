using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class ReportDataSource
    {
        public List<ReportSource> GetDataSource(byte[] barcode)
        {
            List<ReportSource> lstReportSources = new List<ReportSource>();
            var reportSource = new ReportSource(barcode);
            lstReportSources.Add(reportSource);
            return lstReportSources;
            //List<byte[]> lstStr = new List<byte[]>();
            //lstStr.Add(barcode);
            //return lstStr;

        }
    }
}
