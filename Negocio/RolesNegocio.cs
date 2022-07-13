using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using Datos;

namespace Negocio
{
    public static class RolesNegocio
    {
        public static RolesEntidades DevolverRol(int rolID)
        {
            return RolesDatos.DevolverRol(rolID);
        }
    }
}
