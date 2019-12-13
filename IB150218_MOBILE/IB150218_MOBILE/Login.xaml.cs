using IB150218_PCL.Model;
using IB150218_PCL.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IB150218_MOBILE
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        private WebAPIHelper klijentiService = new WebAPIHelper("http://192.168.1.8/", "api/Kupci");
        public Login ()
		{
			InitializeComponent ();
		}

        private void BtnLogin_Clicked(object sender, EventArgs e)
        {
            HttpResponseMessage response = klijentiService.GetActionResponse("KlijentByKorisnickoIme", inputUsername.Text);
            if (response.IsSuccessStatusCode)

            {
                var jsonResult = response.Content.ReadAsStringAsync();
                Kupci k = new Kupci();
                k= JsonConvert.DeserializeObject<Kupci>(jsonResult.Result);
                Global.LogiraniKupac = k;

                if (k.LozinkaHash == UIHelper.GenerateHash(inputPassword.Text, k.LozinkaSalt))
                {
                    this.Navigation.PushModalAsync(new Navigation.MasterDetailPage1(new Proizvodi.Odabir()));

                }
                else
                {
                    DisplayAlert("Upozorenje", "Lozinka nije ispravna!", "OK");
                }

            }
            else
            {
                DisplayAlert("Greška", "Niste unijeli ispravne podatke za prijavu!", "OK");
            }


        }

        private void BtnRegistracija_Clicked(object sender, EventArgs e)
        {
            new Navigation.MasterDetailPage1(new Registracija());
        }
    }
}