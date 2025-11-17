using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AppQR.Services.Servicios;
using AppQR.Core.Servicios.IServicios;

namespace AppQR.Test
{
    public class TestAdoQR
    {
        [Fact]
        public void AlGenerar_LaUrlDeUnQr_DebeDevolverUna_UrlCorrespondiente_AlToken()
        {
            var mock = new Mock<IQrService>();

            string token = "ABC123TOKEN";
            string urlEsperada = "https://miapp.com/qr/ABC123TOKEN";

            mock.Setup(s => s.GenerarUrldeQR(token))
                .Returns(urlEsperada);

            var resultado = mock.Object.GenerarUrldeQR(token);

            Assert.Equal(urlEsperada, resultado);
        }
        [Fact]
        public void AlCrear_UnQr_DebeDevolver_UnArrayDeBytesCorrespondienteAlUrl()
        {
            var MOQ = new Mock<IQrService>();
            var url= "https://miapp.com/qr/3";
            var qrBytes = new byte[] { 1, 2, 3, 4, 5};

            MOQ.Setup(s => s.CrearQR(url)).Returns(qrBytes);

            var resultado = MOQ.Object.CrearQR(url);

            Assert.Equal(qrBytes, resultado);
        }

    }
}