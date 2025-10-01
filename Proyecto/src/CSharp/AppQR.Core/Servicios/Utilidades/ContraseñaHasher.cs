using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Isopoh.Cryptography.Argon2;

namespace AppQR.Core.Servicios.Utilidades
{
    public class ContraseñaHasher
    {
        public static string Hash(string contraseña)
        {
            return Argon2.Hash(contraseña);
        }

        public static bool Verificar(string hash, string contraseña)
        {
            return Argon2.Verify(hash, contraseña);
        }
    }
}