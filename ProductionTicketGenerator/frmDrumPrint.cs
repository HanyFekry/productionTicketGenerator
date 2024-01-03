using DBLayer;
using DomainClasses;
using ProductionTicketGenerator.AppCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Utility;

namespace ProductionTicketGenerator
{
    public partial class frmDrumPrint : Form
    {
        ProgramHelper helper = new ProgramHelper();
        public frmDrumPrint()
        {
            InitializeComponent();
        }
        private void frmDrumPrint_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            using (var context = new ManufacturingContext())
            {
                var opfs = context.WoHeaders
                    .Select(p => new StringItem { Name = p.OPF }).Distinct().OrderBy(o => o.Name).ToList();
                helper.BindCompo(cmbOPF, opfs, true, "Name", "Name");
            }
            BindPrinterCmbo(cmbPrinter);
        }
        void BindPrinterCmbo(ComboBox cmb)
        {
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cmb.Items.Add(printer);
                if (printer.ToLower().Contains("zd220"))
                    cmb.SelectedItem = (object)printer;
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Dispose();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                gvTickets.DataSource = null;
                gvTickets.Columns.Clear();
                var opf = cmbOPF.SelectedValue.ToString();
                var ProductionDate = dtpProduction.Value;
                using (var context = new ManufacturingContext())
                {
                    IQueryable<DrumProductivity> query = context.DrumProductivities;
                    //bool isPrinted;
                    query = ProductionDate == null ? query : query.Where(d => DbFunctions.TruncateTime(((DateTime)d.ProductionDate)) == DbFunctions.TruncateTime(ProductionDate));
                    query = opf == "اختر ..." ? query : query.Where(d => d.Wo.StartsWith(opf));
                    var pallets = query.Select(d => new { d.Id, d.Wo, d.SN, d.WoodenNo, d.NetWeight, d.GrossWeight, d.LengthKm }).ToList().OrderBy(d => d.Wo).ThenBy(d => d.SN).ToList();
                    //pallets.ForEach(p => { p.isPrinted = Convert.ToBoolean(p.PrintCount); });
                    gvTickets.DataSource = pallets;
                    DataGridViewCheckBoxColumn chkPrint = new DataGridViewCheckBoxColumn();
                    DataGridViewTextBoxColumn sn = new DataGridViewTextBoxColumn();
                    sn.HeaderText = "No";
                    chkPrint.HeaderText = "print";
                    gvTickets.Columns.Insert(0, chkPrint);
                    gvTickets.Columns.Insert(2, sn);
                    gvTickets.Columns[1].Visible = false;
                    for (int i = 1; i <= gvTickets.Rows.Count; i++)
                    {
                        gvTickets.Rows[i - 1].Cells[2].Value = i;
                    }

                }
            }
            catch (Exception ex) { }
        }

        private void frmAutomotivePrint_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            this.Dispose();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var checkedRows = this.gvTickets.Rows.Cast<DataGridViewRow>()
                         .Where(row => (bool?)row.Cells[0].Value == true)
                         .ToList();
            //if (gvTickets.SelectedRows.Count > 0)
            //{
            //    string opf = gvTickets.SelectedRows[0].Cells[0].Value.ToString();
            //    int palletNo = Convert.ToInt32(gvTickets.SelectedRows[0].Cells[1].Value);
            if (checkedRows.Count > 0)
            {
                string wo = string.Empty;// gvTickets.SelectedRows[0].Cells[0].Value.ToString();
                string woodenNo;//= Convert.ToInt32(gvTickets.SelectedRows[0].Cells[1].Value);
                BarcodeData bData;
                StringBuilder sb = new StringBuilder();
                List<string> lstExclude = new List<string> { "NonStdLength", "WO", "Size", "Desc", "CondType", "Width", "Diameter", "Totalquantity", "BarcodeImage" };
                using (var context = new ManufacturingContext())
                {
                    for (int i = 0; i < checkedRows.Count; i++)
                    {
                        wo = checkedRows[i].Cells[3].Value.ToString().Trim().ToLower();
                        woodenNo = checkedRows[i].Cells[5].Value.ToString().Trim().ToLower();
                        var drum = context.DrumProductivities
                            .Include(c => c.WoHeader)
                            .Include(c => c.Color)
                            .FirstOrDefault(d => d.Wo.Trim().ToLower() == wo && d.WoodenNo.Trim().ToLower() == woodenNo);
                        var drumBarcode = new BarcodeData
                        {
                            item_code = drum.WoHeader.CableCode,
                            lot_number = drum.SN + "/" + drum.Wo,
                            item_uom = SD.DrumUOM,
                            production_date = drum.ProductionDate,
                            trading_no = drum.WoHeader.OPF,
                            trading_partner = drum.WoHeader.Customer,
                            gweight = Math.Round(drum.GrossWeight, 3),
                            nweight = Math.Round(drum.NetWeight, 3),
                            wooden_no = drum.WoodenNo,
                            quantity = Math.Round(drum.LengthKm * 1000, 2),

                            Desc = drum.WoHeader.Description,
                            Size = drum.WoHeader.Size,
                            WO = drum.Wo,
                            Volt = drum.WoHeader.Volt,
                            Color = drum.Color != null ? drum.Color.EnName : "NA",
                            coil_length = null,
                            pallet_id = null,
                            pallet_no = null,
                            Width = drum.Width,
                            Diameter = drum.Diameter
                        };
                        if (drumBarcode != null)
                        {
                            StringBuilder sbAttributes = new StringBuilder();
                            sb.Clear();
                            sb.Append("q_drum ");
                            drumBarcode.GetType().GetProperties().ToList().ForEach(p =>
                            {
                                if (lstExclude.Contains(p.Name) == false)
                                {
                                    sb.Append(p.Name + ":");
                                    if (p.GetValue(drumBarcode) != null)
                                    {
                                        if (p.GetValue(drumBarcode).GetType() == typeof(DateTime))
                                        {
                                            sb.Append(((DateTime)p.GetValue(drumBarcode)).ToString("dd/MM/yyyy"));
                                        }
                                        else if (double.TryParse(p.GetValue(drumBarcode).ToString(), out _))
                                        {
                                            sb.Append(Math.Round(Convert.ToDouble(p.GetValue(drumBarcode).ToString()), 2));
                                        }
                                        else
                                        {
                                            sb.Append(p.GetValue(drumBarcode).ToString());
                                        }
                                    }
                                    sb.Append("#");
                                }
                            });

                            BarCodeHelper barCodeHelper = new BarCodeHelper();
                            var ImageStream = barCodeHelper.GenerateBarcode(sb.ToString(), 130, 55);
                            //PrintHelper printHelper = new PrintHelper(barCodeHelper.GenerateBarcode(sb.ToString(), 300, 188), new List<int> { 10, 35, 260, 250 });
                            //printHelper.PrintImg();
                            //MemoryStream ms = new MemoryStream();
                            //ImageStream.CopyTo(ms);
                            //coil.BarcodeImage = ms.ToArray();
                            drumBarcode.BarcodeImage = ((MemoryStream)ImageStream).ToArray();
                            string _ReportPath = @"ProductionTicketGenerator.Reports.rptDrum.rdlc";
                            using (PrintHelper PrintHelper = new PrintHelper())
                            {
                                //PrintHelper.ReportAttributes = GetReportAttributesFromDB(_ticketID, out _ReportPath);
                                PrintHelper.ReportAttributes = drumBarcode;
                                PrintHelper.Run(_ReportPath, Convert.ToInt16(1), cmbPrinter.SelectedItem.ToString());

                            }
                        }
                    }
                }
            }
            else
                MessageBox.Show("من فضلك اختر بالتة أولا ", "خطأ");

        }

    }
}
