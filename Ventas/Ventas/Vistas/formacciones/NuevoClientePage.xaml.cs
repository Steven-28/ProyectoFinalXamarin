using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ventas.Vistas.FormEdit
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NuevoClientePage : ContentPage
	{
		public NuevoClientePage ()
		{
			InitializeComponent ();
		}

        private async void BtnGuardarCliente_Clicked(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Clientes client = new Clientes
                {
                    NombreCliente = txtNombre.Text,
                    ApellidoCliente = txtApellido.Text,
                };
                await App.SQLiteDB.SaveClientesAsync(client);

                await DisplayAlert("Registro", "Se Guardo Correctamente", "Ok");
                MessagingCenter.Send(this, "ClienteGuardado"); // Envía una señal de cliente guardado
                await Navigation.PopModalAsync(); // Cierra la página de NuevoCliente
            }
            else
            {
                await DisplayAlert("Advertencia", "Ingresar todos los Datos", "Ok");
            }
            //if (!string.IsNullOrWhiteSpace(txtNombre.Text) && !string.IsNullOrWhiteSpace(txtApellido.Text))
            //{
            //    // Crear un nuevo cliente con los datos ingresados
            //    Clientes nuevoCliente = new Clientes
            //    {
            //        NombreCliente = txtNombre.Text,
            //        ApellidoCliente = txtApellido.Text
            //    };

            //    // Guardar el nuevo cliente en la base de datos
            //    await App.SQLiteDB.SaveClientesAsync(nuevoCliente);

            //    // Mostrar un mensaje de éxito
            //    await DisplayAlert("Éxito", "El nuevo cliente se ha guardado correctamente", "Aceptar");

            //    // Regresar a la página principal
            //    await Navigation.PopModalAsync();
            //}
            //else
            //{
            //    // Mostrar un mensaje de advertencia si no se completaron todos los campos
            //    await DisplayAlert("Advertencia", "Por favor, complete todos los campos", "Aceptar");
            //}
        }
        public bool validarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellido.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

    }
}