using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ventas.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Nav : ContentPage
    {
        public Nav()
        {
            InitializeComponent();
        }

        private async void btnClientes_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new cliente());

        }
    }
}