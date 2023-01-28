using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

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
            //string cadenaConexion = "Data Source=DESKTOP-B4790FP\\SQLEXPRESS01;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //using (SqlConnection conn = new SqlConnection(cadenaConexion))
            //{
            //    SqlCommand comando = new SqlCommand("SELECT * FROM Usuario", conn);
            //    List<Usuario> usuarios = new List<Usuario>();
            //    conn.Open();
            //    using (SqlDataReader reader = comando.ExecuteReader())
            //    {
            //        if (reader.HasRows)
            //        {
            //            while (reader.Read())
            //            {
            //                usuarios.Add(new Usuario(Convert.ToInt64(reader["Id"]), reader["Nombre"].ToString(), reader.GetString(2), reader.GetString(5), reader.GetString(3), reader.GetString(4)));
            //            }
            //        }
            //    }
            //    foreach (var item in usuarios)
            //    {
            //        Console.WriteLine(item.Nombre);
            //    }
            //}


            //***************************************************************
            ////Conexion
            //string cadenaConexion = "Data Source=DESKTOP-B4790FP\\SQLEXPRESS01;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //SqlConnection conn = new SqlConnection(cadenaConexion);

            ////Command
            //SqlCommand command = new SqlCommand("SELECT * FROM Producto", conn);

            ////Lista para gestionar los Productos
            //List<Producto> productos= new List<Producto>();

            ////Gestionar el resultado
            //conn.Open();
            //SqlDataReader reader = command.ExecuteReader();
            //if (reader.HasRows) //Asegurarme de que la consulta devolvio por lo menos una fila
            //{
            //    while(reader.Read())
            //    {
            //        productos.Add(new Producto(Convert.ToInt64(reader["Id"]), reader["Descripciones"].ToString(), 
            //            reader.GetDecimal(2), Convert.ToDecimal(reader["PrecioVenta"]),Convert.ToInt32( reader["Stock"]),
            //            Convert.ToInt64(reader["IdUsuario"])));
            //    }
            //}

            //foreach (var item in productos)
            //{
            //    Console.WriteLine(item.Descripciones);
            //}

            //El front-end hace un peticion solicitando los nombres de los productos y el nombre de quien los cargo

            //Conexion
            //string cadenaConexion = "Data Source=DESKTOP-B4790FP\\SQLEXPRESS01;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //SqlConnection conn = new SqlConnection(cadenaConexion);

            ////Command
            //SqlCommand command = new SqlCommand("select Producto.Descripciones, Usuario.Nombre from Producto\r\ninner join Usuario\r\nON Producto.IdUsuario = Usuario.Id", conn);

            //conn.Open();
            //SqlDataReader reader = command.ExecuteReader();
            //if (reader.HasRows) //Asegurarme de que la consulta devolvio por lo menos una fila
            //{
            //    while (reader.Read())
            //    {
            //        Console.WriteLine("Nombre Producto: {0} \t\t Nombre usuario que cargó: {1}",reader.GetString(0), reader.GetString(1));
            //    }
            //}

            //Dado el nombre de usuario, obtener todos los productos que cargo tal usuario
            //foreach (var item in ObtenerProductos("tcasazza"))
            //{
            //    Console.WriteLine(item.Descripciones);
            //}




            //Login
            string cadenaConexion = "Data Source=DESKTOP-B4790FP\\SQLEXPRESS01;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                Usuario usuarioLogueado = new Usuario();
                usuarioLogueado = Login("tobiascasazza@gmail.com", "SoyTobiasCasazza", conn);
                if (usuarioLogueado == null)
                {
                    Console.WriteLine("Passw o usuario incorrectos");
                }
                else
                {
                    Console.WriteLine("Bienvenido " + usuarioLogueado.NombreUsuario);

                }
            }


        }

        public static Usuario Login(string mail, string passw, SqlConnection conn)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Usuario WHERE Mail = @mail AND Contraseña = @passw", conn);

            //Se utiliza SQL Parameter para reemplazar los @ de la consulta
            SqlParameter parameterMail = new SqlParameter();
            parameterMail.ParameterName = "mail";
            parameterMail.SqlValue = SqlDbType.VarChar;
            parameterMail.Value = mail;

            SqlParameter parameterContrasena = new SqlParameter();
            parameterContrasena.ParameterName = "passw";
            parameterContrasena.SqlValue = SqlDbType.VarChar;
            parameterContrasena.Value = passw;

            //Se aplica los parámetros al comando
            command.Parameters.Add(parameterMail);
            command.Parameters.Add(parameterContrasena);
            conn.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    Usuario usuarioEncontrado = new Usuario();
                    reader.Read();
                    usuarioEncontrado.Nombre = reader.GetString(1);
                    usuarioEncontrado.Apellido = reader.GetString(2);
                    usuarioEncontrado.NombreUsuario = reader.GetString(3);
                    usuarioEncontrado.Mail = reader.GetString(5);
                    return usuarioEncontrado;
                }
            }
            //En caso de que la consulta este vacía retornara un Usuario vacio
            return null;
        }

        public static List<Producto> ObtenerProductos(string nombreUsuario)
        {
            string cadenaConexion = "Data Source=DESKTOP-B4790FP\\SQLEXPRESS01;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand command = new SqlCommand("select Producto.* from Producto inner " +
                    "join Usuario ON Producto.IdUsuario = Usuario.Id WHERE Usuario.NombreUsuario = '" + nombreUsuario + "'", conn);
                List<Producto> productos = new List<Producto>();
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) //Se asegura de que la consulta devolvio por lo menos una fila
                    {
                        while (reader.Read())
                        {
                            productos.Add(new Producto(Convert.ToInt64(reader["Id"]), reader["Descripciones"].ToString(),
                                reader.GetDecimal(2), Convert.ToDecimal(reader["PrecioVenta"]), Convert.ToInt32(reader["Stock"]),
                                Convert.ToInt64(reader["IdUsuario"])));
                        }
                    }
                }
                return productos;
            }
        }


    }
}