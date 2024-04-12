using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Vistas;
using Xamarin.Forms;

namespace Ventas
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.Master = new Nav();
            this.Detail = new NavigationPage(new Inicio());
            App.MasterDet = this;

        }
    }
}
