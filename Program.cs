using System.Data;
using System.Data.SqlClient;

namespace Clase12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string connectionString = "Data Source=DESKTOP-B4790FP\\SQLEXPRESS01;Initial Catalog=VETERINARIA;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //using (SqlConnection connection = new SqlConnection(connectionString)) { 

            //    //Vamos a hacer un SELECT de todas las mascotas
            //    //SqlCommand comando = new SqlCommand("SELECT * FROM Mascotas", connection);
            //    List<Mascota> mascotas = new List<Mascota>();
            //    connection.Open();
            //SqlDataReader reader = comando.ExecuteReader();

            //if (reader.HasRows)
            //{
            //    while (reader.Read())
            //    {
            //        mascotas.Add(new Mascota(reader.GetString(0), reader.GetInt32(1)));
            //    }
            //}

            //foreach (var item in mascotas)
            //{
            //    Console.WriteLine(item);
            //}


            //SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT edad, nombre FROM Mascotas", connection);
            //DataSet datos = new DataSet("Mascotas");
            //dataAdapter.Fill(datos);
            //foreach (DataRow item in datos.Tables[0].Rows)
            //{
            //    mascotas.AddMascota(new Mascota(Convert.ToInt32(item.ItemArray[0]), item.ItemArray[1].ToString()));
            //}
            ////mascotas.ImprimirConsoleMascotas();
            //}



            //***********************************************
            string cadenaConexion = "Data Source=DESKTOP-B4790FP\\SQLEXPRESS01;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuario", conn);
                List<Usuario> usuarios = new List<Usuario>();
                conn.Open();
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new Usuario(Convert.ToInt64(reader["Id"]), reader["Nombre"].ToString(), reader.GetString(2), reader.GetString(5), reader.GetString(3), reader.GetString(4)));
                        }
                    }
                }
                foreach (var item in usuarios)
                {
                    Console.WriteLine(item.Nombre);
                }
            }




        }
    }
}