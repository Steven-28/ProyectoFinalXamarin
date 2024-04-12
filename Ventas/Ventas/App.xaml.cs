using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ventas.Data;
using System.IO;

namespace Ventas
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDet { get; set; }

        public static SQLiteHelper db;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
        public static SQLiteHelper SQLiteDB
        {
            get
            {
                if(db == null)
                {
                    db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Ventas.db3"));
                }
                return db;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
