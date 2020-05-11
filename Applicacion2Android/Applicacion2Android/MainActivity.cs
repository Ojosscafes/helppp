using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Content;

namespace Applicacion2Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        string Usuario, Password;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            var btnIngeresa = FindViewById<Button>(Resource.Id.btningresar);
            var txtUser = FindViewById<EditText>(Resource.Id.txtusuario);
            var txtPass = FindViewById<EditText>(Resource.Id.txtpassword);
            var Imagen= FindViewById<ImageView>(Resource.Id.imagen);
            Imagen.SetImageResource(Resource.Drawable.logo);
            btnIngeresa.Click += delegate
              {
                  var svc = new WebReference.Servicio();
                  var valido = svc.Login(txtUser.Text, txtUser.Text);
                  if(valido)
                  {
                      Intent i = new Intent(this,typeof(Principal));
                      StartActivity(i);
          
                  }
                  else{
                      Toast.MakeText(this, "Usuario incorrecto", ToastLength.Long);
                  }
                  //try
                  //{
                  //    Usuario = txtUser.Text;
                  //    Password = txtPass.Text;
                  //    if ((Usuario == "Leomaris") && (Password == "123"))
                  //    {
                  //        Cargar();
                  //    }
                  //    else
                  //    {
                  //        if ((Usuario == "Fernanda") && (Password == "456"))
                  //        {
                  //            Cargar();
                  //        }
                  //        else
                  //        {
                  //            if ((Usuario == "Nay") && (Password == "789"))
                  //            {
                  //                Cargar();
                  //            }
                  //            else
                  //            {
                  //                Toast.MakeText(this, "Error de usuario o Password", ToastLength.Long).Show();
                  //            }
                  //        }
                  //    }
                  //}
                  //catch (Exception ex)
                  //{
                  //    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();

                  //}

              };
    }
        //public void Cargar()
        //{
        //    Intent Instancia = new Intent(this, typeof(Principal));
        //    Instancia.PutExtra("Usuario", Usuario);
        //    StartActivity(Instancia);
        //}


    }
}