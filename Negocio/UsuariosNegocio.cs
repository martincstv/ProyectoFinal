using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using Datos;

namespace Negocio
{
    public static class UsuariosNegocio
    {
        public static int Login(string usuario, string contraseña)
        {
            return UsuariosDatos.Login(usuario,contraseña);
        }

        public static UsuariosEntidades DevolverUsuario(int usuarioID)
        {
            return UsuariosDatos.DevolverUsuario(usuarioID);
        }

        public static List<MenusEntidades> ObtenerPermisos(int usuarioID)
        {
            return UsuariosDatos.ObtenerPermisos(usuarioID);
        }
    }
}
