using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace AppQR.Core
{
    public interface IAdo
    {
        IDbConnection GetDbConnection();
    }
}