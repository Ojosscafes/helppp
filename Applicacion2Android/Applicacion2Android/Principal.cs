using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using System.Threading.Tasks;
using System.Net;
using System.Data;
using Android.Widget;

namespace Applicacion2Android
{
    [Activity(Label = "Principal")]
    public class Principal : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityPrincipal);
            var lblDestino = FindViewById<TextView>(Resource.Id.lblusuario);
            var Imagen = FindViewById<ImageView>(Resource.Id.imagen);
            var txtFolio = FindViewById<EditText>(Resource.Id.txtfolio);
            var txtUsuario = FindViewById<EditText>(Resource.Id.txtusuario);
            var txtCorreo = FindViewById<EditText>(Resource.Id.txtcorreo);
            var txtPass = FindViewById<EditText>(Resource.Id.txtpass);
            var btnGuardar = FindViewById<Button>(Resource.Id.btnguardar);
            var btnLoginn = FindViewById<Button>(Resource.Id.btnlogear);
            var btnBuscar = FindViewById<Button>(Resource.Id.btnbuscar);
            string Usuario;
            Usuario = Intent.GetStringExtra("Usuario");
            lblDestino.Text = Usuario;
            
            //if (Usuario == "Leomaris") 
            //{
            //    ArchivoImagen("https://pbs.twimg.com/profile_images/1216547181496324102/lvpJjQAR_400x400.jpg");
            //}
            //if (Usuario == "Fernanda")
            //{
            //    ArchivoImagen("https://pbs.twimg.com/profile_images/839476805627179008/7POypSI-_400x400.jpg");
            //}
            //if (Usuario == "Nay")
            //{
            //    ArchivoImagen("https://pbs.twimg.com/profile_images/1115077272447225856/W5t8Enb-_400x400.jpg");
            //}

            async void ArchivoImagen (string url)
            {
                var ruta = await DescargaImagen(url);
                Android.Net.Uri rutaImagen = Android.Net.Uri.Parse(ruta);
                Imagen.SetImageURI(rutaImagen);

            }
            btnGuardar.Click += delegate
             {
                 string Usuario,Correo,Pass;
                 //int Edad;
                 //double Saldo;
                 try
                 {
                     Usuario = txtUsuario.Text;
                     Pass = txtPass.Text;
                     Correo = txtCorreo.Text; 
                     //Saldo = double.Parse(txtSaldo.Text);
                     var WS = new WebReference.Servicio();
                     if (WS.Insertar(Usuario, Pass, Correo))
                     {
                         Toast.MakeText(this, "Guardado en SQLServer", ToastLength.Short).Show();


                     }
                     else
                     {
                         Toast.MakeText(this, "No guardado", ToastLength.Short).Show();
                     }
                 }
                 catch (System.Exception ex )
                 {
                     Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                 }
             
             };
            btnBuscar.Click += delegate
              {
                  var Repositorio = new DataSet();
                  DataRow Renglon;
                  int Folio;
                  try
                  {
                      Folio = int.Parse(txtFolio.Text);
                      var WS = new WebReference.Servicio();
                      Repositorio = WS.Consultar(Usuario);
                      Renglon = Repositorio.Tables["Datos"].Rows[0];
                      txtUsuario.Text = Renglon["Usuario"].ToString();
                      txtPass.Text = Renglon["Pass"].ToString();
                      txtCorreo.Text = Renglon["Correo"].ToString();
                      
                  }
                  catch (System.Exception ex)
                  {
                      Toast.MakeText(this, ex.Message, ToastLength.Short).Show();

                  }
              };

        }
        public async Task<string> DescargaImagen (string url)
        {
            WebClient cliente = new WebClient();
            byte[] datosdeimagen = await cliente.DownloadDataTaskAsync(url);
            string ruta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string nombrearchivo = "foto.jpg";
            string rutalocal = Path.Combine(ruta, nombrearchivo);
            File.WriteAllBytes(rutalocal, datosdeimagen);
            return rutalocal;
        }
        public void Cargar()
        {
            Intent Instancia = new Intent(this, typeof(MainActivity));
            //Instancia.PutExtra("Usuario", Usuario);
            StartActivity(Instancia);
        }
    }
}