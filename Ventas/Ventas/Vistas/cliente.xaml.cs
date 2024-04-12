using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Models;
using Ventas.Vistas.FormEdit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ventas.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class cliente : ContentPage
    {
        public cliente()
        {
            InitializeComponent();
            LlenarDatos();
            // Suscríbete al mensaje de cliente guardado
            MessagingCenter.Subscribe<NuevoClientePage>(this, "ClienteGuardado", async (sender) =>
            {
                await ActualizarListaClientes();
            });
            // Suscríbete al mensaje de cliente actualizado
            MessagingCenter.Subscribe<ActualizarClientePage>(this, "ClienteActualizado", async (sender) =>
            {
                await ActualizarListaClientes();
            });
        }
        private async Task ActualizarListaClientes()
        {
            var clientList = await App.SQLiteDB.GetClientesAsync();
            if (clientList != null)
            {
                lstClientes.ItemsSource = clientList;
            }
        }

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Clientes client = new Clientes
                {
                    NombreCliente = txtNombreClient.Text,
                    ApellidoCliente = txtApellidoClient.Text,
                };
                await App.SQLiteDB.SaveClientesAsync(client);
               
                await DisplayAlert("Registro", "Se Guardo Correctamente", "Ok");
                LimpiarControles();
                LlenarDatos();
            }
            else
            {
                await DisplayAlert("Advertencia", "Ingresar todos los Datos", "Ok");
            }
        }
        public async void LlenarDatos()
        {
            var clientList = await App.SQLiteDB.GetClientesAsync();
            if (clientList != null)
            {
                lstClientes.ItemsSource = clientList;
            }
        }

        public bool validarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtNombreClient.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellidoClient.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }
        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            var clienteSeleccionado = (Clientes)((Button)sender).BindingContext;
            await Navigation.PushAsync(new ActualizarClientePage(clienteSeleccionado));
           

            //if (!string.IsNullOrEmpty(txtIdCliente.Text))
            //{
            //    Clientes client = new Clientes()
            //    {
            //        IdCliente = Convert.ToInt32(txtIdCliente.Text),
            //        NombreCliente = txtNombreClient.Text,
            //        ApellidoCliente = txtApellidoClient.Text,
            //    };
            //    await App.SQLiteDB.SaveClientesAsync(client);
            //    await DisplayAlert("Actualizar", "Se Actualizo Correctamente", "Ok");

            //    LimpiarControles();
            //    txtIdCliente.IsVisible = false;
            //    BtnActualizar.IsVisible = false;
            //    btnRegistrar.IsVisible = true;
            //    LlenarDatos();
            //}
        }

        private async void lstClientes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Clientes)e.SelectedItem;
            btnRegistrar.IsEnabled = false;
            txtIdCliente.IsVisible = true;
            BtnActualizar.IsVisible = true;
            BtnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.IdCliente.ToString()))
            {
                var cliente = await App.SQLiteDB.GetClienteByIdAsync(obj.IdCliente);
                if (cliente != null)
                {
                    txtIdCliente.Text = cliente.IdCliente.ToString();
                    txtNombreClient.Text = cliente.NombreCliente;
                    txtApellidoClient.Text = cliente.ApellidoCliente;

                    
                }
            }
            //var obj = (Clientes)e.SelectedItem;
            //btnRegistrar.IsEnabled = false;
            //txtIdCliente.IsVisible = true;
            //BtnActualizar.IsVisible = true;
            //BtnEliminar.IsVisible = true;
            //if (!string.IsNullOrEmpty(obj.IdCliente.ToString()))
            //{
            //    var cliente = await App.SQLiteDB.GetClienteByIdAsync(obj.IdCliente);
            //    if (cliente != null)
            //    {
            //        txtIdCliente.Text = cliente.IdCliente.ToString();
            //        txtNombreClient.Text = cliente.NombreCliente;
            //        txtApellidoClient.Text = cliente.ApellidoCliente;
            //    }
            //}
        }

        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var cliente = button?.BindingContext as Clientes;
                if (cliente != null)
                {
                    bool respuesta = await DisplayAlert("Eliminar", $"¿Está seguro de eliminar al cliente {cliente.NombreCliente}?", "Sí", "No");
                    if (respuesta)
                    {
                        await App.SQLiteDB.DeleteClienteAsync(cliente);
                        await DisplayAlert("Eliminar", "Se Eliminó Correctamente", "Ok");
                        LlenarDatos(); // Vuelve a cargar la lista después de eliminar el cliente
                    }
                }
            }
            //var cliente = await App.SQLiteDB.GetClienteByIdAsync(Convert.ToInt32(txtIdCliente.Text));
            //if (cliente != null)
            //{
            //    await App.SQLiteDB.DeleteClienteAsync(cliente);
            //    await DisplayAlert("eliminar", "Se Elimino Correctamente", "Ok");
            //    LimpiarControles();
            //    LlenarDatos();
            //    txtIdCliente.IsVisible = false;
            //    BtnActualizar.IsVisible = false;
            //    BtnEliminar.IsVisible = false;
            //    btnRegistrar.IsEnabled = true;
            //}

        }

        public void LimpiarControles()
        {
            txtIdCliente.Text = "";
            txtNombreClient.Text = "";
            txtApellidoClient.Text = "";
        }

        private async void BtnNuevoCliente_Clicked(object sender, EventArgs e)
        {
            // Abrir el formulario flotante para agregar un nuevo cliente
            await Navigation.PushModalAsync(new NuevoClientePage());
        }
    }

}