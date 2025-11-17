using AppQR.Core.Servicios.IServicios;
using QRCoder;
using SkiaSharp;

namespace AppQR.Services.Servicios
{
    public class QrService : IQrService
    {
        readonly IUrlConstructService _urlConstruct;

        public QrService(IUrlConstructService UrlConstruct)
        {
            _urlConstruct = UrlConstruct;
        }

        public string GenerarUrldeQR(string token)
        {
            return _urlConstruct.GenerarUrldeQR(token);
        }

        public byte[] CrearQR(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("La URL no puede estar vacía.", nameof(url));

            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qRCode = new BitmapByteQRCode(qRCodeData);
            byte[] qrCodeBytes = qRCode.GetGraphic(20);

            // Top 1 copia y pega (Pasa el QR a PNG utilizando SkiaSharp)
            using var bitmap = SKBitmap.Decode(qrCodeBytes);
            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);

            return data.ToArray();
        }
    }
}