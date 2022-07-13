using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public static class RolesDatos
    {
        public static RolesEntidades DevolverRol(int rolID)
        {
            try
            {
                RolesEntidades rolE = new RolesEntidades();

                SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionBD);
                conexion.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = @"sp_DevolverRol";

                cmd.Parameters.AddWithValue("@RolID", rolID);

                using (var dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    if (dr.HasRows)
                    {
                        rolE.RolID = Convert.ToInt32(dr["RolID"]);
                        rolE.NombreRol = dr["NombreRol"].ToString();
                    }
                }
                conexion.Close();
                return rolE;
            }
            catch (Exception e)
            {
                var error = e.Message;
                throw;
            }
        }
    }
}
