using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Configuration;
using System.Drawing;
//using Utility;

namespace ProductionTicketGenerator.AppCode
{
    public class PrintHelper : IDisposable
    {
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        public BarcodeData ReportAttributes { get; set; }
        private object[] _ReportParameters;
        public object[] ReportParameters
        {
            get { return _ReportParameters; }
            set { _ReportParameters = value; }
        }
        //private DataTable LoadProductionTicketData()
        //{
        //    dsInprocess ds = new dsInprocess();
        //    //// Create a new DataSet and read DrumSticker data file 
        //    ////    data.xml into the first DataTable.
        //    //DataSet dataSet = new DataSet();
        //    //dataSet.ReadXml(@"..\..\data.xml");
        //    //return dataSet.Tables[0];
        //    //_ReportParameters = new object[] { "Size:", "hany", "Grade:", "hany", "Class:", "hany", "Shift:", "hany", "SP.No:", "hany", "Date:", "hany", "NetWeight:", "hany", "OPF:", "hany", _barCode };
        //    ds.Tables["ProductionTicket_DR1"].Rows.Add(ReportParameters);
        //    return ds.Tables["ProductionTicket_DR1"];
        //    //return new DataTable();
        //}
        List<BarcodeData> GetReportDataSource()
        {
            List<BarcodeData> lstAttributes = new List<BarcodeData>();
            lstAttributes.Add(ReportAttributes);
            return lstAttributes;
        }
        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\" + name + "." + fileNameExtension, FileMode.Create);
            m_streams.Add(stream);
            return stream;
        }
        // Export the given report as an EMF (Enhanced Metafile) file.
        private void Export(LocalReport report)
        {
            string deviceInfo =
              "<DeviceInfo>" +
              "  <OutputFormat>EMF</OutputFormat>" +
              "  <PageWidth>4.0in</PageWidth>" +
              "  <PageHeight>6.0in</PageHeight>" +
              "  <MarginTop>0.0in</MarginTop>" +
              "  <MarginLeft>0.15in</MarginLeft>" +
              "  <MarginRight>0.05in</MarginRight>" +
              "  <MarginBottom>0.0in</MarginBottom>" +
              "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            //report.Render("Image", deviceInfo, CreateStream, out warnings);
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }
        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);

            Graphics g = ev.Graphics;
            g.TranslateTransform(50, 0);
            g.RotateTransform(ev.PageSettings.Landscape ? 30 : 60);
            g.DrawString("UIC Software Development", new Font("Arial", 36, FontStyle.Bold),
                         new SolidBrush(Color.FromArgb(64, Color.Black)), 0, 0);
        }

        private void Print(short PrintCount, string printerName)
        {
            //const string printerName = "Microsoft Office Document Image Writer";
            if (string.IsNullOrEmpty(printerName))
            {
                printerName = ConfigurationManager.AppSettings["Printer"].ToString();
            }
            if (m_streams == null || m_streams.Count == 0)
                return;
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = printerName;
            printDoc.PrinterSettings.Copies = PrintCount;
            if (!printDoc.PrinterSettings.IsValid)
            {
                string msg = String.Format(
                   "Can't find printer \"{0}\".", printerName);
                MessageBox.Show(msg, "Print Error");
                return;
            }
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            printDoc.Print();
        }
        // Create a local report for Report.rdlc, load the data,
        //    export the report to an .emf file, and print it.
        public void Run(string ReportPath, short PrintCount, string printerName)
        {
            LocalReport report = new LocalReport();
            //report.ReportPath = @"..\..\rptProductionTicket_DR1.rdlc";
            //report.ReportPath = @"E:\ElSewedySolutions\Weight\WeightSystem_Old\WeightSystem\rptProductionTicket_DR1.rdlc";
            report.ReportEmbeddedResource = ReportPath;
            //report.DataSources.Add(new ReportDataSource("DataSet1", LoadProductionTicketData()));
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = GetReportDataSource();
            report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", bindingSource));
            Export(report);
            m_currentPageIndex = 0;
            Print(PrintCount,printerName);
        }
        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }

    }
}