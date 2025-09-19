using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core
{
    public interface IAdo
    {
        IDbConnection GetDbConnection();
    }
}