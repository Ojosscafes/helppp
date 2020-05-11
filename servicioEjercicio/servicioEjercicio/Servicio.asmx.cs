using System.Data;
using System.Web.Services;
using System.Data.SqlClient;
using System;
using System.Security.Cryptography;
using System.Text;

namespace servicioEjercicio
{
    /// <summary>
    /// Summary description for Servicio
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Servicio : System.Web.Services.WebService
    {

        [WebMethod]
        public DataSet Consultar(string usuario)
        {
            var Conexion = new SqlConnection
                ("Data Source=DESKTOP-UR3MV4N\\SERVERKMVC; Initial Catalog=InformacionITSN; User ID=sa; Password= 170997kmvc");
            var Adaptador = new SqlDataAdapter
                ("SELECT * FROM Datos WHERE Usuario ='" + usuario + "'", Conexion);
            DataSet Conjunto = new DataSet();
            try
            {
                Conexion.Open();
                Adaptador.Fill(Conjunto, "Datos");
                Conexion.Close();
                return Conjunto;
            }
            catch (System.Exception ex)
            {
                Conexion.Close();
                return Conjunto;
            }

        }
        [WebMethod]
        public bool Insertar(string Usuario, string Pass, string Correo)
        {

            var passCifrada = CifradoSHA(Pass);
            var Conexion = new SqlConnection
                ("Data Source=DESKTOP-UR3MV4N\\SERVERKMVC; Initial Catalog=InformacionITSN; User ID=sa; Password= 170997kmvc");
            var Insercion = new SqlCommand
                ("INSERT INTO Datos (Usuario, Pass, Correo) VALUES " +
                    "('" + Usuario + "','" + passCifrada + "','" + Correo + "')",
                    Conexion);

            try
            {
                Conexion.Open();
                Insercion.ExecuteNonQuery();
                Conexion.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                Conexion.Close();
                throw ex;
                //return false;
            }

        }

        [WebMethod]
        public bool Login(string usuario, string pass)
        {
            pass = CifradoSHA(pass);
            var Conexion = new SqlConnection
                ("Data Source=DESKTOP-UR3MV4N\\SERVERKMVC; Initial Catalog=InformacionITSN; User ID=sa; Password= 170997kmvc");
            var Adaptador = new SqlDataAdapter
                ("SELECT * FROM Datos WHERE Usuario ='" + usuario + "' AND Pass = '"+pass+"'", Conexion);
            DataSet Conjunto = new DataSet();
            try
            {
                Conexion.Open();
                Adaptador.Fill(Conjunto, "Datos");
                Conexion.Close();
                return Conjunto.Tables["Datos"].Rows.Count > 0;
            }
            catch (System.Exception ex)
            {
                Conexion.Close();
                return false;
            }

        }
        public string CifradoSHA(string Texto)
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] arreglo = hash.ComputeHash(Encoding.UTF8.GetBytes(Texto));
                StringBuilder cadenasdetexto = new StringBuilder();
                for (int i = 0; i < arreglo.Length; i++)
                {
                    cadenasdetexto.Append(arreglo[i].ToString("X2"));

                }
                return cadenasdetexto.ToString();
            }
        }
    }
}
   
