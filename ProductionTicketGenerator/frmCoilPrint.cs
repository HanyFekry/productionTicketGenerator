using DBLayer;
using DomainClasses;
using ProductionTicketGenerator.AppCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Utility;

namespace ProductionTicketGenerator
{
    public partial class frmCoilPrint : Form
    {
        ProgramHelper helper = new ProgramHelper();
        public frmCoilPrint()
        {
            InitializeComponent();
        }
        private void frmCoilPrint_Load(object sender, EventArgs e)
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
                    ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 180;
                    //IQueryable<Pallet> query = context.Pallets
                    //        .Where(p => p.QStatus.Equals(SD.Qstatus.Approved.ToString()));
                    //query = ProductionDate == null ? query : query.Where(d => DbFunctions.TruncateTime(((DateTime)d.ClosureDate)) == DbFunctions.TruncateTime(ProductionDate));
                    //query = opf == "اختر ..." ? query : query.Where(d => d.Opf == opf);
                    //var pallets = query.Select(d => new { d.Id,d.Opf, d.Sn, d.ClosureDate }).ToList().Distinct().OrderBy(d => d.Opf).ThenBy(d => d.Sn).ToList();
                    IQueryable<Pallet> query = context.Pallets.Include(p => p.PalletsQualityStatus)
                           .Where(p => p.QStatus.Equals(SD.Qstatus.Approved.ToString()));
                    if (ProductionDate != null)
                        query = query.Where(d => DbFunctions.TruncateTime((d.PalletsQualityStatus.Max(qs => qs.QualityStatusDate))) == DbFunctions.TruncateTime(ProductionDate));
                    if (opf != "اختر ...")
                        query = query.Where(d => d.Opf == opf);
                    var pallets = query.Select(d => new { d.Id, d.Opf, d.Sn, ClosureDate = d.PalletsQualityStatus.Max(qs => qs.QualityStatusDate) }).ToList().Distinct().OrderBy(d => d.Opf).ThenBy(d => d.Sn).ToList();

