using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ventas.Vistas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActualizarClientePage : ContentPage
	{
        private Clientes clienteSeleccionado; // Cambiando el nombre de la variable
        public ActualizarClientePage(Clientes cliente)
        {
            InitializeComponent();
            clienteSeleccionado = cliente; // Asignando al nuevo nombre de variable
            CargarDatosCliente();
        }
        private void CargarDatosCliente()
        {
            if (clienteSeleccionado != null)
            {
                txtNombre.Text = clienteSeleccionado.NombreCliente;
                txtApellido.Text = clienteSeleccionado.ApellidoCliente;
            }
        }
        private async void BtnGuardarCambios_Clicked(object sender, EventArgs e)
        {
            if (clienteSeleccionado != null)
            {
                // Actualiza los datos del cliente
                clienteSeleccionado.NombreCliente = txtNombre.Text;
                clienteSeleccionado.ApellidoCliente = txtApellido.Text;
                await App.SQLiteDB.SaveClientesAsync(clienteSeleccionado);

                await DisplayAlert("Actualizar", "Se Actualizó Correctamente", "Ok");
                MessagingCenter.Send(this, "ClienteActualizado"); // Envía una señal de cliente actualizado
                await Navigation.PopAsync(); // Regresa a la página principal
            }
        }
    }
}