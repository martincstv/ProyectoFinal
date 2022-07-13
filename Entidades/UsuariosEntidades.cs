using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class UsuariosEntidades
    {
        public int UsuarioID { get; set; }
        public int RolID { get; set; }
        public int BodegaID { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public bool EstadoActivo { get; set; }
        public DateTime FechaRegistro { get; set; }

        public UsuariosEntidades()
        {

        }

        public UsuariosEntidades(int usuarioID, int rolID, int bodegaID, string usuario, string contraseña, string cedula, string nombre, string apellido, string correo, bool estadoActivo, DateTime fechaRegistro)
        {
            UsuarioID = usuarioID;
            RolID = rolID;
            BodegaID = bodegaID;
            Usuario = usuario;
            Contraseña = contraseña;
            Cedula = cedula;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            EstadoActivo = estadoActivo;
            FechaRegistro = fechaRegistro;
        }
    }
}
