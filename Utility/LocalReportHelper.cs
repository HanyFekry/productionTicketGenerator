using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections.Generic;
//using System.Windows.Forms;
//using Microsoft.Reporting.WinForms;
////using ElsewedyCables.Reports.BarcodeGenerator;
//using System.Configuration;
//using InProcessProductionManager;
//using InProcessProductionManager.AppData;
using System.Drawing;
using Microsoft.Reporting.WebForms;

namespace Utility
{
    public class LocalReportHelper : IDisposable
    {
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        private object[] _ReportParameters;
        public object[] ReportParameters
        {
            get { return _ReportParameters; }
            set { _ReportParameters = value; }
        }

        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + name + "." + fileNameExtension, FileMode.Create);
            m_streams.Add(stream);
            return stream;
        }
        // Export the given report as an EMF (Enhanced Metafile) file.
        private void Export(LocalReport report)
        {
            string deviceInfo =
              "<DeviceInfo>" +
              "  <OutputFormat>EMF</OutputFormat>" +
              "  <PageWidth>2.10in</PageWidth>" +
              "  <PageHeight>1.40in</PageHeight>" +
              "  <MarginTop>0.0in</MarginTop>" +
              "  <MarginLeft>0.0in</MarginLeft>" +
              "  <MarginRight>0.0in</MarginRight>" +
              "  <MarginBottom>0.0in</MarginBottom>" +
              "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            //report.Render("Image", deviceInfo, CreateStream, out warnings);
            try
            {
                report.Render("Image", deviceInfo, CreateStream, out warnings);
            }
            catch(Exception ex) { }
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

            //Graphics g = ev.Graphics;
            //g.TranslateTransform(50, 0);
            //g.RotateTransform(ev.PageSettings.Landscape ? 30 : 60);
            //g.DrawString("UIC Software Development", new Font("Arial", 36, FontStyle.Bold),
            //             new SolidBrush(Color.FromArgb(64, Color.Black)), 0, 0);
        }

        private void Print(short PrintCount, string printerName)
        {
            //const string printerName = "Microsoft Office Document Image Writer";
            PrintDocument printDoc = new PrintDocument();
            if (!string.IsNullOrEmpty(printerName))
            {
                printDoc.PrinterSettings.PrinterName = printerName;
                printDoc.PrinterSettings.Copies = PrintCount;
            }
            if (m_streams == null || m_streams.Count == 0)
                return;
            if (!printDoc.PrinterSettings.IsValid)
            {
                string msg = String.Format(
                   "Can't find printer \"{0}\".", printerName);
                //MessageBox.Show(msg, "Print Error");
                return;
            }
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            printDoc.Print();
        }
        // Create a local report for Report.rdlc, load the data,
        //    export the report to an .emf file, and print it.
        public void Run(LocalReport report, short PrintCount, string printerName)
        {
            //LocalReport report = new LocalReport();
            ////report.ReportPath = @"..\..\rptProductionTicket_DR1.rdlc";
            ////report.ReportPath = @"E:\ElSewedySolutions\Weight\WeightSystem_Old\WeightSystem\rptProductionTicket_DR1.rdlc";
            //report.ReportEmbeddedResource = ReportPath;
            ////report.DataSources.Add(new ReportDataSource("DataSet1", LoadProductionTicketData()));
            //BindingSource bindingSource = new BindingSource();
            //bindingSource.DataSource = GetReportDataSource();
            //report.DataSources.Add(new ReportDataSource("DataSet1", bindingSource));
            Export(report);
            m_currentPageIndex = 0;
            Print(PrintCount, printerName);
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