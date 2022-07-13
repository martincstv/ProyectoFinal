using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;

namespace Datos
{
    public static class UsuariosDatos
    {
        public static int Login(string usuario, string contraseña)
        {
            try
            {
                var respuesta = 0;

                SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionBD);
                conexion.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.Parameters.Add("UsuarioID", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = @"sp_LoginUsuario";

                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@Contraseña", contraseña);

                cmd.ExecuteScalar();

                conexion.Close();
                respuesta = Convert.ToInt32(cmd.Parameters["UsuarioID"].Value);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = e.Message;
                throw;
            }
        }

        public static UsuariosEntidades DevolverUsuario(int usuarioID)
        {
            try
            {
                UsuariosEntidades usuarioE = new UsuariosEntidades();

                SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionBD);
                conexion.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = @"sp_DevolverUsuario";

                cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);

                using (var dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    if (dr.HasRows)
                    {
                        usuarioE.UsuarioID = Convert.ToInt32(dr["UsuarioID"]);
                        usuarioE.RolID = Convert.ToInt32(dr["RolID"]);
                        usuarioE.BodegaID = Convert.ToInt32(dr["BodegaID"]);
                        usuarioE.Usuario = dr["Usuario"].ToString();
                        usuarioE.Contraseña = dr["Contraseña"].ToString();
                        usuarioE.Cedula = dr["Cedula"].ToString();
                        usuarioE.Nombre = dr["Nombre"].ToString();
                        usuarioE.Apellido = dr["Apellido"].ToString();
                        usuarioE.Correo = dr["Correo"].ToString();
                        usuarioE.EstadoActivo = Convert.ToBoolean(dr["EstadoActivo"]);
                        usuarioE.FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);
                    }
                }
                conexion.Close();
                return usuarioE;
            }
            catch (Exception e)
            {
                var error = e.Message;
                throw;
            }
        }

        public static List<MenusEntidades> ObtenerPermisos(int usuarioID)
        {
            try
            {
                List<MenusEntidades> listaPermisos = new List<MenusEntidades>();

                SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionBD);
                conexion.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = @"sp_ObtenerPermisos";

                cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);
//              < PERMISOS >
//  < DetalleMenu >
//    < Menu >
//      < NombreMenu > Ventas </ NombreMenu >
//      < Icono >\Iconos\Venta.png </ Icono >
//      < DetalleSubMenu >
//        < SubMenu >
//          < NombreSubMenu > Crear Venta </ NombreSubMenu >
//          < NombreFormulario > frmCrearVenta </ NombreFormulario >
//        </ SubMenu >
//        < SubMenu >
//          < NombreSubMenu > Editar Venta </ NombreSubMenu >
//          < NombreFormulario > frmEditarVenta </ NombreFormulario >
//        </ SubMenu >
//      </ DetalleSubMenu >
//    </ Menu >
//  </ DetalleMenu >
//</ PERMISOS >
                XmlReader xr = cmd.ExecuteXmlReader();
                while (xr.Read())
                {
                    XDocument doc = XDocument.Load(xr);

                    if (doc.Element("PERMISOS") != null)
                    {
                        listaPermisos = doc.Element("PERMISOS").Element("DetalleMenu") == null ? new List<MenusEntidades>() :
                            (from MenusEntidades in doc.Element("PERMISOS").Element("DetalleMenu").Elements("Menu")
                             select new MenusEntidades()
                             {
                                 NombreMenu = MenusEntidades.Element("NombreMenu").Value,
                                 Icono = MenusEntidades.Element("Icono").Value,
                                 ListaSubMenu = MenusEntidades.Element("DetalleSubMenu") == null ? new List<SubMenusEntidades>() :
                                 (from SubMenusEntidades in MenusEntidades.Element("DetalleSubMenu").Elements("SubMenu")
                                  select new SubMenusEntidades()
                                  {
                                      NombreSubMenu = SubMenusEntidades.Element("NombreSubMenu").Value,
                                      NombreFormulario = SubMenusEntidades.Element("NombreFormulario").Value
                                  }).ToList()
                             }).ToList();
                    }
                }
                return listaPermisos;
                conexion.Close();
            }
            catch (Exception e)
            {
                var error = e.Message;
                throw;
            }
        }

    }
}
