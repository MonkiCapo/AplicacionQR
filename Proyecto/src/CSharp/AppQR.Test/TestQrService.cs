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
    public class TestQrService
    {
        [Fact]
        public void AlGenerar_LaUrlDeUnQr_DebeDevolverUna_UrlCorrespondiente_AlIdDeEntrada()
        {
            var MOQ = new Mock<IQrService>();
            var idEntrada = 3;
            var urlEsperada = "https://miapp.com/qr/3";

            MOQ.Setup(s => s.GenerarUrldeQR(idEntrada)).Returns(urlEsperada);

            var resultado = MOQ.Object.GenerarUrldeQR(idEntrada);

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