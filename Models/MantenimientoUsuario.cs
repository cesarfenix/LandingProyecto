using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TrabajoFinal.Models
{
    public class MantenimientoUsuario
    {
        private SqlConnection conexion;
        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["conectBd"].ToString();
            conexion = new SqlConnection(constr);
        }
        public int CrearUsuario(Usuario usu)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("insert into Usuarios(Nombre, Edad, Email,Telefono, Ciudad, Interes) values (@Nombre, @Edad, @Email, @Telefono, @Ciudad, @Interes)", conexion);
            comando.Parameters.Add("@Nombre", SqlDbType.VarChar);
            comando.Parameters.Add("@Edad", SqlDbType.Int);
            comando.Parameters.Add("@Email", SqlDbType.VarChar);
            comando.Parameters.Add("@Telefono", SqlDbType.Int);
            comando.Parameters.Add("@Ciudad", SqlDbType.VarChar);
            comando.Parameters.Add("@Interes", SqlDbType.Bit);

            comando.Parameters["@Nombre"].Value = usu.Nombre;
            comando.Parameters["@Edad"].Value = usu.Edad;
            comando.Parameters["@Email"].Value = usu.Email;
            comando.Parameters["@Telefono"].Value = usu.Telefono;
            comando.Parameters["@Ciudad"].Value = usu.Ciudad;
            comando.Parameters["@Interes"].Value = usu.Interes;
            conexion.Open();
            int i = comando.ExecuteNonQuery();
            conexion.Close();
            return i;
        }
        public List<Usuario> ListarTodos()
        {
            Conectar();
            List<Usuario> usuarios = new List<Usuario>();
            SqlCommand com = new SqlCommand("select * from Usuarios", conexion);
            conexion.Open();
            SqlDataReader registros = com.ExecuteReader();
            while (registros.Read())
            {
                Usuario usu = new Usuario
                {
                    Nombre = registros["Nombre"].ToString(),
                    Email = registros["Email"].ToString(),
                    Telefono = int.Parse(registros["Telefono"].ToString()),
                    Edad = int.Parse(registros["Edad"].ToString()),
                    Ciudad = registros["Ciudad"].ToString(),
                    Interes = Boolean.Parse(registros["Interes"].ToString())
                };
                usuarios.Add(usu);
            }
            conexion.Close();
            return usuarios;
        }
    }
}