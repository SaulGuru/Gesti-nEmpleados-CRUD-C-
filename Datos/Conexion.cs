using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pjGestionEmpleados.Datos
{
    public class Conexion
    {
        private string Base;
        private string Servidor;
        private string Usuario;
        private string Clave;
        private static Conexion Con = null;

        //Constrictor de la clase
        private Conexion()
        {
            this.Servidor = "SaulGuru\\SQLEXPRESS";
            this.Base = "bd_gestion_empleados";
            this.Usuario = "Saul_Guru";
            this.Clave = "Mgespoec7";
        }

        public SqlConnection CrearConexion()
        {
            SqlConnection Cadena = new SqlConnection();
            try
            {
                Cadena.ConnectionString = "Server=" + this.Servidor +
                                           "; Database=" + this.Base +
                                           "; User Id=" + this.Usuario +
                                           "; Password=" + this.Clave;
            }
            catch (Exception ex)
            {
                Cadena = null;
                throw ex;
            }

            return Cadena;

        }


        public static Conexion crearInstancia()
        {
            if (Con == null)
            {
                Con = new Conexion();
            }
            return Con;

        }
    }

}
