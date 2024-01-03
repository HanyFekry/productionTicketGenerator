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
    public partial class frmAutomotivePrint : Form
    {
        ProgramHelper helper = new ProgramHelper();
        public frmAutomotivePrint()
        {
            InitializeComponent();
        }
        private void frmAutomotivePrint_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            using (var context = new ManufacturingContext())
            {
                var opfs = context.LeoniOPFs
                    .Select(p => new StringItem { Name = p.OPF }).OrderBy(o => o.Name).ToList();
                var lOpfs = opfs.Where(o => o.Name.Trim().StartsWith("00")).ToList();
                var aOpfs = opfs.Where(o => !o.Name.Trim().StartsWith("00")).ToList();
                helper.BindCompo(cmbOPF, lOpfs, true, "Name", "Name");
                helper.BindCompo(cmbOPFAuto, aOpfs, true, "Name", "Name");
            }
            BindPrinterCmbo(cmbPrinter);
            BindPrinterCmbo(cmbPrinterAuto);
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
                    IQueryable<LeoniPalletDetails> query = context.LeoniPalletDetails;
                    query = ProductionDate == null ? query : query.Where(d => DbFunctions.TruncateTime(((DateTime)d.ProductionDate)) == DbFunctions.TruncateTime(ProductionDate));
                    query = opf == "اختر ..." ? query : query.Where(d => d.OPF == opf);
                    var pallets = query.Select(d => new { d.OPF, d.PalletNo, d.NoOfBox, NWeight = Math.Round(d.NWeight, 2), GWeight = Math.Round(d.GWeight, 2), d.ProductionDate }).ToList().Distinct().OrderBy(d => d.OPF).ThenBy(d => d.PalletNo).ToList();
                    gvTickets.DataSource = pallets;
                    DataGridViewCheckBoxColumn chkPrint = new DataGridViewCheckBoxColumn();
                    chkPrint.HeaderText = "print";
                    gvTickets.Columns.Insert(0, chkPrint);

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
            if (!string.IsNullOrEmpty(txtSalesOrder.Text))
            {
                if (checkedRows.Count > 0)
                {
                    string opf = string.Empty;// gvTickets.SelectedRows[0].Cells[0].Value.ToString();
                    string salesOrder = txtSalesOrder.Text.Trim();
                    int palletNo;//= Convert.ToInt32(gvTickets.SelectedRows[0].Cells[1].Value);
                    //BarcodeData bData;
                    StringBuilder sb = new StringBuilder();
                    List<string> lstExclude = new List<string> { "NonStdLength",  "WO", "Size", "Desc", "CondType", "Width", "Diameter", "Totalquantity", "BarcodeImage" };
                    using (var context = new ManufacturingContext())
                    {
                        for (int i = 0; i < checkedRows.Count; i++)
                        {
                            opf = checkedRows[i].Cells[1].Value.ToString();
                            palletNo = Convert.ToInt32(checkedRows[i].Cells[2].Value);
                            var palletDetails = context.LeoniPalletDetails.Include(d => d.WoHeader).Where(d => d.OPF.Equals(opf) && d.PalletNo.Equals(palletNo))
                                .Select(pd => new BarcodeData
                                {

                                    item_code = pd.WoHeader.CableCode,
                                    lot_number = pd.WO + "/" + pd.PalletNo,
                                    coil_length = pd.BoxLength,
                                    item_uom = SD.CoilUOM,
                                    production_date = pd.ProductionDate,
                                    trading_no = salesOrder,//pd.OPF,
                                    trading_partner = pd.WoHeader.Customer,
                                    gweight = pd.GWeight,
                                    nweight = pd.NWeight,
                                    wooden_no = null,
                                    quantity = pd.PNoOfBox,
                                    pallet_id = null,

                                    WO = pd.WO,
                                    Size = pd.WoHeader.Size,
                                    Desc = pd.WoHeader.Description,
                                    CondType = pd.WoHeader.ConductorType,
                                    pallet_no = pd.OPF + "/" + pd.PalletNo.ToString(),
                                    Volt = null,
                                    Color = "N/A",
                                    Totalquantity = pd.Quantity
                                })
                                .ToList();
                            foreach (var coil in palletDetails)
                            {

                                sb.Clear();
                                sb.AppendLine("q_coil");
                                coil.GetType().GetProperties().ToList().ForEach(p =>
                                {
                                    if (lstExclude.Contains(p.Name) == false)
                                    {
                                        sb.Append(p.Name + ":");
                                        if (p.GetValue(coil) != null)
                                        {
                                            if (p.GetValue(coil).GetType() == typeof(DateTime))
                                            {
                                                sb.Append(((DateTime)p.GetValue(coil)).ToString("dd/MM/yyyy"));
                                            }
                                            else
                                            {
                                                sb.Append(p.GetValue(coil).ToString());
                                            }
                                        }
                                        sb.Append("#");
                                    }
                                });

                                BarCodeHelper barCodeHelper = new BarCodeHelper();
                                var ImageStream = barCodeHelper.GenerateBarcode(sb.ToString(), 175, 125);
                                //PrintHelper printHelper = new PrintHelper(barCodeHelper.GenerateBarcode(sb.ToString(), 300, 188), new List<int> { 10, 35, 260, 250 });
                                //printHelper.PrintImg();
                                //MemoryStream ms = new MemoryStream();
                                //ImageStream.CopyTo(ms);
                                //coil.BarcodeImage = ms.ToArray();
                                coil.BarcodeImage = ((MemoryStream)ImageStream).ToArray();
                                string _ReportPath = @"ProductionTicketGenerator.Reports.rptCoil_Leoni.rdlc";
                                using (PrintHelper PrintHelper = new PrintHelper())
                                {
                                    //PrintHelper.ReportAttributes = GetReportAttributesFromDB(_ticketID, out _ReportPath);
                                    PrintHelper.ReportAttributes = coil;
                                    PrintHelper.Run(_ReportPath, Convert.ToInt16(1), cmbPrinter.SelectedItem.ToString());

                                }
                            }
                        }
                    }
                }
                else
                    MessageBox.Show("من فضلك اختر بالتة أولا ", "خطأ");
            }
            else
                MessageBox.Show("من فضلك أدخل أمر التصنيع أولا ", "خطأ");

        }

        private void gvTickets_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            string opf = gvTickets.SelectedRows[0].Cells[1].Value.ToString();
            int palletNo = Convert.ToInt32(gvTickets.SelectedRows[0].Cells[2].Value);
            using (var context = new ManufacturingContext())
            {
                var palletDetails = context.LeoniPalletDetails.Include(d => d.WoHeader).Where(d => d.OPF.Equals(opf) && d.PalletNo.Equals(palletNo))
                    .Select(d => new { d.WO, d.WoHeader.Size, d.PNoOfBox, d.BoxLength })
                    .OrderBy(w => w.WO)
                    .ThenBy(w => w.Size)
                    .ToList();
                gvWos.DataSource = palletDetails;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSalesOrder.Text) && cmbOPF.SelectedValue != "-1")
            {
                var opf = cmbOPF.SelectedValue.ToString();
                using (var context = new ManufacturingContext())
                {
                    var so = context.SalesOrderOPFs.FirstOrDefault(s => s.OPF.Equals(opf));
                    if (null == so)
                    {
                        so = new SalesOrderOPF();
                        so.OPF = opf;
                        so.SalesOrderNo = txtSalesOrder.Text.Trim();
                        context.SalesOrderOPFs.Add(so);
                        context.SaveChanges();
                        MessageBox.Show("تم الحفظ بنجاح ", "Success");
                    }
                }
            }
            else
            {
                MessageBox.Show("من فضلك أدخل رقم أمر البيع أولا ", "خطأ");
            }
        }

        private void cmbOPF_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtSalesOrder.Text = string.Empty;
                var opf = ((ComboBox)sender).SelectedValue.ToString();
                if (opf != "-1")
                {
                    using (var context = new ManufacturingContext())
                    {
                        var so = context.SalesOrderOPFs.FirstOrDefault(s => s.OPF.Equals(opf));
                        if (null != so)
                        {
                            txtSalesOrder.Text = so.SalesOrderNo;
                            txtSalesOrder.Enabled = false;
                            btnSave.Enabled = false;
                        }
                        else
                        {
                            txtSalesOrder.Enabled = true;
                            btnSave.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex) { }

        }

        private void btnViewAuto_Click(object sender, EventArgs e)
        {
            try
            {
                gvTicketsAuto.DataSource = null;
                gvTicketsAuto.Columns.Clear();
                var opf = cmbOPFAuto.SelectedValue.ToString();
                var ProductionDate = dtpProductionAuto.Value;
                using (var context = new ManufacturingContext())
                {
                    IQueryable<AutoPalletDetails> query = context.AutoPalletDetails;
                    query = ProductionDate == null ? query : query.Where(d => DbFunctions.TruncateTime(((DateTime)d.ProductionDate)) == DbFunctions.TruncateTime(ProductionDate));
                    query = opf == "اختر ..." ? query : query.Where(d => d.OPF == opf);
                    var pallets = query.Select(d => new { d.OPF, d.PalletNo, d.NoOfBox, NWeight = Math.Round(d.NWeight, 2), GWeight = Math.Round(d.GWeight, 2), d.ProductionDate }).ToList().Distinct().OrderBy(d => d.OPF).ThenBy(d => d.PalletNo).ToList();
                    gvTicketsAuto.DataSource = pallets;
                    DataGridViewCheckBoxColumn chkPrint = new DataGridViewCheckBoxColumn();
                    chkPrint.HeaderText = "print";
                    gvTicketsAuto.Columns.Insert(0, chkPrint);

                }
            }
            catch (Exception ex) { }

        }

        private void gvTicketsAuto_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string opf = gvTicketsAuto.SelectedRows[0].Cells[1].Value.ToString();
            int palletNo = Convert.ToInt32(gvTicketsAuto.SelectedRows[0].Cells[2].Value);
            using (var context = new ManufacturingContext())
            {
                var palletDetails = context.AutoPalletDetails.Include(d => d.WoHeader).Where(d => d.OPF.Equals(opf) && d.PalletNo.Equals(palletNo))
                    .Select(d => new { d.WO, d.Size, d.PNoOfBox, d.BoxLength })
                    .OrderBy(w => w.WO)
                    .ThenBy(w => w.Size)
                    .ToList();
                gvPalletDetailsAuto.DataSource = palletDetails;
            }

        }

        private void btnPrintAuto_Click(object sender, EventArgs e)
        {
            var checkedRows = this.gvTicketsAuto.Rows.Cast<DataGridViewRow>()
                         .Where(row => (bool?)row.Cells[0].Value == true)
                         .ToList();
            //if (gvTicketsAuto.SelectedRows.Count > 0)
            //{
            //    string opf = gvTicketsAuto.SelectedRows[0].Cells[0].Value.ToString();
            //    int palletNo = Convert.ToInt32(gvTicketsAuto.SelectedRows[0].Cells[1].Value);
            if (checkedRows.Count > 0)
            {
                string opf = string.Empty;// gvTicketsAuto.SelectedRows[0].Cells[0].Value.ToString();
                int palletNo;//= Convert.ToInt32(gvTicketsAuto.SelectedRows[0].Cells[1].Value);
                BarcodeData bData;
                StringBuilder sb = new StringBuilder();
                List<string> lstExclude = new List<string> { "NonStdLength",  "WO", "Size", "Desc", "CondType", "PalletNo", "Width", "Diameter", "Totalquantity", "BarcodeImage" };
                using (var context = new ManufacturingContext())
                {
                    for (int i = 0; i < checkedRows.Count; i++)
                    {
                        opf = checkedRows[i].Cells[1].Value.ToString();
                        palletNo = Convert.ToInt32(checkedRows[i].Cells[2].Value);
                        var palletDetails = context.AutoPalletDetails.Include(d => d.WoHeader).Where(d => d.OPF.Equals(opf) && d.PalletNo.Equals(palletNo))
                            .Select(pd => new BarcodeData
                            {

                                item_code = pd.WoHeader.CableCode,
                                lot_number = pd.WO + "/" + pd.PalletNo,
                                coil_length = pd.BoxLength,
                                item_uom = SD.CoilUOM,
                                production_date = pd.ProductionDate,
                                trading_no = pd.OPF,
                                trading_partner = pd.WoHeader.Customer,
                                gweight = pd.GWeight,
                                nweight = pd.NWeight,
                                wooden_no = null,
                                quantity = pd.PNoOfBox,
                                pallet_id = null,

                                WO = pd.WO,
                                Size = pd.WoHeader.Size,
                                Desc = pd.WoHeader.Description,
                                CondType = pd.WoHeader.ConductorType,
                                pallet_no = pd.OPF + "/" + pd.PalletNo.ToString(),
                                Volt = null,
                                Color = "N/A",
                                Totalquantity = pd.Quantity
                            })
                            .ToList();
                        int counter = 1;
                        foreach (var coil in palletDetails)
                        {
                            coil.lot_number += "/" + counter.ToString();
                            sb.Clear();
                            sb.AppendLine("q_coil");
                            coil.GetType().GetProperties().ToList().ForEach(p =>
                            {
                                if (lstExclude.Contains(p.Name) == false)
                                {
                                    sb.Append(p.Name + ":");
                                    if (p.GetValue(coil) != null)
                                    {
                                        if (p.GetValue(coil).GetType() == typeof(DateTime))
                                        {
                                            sb.Append(((DateTime)p.GetValue(coil)).ToString("dd/MM/yyyy"));
                                        }
                                        else
                                        {
                                            sb.Append(p.GetValue(coil).ToString());
                                        }
                                    }
                                    sb.Append("#");
                                }
                            });

                            BarCodeHelper barCodeHelper = new BarCodeHelper();
                            var ImageStream = barCodeHelper.GenerateBarcode(sb.ToString(), 175, 120);
                            //PrintHelper printHelper = new PrintHelper(barCodeHelper.GenerateBarcode(sb.ToString(), 300, 188), new List<int> { 10, 35, 260, 250 });
                            //printHelper.PrintImg();
                            //MemoryStream ms = new MemoryStream();
                            //ImageStream.CopyTo(ms);
                            //coil.BarcodeImage = ms.ToArray();
                            coil.BarcodeImage = ((MemoryStream)ImageStream).ToArray();
                            string _ReportPath = @"ProductionTicketGenerator.Reports.rptCoil_Leoni.rdlc";
                            using (PrintHelper PrintHelper = new PrintHelper())
                            {
                                //PrintHelper.ReportAttributes = GetReportAttributesFromDB(_ticketID, out _ReportPath);
                                PrintHelper.ReportAttributes = coil;
                                PrintHelper.Run(_ReportPath, Convert.ToInt16(1), cmbPrinterAuto.SelectedItem.ToString());

                            }
                            counter++;
                        }
                    }
                }
            }
            else
                MessageBox.Show("من فضلك اختر بالتة أولا ", "خطأ");

        }
    }
}
