using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace university_25_26.Recursos
{
    public class ConexionMySql
    {        private MySqlConnection conexion;

        public ConexionMySql()
        {
            string cadena = ConfigurationManager.ConnectionStrings["ConexionMySql"].ConnectionString;
            conexion = new MySqlConnection(cadena);
        }

        public MySqlConnection AbrirConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
                conexion.Open();
            return conexion;
        }

        public void CerrarConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
                conexion.Close();
        }
    }
}