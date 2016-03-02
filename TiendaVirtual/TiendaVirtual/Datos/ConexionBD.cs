using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Uso para manejo de Bases de Datos
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace Datos
{
    public class ConexionBD
    {
        #region variables
        /// <summary>
        /// Cadena de conexión de base de datos
        /// </summary>
        private string cadenaConexion;

        /// <summary>
        /// Conexion de base de datos
        /// </summary>
        private SqlConnection cnx;
        #endregion

        #region propiedades
        /// <summary>
        /// Cadena de conexión de base de datos
        /// </summary>
        public string CadenaConexion
        {
            get { return cadenaConexion; }
        }
        #endregion

        /// <summary>
        /// Constructor de la conexión
        /// </summary>
        public ConexionBD()
        {
            ObtenerConexion();
        }

        /// <summary>
        /// Obtiene el connection string de la sección de configuración
        /// </summary>
        /// <returns></returns>
        public string ObtenerConexion()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return cadenaConexion;
        }

        public void Ejecutar(SqlCommand comando)
        {
            try
            {
                using (cnx = new SqlConnection(CadenaConexion))
                {
                    cnx.Open();
                    comando.Connection = cnx;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                    cnx.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cnx != null)
                    cnx.Dispose();
            }
        }

        public DataSet Consultar(SqlCommand comando, string nombreTabla)
        {
            SqlDataAdapter adaptador;
            DataSet resultado = new DataSet();

            try
            {
                using (cnx = new SqlConnection(cadenaConexion))
                {
                    cnx.Open();
                    comando.Connection = cnx;
                    comando.CommandType = CommandType.StoredProcedure;

                    adaptador = new SqlDataAdapter(comando);
                    adaptador.Fill(resultado, nombreTabla);
                    cnx.Close();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cnx != null)
                    cnx.Dispose();
            }
        }
    }

} //Fin de Clase Conexion DB

