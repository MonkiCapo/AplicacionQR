using System;
using Microsoft.Extensions.Configuration;
using AppQR.Core.Servicios.IServicios;

namespace AppQR.Services.Servicios
{
    public class DataBaseConnectionService : IDataBaseConnectionService
    {
        public string GetConnectionRootString()
        {
            var configuration = LeerJson();

            var connectionStrings = configuration.GetSection
        }
    }
}