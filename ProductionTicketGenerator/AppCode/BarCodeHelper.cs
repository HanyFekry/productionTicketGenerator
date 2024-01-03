using System;
using System.Collections.Generic;
using System.Drawing;
//using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.Rendering;

namespace ProductionTicketGenerator.AppCode
{
    public class BarCodeHelper
    {
        private EncodingOptions EncodingOptions { get; set; }
        private Type Renderer { get; set; }
        public BarCodeHelper()
        {
            Renderer = typeof(BitmapRenderer);

        }
        public Stream GenerateBarcode(string barcodeValue, int width = 130, int height = 95)
        {
            MemoryStream ms = new MemoryStream();
            try
            {
                var writer = new BarcodeWriter
                {
                    Format = BarcodeFormat.PDF_417,
                    Options = EncodingOptions ?? new EncodingOptions
                    {
                        Height = height,
                        Width = width,
                        GS1Format = false,
                        Margin = 0,
                        PureBarcode = true
                    },
                    Renderer = (IBarcodeRenderer<Bitmap>)Activator.CreateInstance(Renderer)
                };
                System.Drawing.Bitmap b = writer.Write(barcodeValue);
                b.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms;
            }
            catch (Exception exc)
            {
                //MessageBox.Show(this, exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ms;
            }

        }
        void GenerateBarCode(string barCodeValue)
        {
            // instantiate a writer object
            var barcodeWriter = new BarcodeWriter();

            // set the barcode format
            barcodeWriter.Format = BarcodeFormat.QR_CODE;

            // write text and generate a 2-D barcode as a bitmap
            barcodeWriter
                .Write(barCodeValue)
                .Save(@"C:\Users\jeremy\Desktop\generated.bmp");
        }

        void CreateBarCode()
        {
            // create a barcode reader instance
            IBarcodeReader reader = new BarcodeReader();
            // load a bitmap
            //var barcodeBitmap = (Bitmap)Image.LoadFrom("C:\\sample-barcode-image.png");
            var barcodeBitmap = (Bitmap)Image.FromFile("C:\\sample-barcode-image.png");
            // detect and decode the barcode inside the bitmap
            var result = reader.Decode(barcodeBitmap);
            // do something with the result
            if (result != null)
            {
                var DecoderType = result.BarcodeFormat.ToString();
                var DecoderContent = result.Text;
            }
        }
        //public override void Process(TagHelperContext context, TagHelperOutput output)
        public void Process(string context, Image output)
        {
            //var QrcodeContent = context.AllAttributes["content"].Value.ToString();
            //var alt = context.AllAttributes["alt"].Value.ToString();
            var QrcodeContent = context;
            var width = 250; // width of the Qr Code    
            var height = 250; // height of the Qr Code    
            var margin = 0;
            var qrCodeWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width,
                    Margin = margin
                }
            };
            var pixelData = qrCodeWriter.Write(QrcodeContent);
            // creating a bitmap from the raw pixel data; if only black and white colors are used it makes no difference    
            // that the pixel data ist BGRA oriented and the bitmap is initialized with RGB    
            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            using (var ms = new MemoryStream())
            {
                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image    
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
                // save to stream as PNG    
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                //output.TagName = "img";
                //output.Attributes.Clear();
                //output.Attributes.Add("width", width);
                //output.Attributes.Add("height", height);
                //output.Attributes.Add("alt", alt);
                //output.Attributes.Add("src", String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray())));
            }
        }

        //public override void Process(TagHelperContext context, TagHelperOutput output)
        //{
        //    var QrcodeContent = context.AllAttributes["content"].Value.ToString();
        //    var alt = context.AllAttributes["alt"].Value.ToString();
        //    var width = 250; // width of the Qr Code    
        //    var height = 250; // height of the Qr Code    
        //    var margin = 0;
        //    var qrCodeWriter = new ZXing.BarcodeWriterPixelData
        //    {
        //        Format = ZXing.BarcodeFormat.QR_CODE,
        //        Options = new QrCodeEncodingOptions
        //        {
        //            Height = height,
        //            Width = width,
        //            Margin = margin
        //        }
        //    };
        //    var pixelData = qrCodeWriter.Write(QrcodeContent);
        //    // creating a bitmap from the raw pixel data; if only black and white colors are used it makes no difference    
        //    // that the pixel data ist BGRA oriented and the bitmap is initialized with RGB    
        //    using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
        //    using (var ms = new MemoryStream())
        //    {
        //        var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
        //        try
        //        {
        //            // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image    
        //            System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
        //        }
        //        finally
        //        {
        //            bitmap.UnlockBits(bitmapData);
        //        }
        //        // save to stream as PNG    
        //        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //        output.TagName = "img";
        //        output.Attributes.Clear();
        //        output.Attributes.Add("width", width);
        //        output.Attributes.Add("height", height);
        //        output.Attributes.Add("alt", alt);
        //        output.Attributes.Add("src", String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray())));
        //    }
        //}

    }
}
