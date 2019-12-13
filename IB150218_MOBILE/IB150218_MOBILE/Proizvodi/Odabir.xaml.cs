using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IB150218_MOBILE.Proizvodi
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Odabir : ContentPage
	{
		public Odabir ()
		{
			InitializeComponent ();
		}

        private void BtnProizvodi_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new Navigation.MasterDetailPage1(new Proizvodi. Pregledproizvoda()));
        }

        private void BtnMeni_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new Navigation.MasterDetailPage1(new Proizvodi.Pregledmenia()));
        }
    }
}