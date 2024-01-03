using DBLayer;
using DomainClasses;
using ProductionTicketGenerator.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionTicketGenerator.AppCode
{
    public class ContextHelper
    {
        private EncodingOptions EncodingOptions { get; set; }
        private Type Renderer { get; set; }
        public ContextHelper()
        {
            Renderer = typeof(BitmapRenderer);
        }
        internal TestAttributes GetReportAttributesFromDB(long ticketID, out string reportPath)
        {
            reportPath = string.Empty;
            TestAttributes reportAttributes = new TestAttributes();
            try
            {
                using (var context = new InProcessContext())
                {
                    var ticket = context.ProductionTickets.Include(t => t.Drums)
                                                        .Include(t => t.Employee)
                                                        .Include(t => t.Machine)
                                                        .Include(t => t.ScaleDevice)
                                                        .Include(t => t.Shift)
                                                        .Include(t => t.Step)
                                                        .Include(t => t.Supplier)
                                                        .Include(t => t.WoHeader.Size)
                                                        .Include(t => t.ProductionTicketsWos)
                                                        .FirstOrDefault(t => t.Id == ticketID);
                    var drumTo = context.Drums.FirstOrDefault(d => d.Id == ticket.DrumToId);
                    var notes = ticket.ProductionTicketsProductionNotes.OrderBy(n => n.ProductionNoteId).Select(n => new { Note = n.ProductionNote.ArName, n.Value }).ToList();
                    TestResult testResult = context.TestResults
                        .Include(s => s.TestRequest.TestRequestsAttributes.Select(qa => qa.Attribute))
                        .Include(s => s.TestResultsAttributes.Select(sa => sa.Attribute))
                        .Include(s => s.QualityStatus)
                        .Where(s => s.ProductionTickets.Where(t => t.Id == ticketID).Any())
                        .OrderByDescending(s => s.CreationDate)
                        .FirstOrDefault();
                    string strWOs = string.Empty;
                    if (ticket.ProductionTicketTypeId > 1)
                    {
                        ticket.ProductionTicketsWos.ToList().ForEach(tw => strWOs += tw.Wo + " - ");
                        strWOs = strWOs.TrimEnd(" - ".ToCharArray());
                    }
                    else
                        strWOs = ticket.Wo;

                    string strdrumFrom = string.Empty;
                    ticket.Drums.ToList().ForEach(d => strdrumFrom += d.Code + ", ");
                    strdrumFrom = strdrumFrom.TrimEnd(", ".ToCharArray());
                    string _Notes = string.Empty;
                    string currentNote = string.Empty;
                    notes.ForEach(n =>
                    {
                        if (currentNote == n.Note)
                        { _Notes = _Notes.TrimEnd(") - ".ToCharArray()); _Notes += "- " + n.Value + ") - "; }
                        else
                        { _Notes += n.Note + "(" + n.Value + ") - "; }
                        currentNote = n.Note;
                    });

                    //notes.ForEach(n => _Notes += n.Note + "(" + n.Value + ") - ");
                    _Notes = _Notes.TrimEnd("- ".ToCharArray());
                    byte[] _barCode = GenerateBarcode(ticketID.ToString());

                    reportAttributes.ProductionTicketID = ticketID.ToString();
                    reportAttributes.Machine = ticket.Machine.Name;
                    reportAttributes.EmployeeCode = ticket.Employee.Code;
                    reportAttributes.WO = strWOs;
                    reportAttributes.Shift = ticket.Shift.Code;
                    reportAttributes.CableSize = ticket.WoHeader.Size.size;
                    reportAttributes.CableLength = ticket.Length.ToString();
                    reportAttributes.TDSNo = ticket.WoHeader.TdsNo;
                    reportAttributes.DrumNoF = strdrumFrom;
                    reportAttributes.DrumNoT = drumTo.Code;
                    reportAttributes.Material = ticket.Material.ArName;
                    reportAttributes.Supplier = ticket.Supplier.ArName;
                    reportAttributes.QualityStatus = testResult.QualityStatus.ArName;
                    reportAttributes.QualityStatusDate = null != testResult.CreationDate ? ((DateTime)testResult.CreationDate).ToString("dd/MM/yyyy") : string.Empty;
                    reportAttributes.ProductionDate = ((DateTime)ticket.ProductionDate).ToString("dd/MM/yyyy");
                    reportAttributes.Notes = _Notes;
                    reportAttributes.GrossWeight = Convert.ToDouble(ticket.GrossWeight);
                    reportAttributes.NetWeight = Convert.ToDouble(ticket.GrossWeight) > 0 ? Convert.ToDouble(ticket.GrossWeight) - Convert.ToDouble(ticket.TareWeight) : 0;
                    reportAttributes.TareWeight = Convert.ToDouble(ticket.TareWeight);
                    reportAttributes.Scale = ticket.ScaleDevice.Name;
                    reportAttributes.RollNo = ticket.RollNo;
                    reportAttributes.MicaType = ticket.MicaType;
                    reportAttributes.BarcodeImage = _barCode;

                    reportAttributes.GetType().GetProperties().ToList().ForEach(p =>
                    {
                        testResult.TestRequest.TestRequestsAttributes.ToList().ForEach(qa => { if (qa.Attribute.EnName == p.Name) { p.SetValue(reportAttributes, qa.Value); } });
                        testResult.TestResultsAttributes.ToList().ForEach(sa => { if (sa.Attribute.EnName == p.Name) { p.SetValue(reportAttributes, sa.Value); } });
                    });
                    //reportAttributes.GetType().GetProperties().ToList().ForEach(p =>
                    //                { testResult.TestResultsAttributes.ToList().ForEach(sa => { if (sa.Attribute.EnName == p.Name) { p.SetValue(reportAttributes, sa.Value); } }); });
                    reportPath = GetReportPath(ticket.Step.ProcessID, ticket.ProductionTicketTypeId);
                }
            }
            catch(Exception ex) { }
            return reportAttributes;
        }
        byte[] GenerateBarcode(string barcodeValue)
        {
            try
            {
                var writer = new BarcodeWriter
                {
                    Format = BarcodeFormat.CODE_39,
                    Options = EncodingOptions ?? new EncodingOptions
                    {
                        Height = 188,
                        Width = 500,
                        GS1Format = false,
                        Margin = 0,
                        PureBarcode = false
                    },
                    Renderer = (IBarcodeRenderer<Bitmap>)Activator.CreateInstance(Renderer)
                };
                System.Drawing.Bitmap b = writer.Write(barcodeValue);
                byte[] _barCode;
                MemoryStream ms = new MemoryStream();
                b.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                _barCode = ms.ToArray();
                return _barCode;
            }
            catch (Exception exc)
            {
                //MessageBox.Show(this, exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new byte[1];
            }

        }
        string GetReportPath(int? processID, int? ProductionTicketTypeID)
        {
            string _ReportPath;
            if (ProductionTicketTypeID == 1)
            {
                switch (processID)
                {
                    case 1:
                        _ReportPath = @"ProductionTicketGenerator.Reports.ProductionTicket_DR.rdlc";
                        break;
                    case 2:
                        _ReportPath = @"ProductionTicketGenerator.Reports.ProductionTicket_ST.rdlc";
                        break;
                    case 3:
                        _ReportPath = @"ProductionTicketGenerator.Reports.ProductionTicket_MI.rdlc";
                        break;
                    default:
                        _ReportPath = @"ProductionTicketGenerator.Reports.ProductionTicket_DR.rdlc";
                        break;
                }
            }
            else
            {
                switch (processID)
                {
                    case 1:
                        _ReportPath = @"ProductionTicketGenerator.Reports.ProductionTicket_DR_MultiWO.rdlc";
                        break;
                    case 2:
                        _ReportPath = @"ProductionTicketGenerator.Reports.ProductionTicket_ST_MultiWO.rdlc";
                        break;
                    case 3:
                        _ReportPath = @"ProductionTicketGenerator.Reports.ProductionTicket_MI_MultiWO.rdlc";
                        break;
                    default:
                        _ReportPath = @"ProductionTicketGenerator.Reports.ProductionTicket_DR_MultiWO.rdlc";
                        break;
                }

            }
            return _ReportPath;
        }

    }
}