                    gvTickets.DataSource = pallets;
                    DataGridViewCheckBoxColumn chkPrint = new DataGridViewCheckBoxColumn();
                    DataGridViewTextBoxColumn sn = new DataGridViewTextBoxColumn();
                    sn.HeaderText = "No";
                    chkPrint.HeaderText = "print";
                    gvTickets.Columns.Insert(0, chkPrint);
                    gvTickets.Columns.Insert(2, sn);
                    gvTickets.Columns[1].Visible = false;
                    for(int i = 1; i <= gvTickets.Rows.Count; i++)
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
                string opf = string.Empty;// gvTickets.SelectedRows[0].Cells[0].Value.ToString();
                int palletId;//= Convert.ToInt32(gvTickets.SelectedRows[0].Cells[1].Value);
                BarcodeData bData;
                StringBuilder sb = new StringBuilder();
                List<string> lstExclude = new List<string> { "NonStdLength",  "WO", "Size", "Desc", "CondType", "Width", "Diameter", "BarcodeImage" };
                using (var context = new ManufacturingContext())
                {
                    for (int i = 0; i < checkedRows.Count; i++)
                    {
                        //opf = checkedRows[i].Cells[1].Value.ToString();
                        palletId = Convert.ToInt32(checkedRows[i].Cells[1].Value);
                        var palletDetails = context.CoileProductivities
                            .Include(c => c.WoHeader)
                            .Include(c => c.Pallet.PalletsQualityStatus)
                            .Include(c => c.Color)
                            .Where(c => c.PalletId == palletId)
                            .GroupBy(c => new { c.Wo, colorEn = c.Color.EnName, c.CoileLength, c.NonStdLength, c.WoHeader.CableCode, c.Pallet.Opf, palletId = c.Pallet.Id, c.Pallet.Sn, c.WoHeader.Customer,ClosureDate= c.Pallet.PalletsQualityStatus.Max(qs => qs.QualityStatusDate), c.WoHeader.Size, c.WoHeader.Description, c.WoHeader.ConductorType })
                            .Select(c => new BarcodeData
                            {
                                item_code = c.Key.CableCode,
                                lot_number = c.Key.Wo + "/" + c.Key.colorEn + "/" + c.Key.Sn + "/" + (c.Key.CoileLength>0?Math.Round(c.Key.CoileLength, 2):Math.Round(c.Key.NonStdLength, 2)),
                                coil_length = Math.Round(c.Key.CoileLength, 2),
                                NonStdLength = Math.Round(c.Key.NonStdLength, 2),
                                item_uom = SD.CoilUOM,
                                production_date = (DateTime)c.Key.ClosureDate,
                                trading_no = c.Key.Opf,
                                trading_partner = c.Key.Customer,
                                gweight = Math.Round(c.Sum(cc => cc.GrossWeight ?? 0), 2),
                                nweight = Math.Round(c.Sum(cc => cc.NetWeight ?? 0), 2),
                                wooden_no = null,
                                quantity = c.Sum(cc => cc.NoOfCoiles),
                                pallet_id = c.Key.palletId,
                                Totalquantity= c.Sum(cc => cc.TotalLength),

                                WO = c.Key.Wo,
                                Size = c.Key.Size,
                                Desc = c.Key.Description,
                                CondType = c.Key.ConductorType,
                                pallet_no = c.Key.Opf + "/" + c.Key.Sn,
                                Volt = null,
                                Color = c.Key.colorEn
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
                            var ImageStream = barCodeHelper.GenerateBarcode(sb.ToString(), 130, 55);
                            //PrintHelper printHelper = new PrintHelper(barCodeHelper.GenerateBarcode(sb.ToString(), 300, 188), new List<int> { 10, 35, 260, 250 });
                            //printHelper.PrintImg();
                            //MemoryStream ms = new MemoryStream();
                            //ImageStream.CopyTo(ms);
                            //coil.BarcodeImage = ms.ToArray();
                            coil.BarcodeImage = ((MemoryStream)ImageStream).ToArray();
                            string _ReportPath = @"ProductionTicketGenerator.Reports.rptCoil.rdlc";
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
        void PrintpalletBarCode(int palletId)
        {
            try
            {
                using (var context = new ManufacturingContext())
                {
                    StringBuilder sb = new StringBuilder();
                    List<string> lstExclude = new List<string> { "NonStdLength",  "WO", "Size", "Desc", "CondType", "PalletNo", "Width", "Diameter", "NoOfBoxes" };
                    var coilsBarcode = context.CoileProductivities
                        .Include(c => c.WoHeader)
                        .Include(c => c.Pallet)
                        .Include(c => c.Color)
                        .Where(c => c.PalletId == palletId)
                        .GroupBy(c => new { c.Wo, colorEn = c.Color.EnName, c.CoileLength, c.WoHeader.CableCode, c.Pallet.Opf, palletId = c.Pallet.Id, c.Pallet.Sn, c.WoHeader.Customer, c.Pallet.ClosureDate, c.WoHeader.Size, c.WoHeader.Description, c.WoHeader.ConductorType })
                        .Select(c => new BarcodeData
                        {
                            item_code = c.Key.CableCode,
                            lot_number = c.Key.Wo + "/" + c.Key.colorEn + "/" + c.Key.Sn,
                            coil_length = Math.Round(c.Key.CoileLength, 2),
                            item_uom = SD.CoilUOM,
                            production_date = (DateTime)c.Key.ClosureDate,
                            trading_no = c.Key.Opf,
                            trading_partner = c.Key.Customer,
                            gweight = Math.Round(c.Sum(cc => cc.GrossWeight ?? 0), 2),
                            nweight = Math.Round(c.Sum(cc => cc.NetWeight ?? 0), 2),
                            wooden_no = null,
                            quantity = c.Sum(cc => cc.NoOfCoiles),
                            pallet_id = c.Key.palletId,

                            WO = c.Key.Wo,
                            Size = c.Key.Size,
                            Desc = c.Key.Description,
                            CondType = c.Key.ConductorType,
                            pallet_no = c.Key.Opf + "/" + c.Key.Sn,
                            Volt = null,
                            Color = c.Key.colorEn
                        })
                        .ToList();
                    if (coilsBarcode != null && coilsBarcode.Count > 0)
                    {
                        foreach (var coil in coilsBarcode)
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
                            var ImageStream = barCodeHelper.GenerateBarcode(sb.ToString(), 130, 55);
                            //PrintHelper printHelper = new PrintHelper(barCodeHelper.GenerateBarcode(sb.ToString(), 300, 188), new List<int> { 10, 35, 260, 250 });
                            //printHelper.PrintImg();
                        }

                        //PrintClientSide(strBarcodes);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void gvTickets_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            //string opf = gvTickets.SelectedRows[0].Cells[1].Value.ToString();
            int palletId = Convert.ToInt32(gvTickets.SelectedRows[0].Cells[1].Value);
            using (var context = new ManufacturingContext())
            {
                var coilsBarcode = context.CoileProductivities
                        .Include(c => c.WoHeader)
                        .Include(c => c.Pallet)
                        .Include(c => c.Color)
                        .Where(c => c.PalletId == palletId)
                        .GroupBy(c => new { c.Wo, colorEn = c.Color.EnName, c.CoileLength, c.NonStdLength, c.WoHeader.CableCode, c.Pallet.Opf, palletId = c.Pallet.Id, c.Pallet.Sn, c.WoHeader.Customer, c.Pallet.ClosureDate, c.WoHeader.Size, c.WoHeader.Description, c.WoHeader.ConductorType })
                        .Select(c => new 
                        {
                            WO = c.Key.Wo,
                            Size = c.Key.Size,
                            quantity = c.Sum(cc => cc.NoOfCoiles),
                            //PalletNo = c.Key.Opf + "/" + c.Key.Sn,
                            coil_length = Math.Round(c.Key.CoileLength>0? c.Key.CoileLength: c.Key.NonStdLength, 2),
                            Color = c.Key.colorEn
                        })
                        .ToList();
                gvWos.DataSource = coilsBarcode;
            }
        }

    }
}
