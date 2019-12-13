using IB150218_PCL.Model;
using IB150218_PCL.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IB150218_MOBILE
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registracija : ContentPage
	{
        private WebAPIHelper klijentiService = new WebAPIHelper("http://192.168.1.8/", "api/Kupci");
        public Registracija ()
		{
			InitializeComponent ();
		}

        private void BtnRegistracija_Clicked(object sender, EventArgs e)
        {
            Kupci k = new Kupci();
            k.Ime = inputIme.Text;
            k.Prezime = inputPrezime.Text;
            k.KorisnickoIme = inputUsername.Text;
            k.Email = inputMail.Text;
            k.DatumRegistracije = DateTime.Now;
            k.LozinkaSalt = UIHelper.GenerateSalt();
            string d = inputPasswordReg.Text;
            k.LozinkaHash = UIHelper.GenerateHash(d, k.LozinkaSalt);
            k.Status = true;
            if (k.Ime == null || k.Prezime == null || k.KorisnickoIme == null || k.Email == null || k.LozinkaSalt == null)
            {

                DisplayAlert("Upozorenje", "Za registraciju je potrebno popuniti sva polja!", "OK");

            }
            else
            {
                HttpResponseMessage response = klijentiService.PostResponse(k);
                if (response.IsSuccessStatusCode)
                {
                    DisplayAlert("Uspjeh", "Uspješno ste se registrovali!", "OK");
                    App.Current.MainPage = new Login();
                    //      Navigation.PushModalAsync(new Login());
                }
                else
                {
                    DisplayAlert("Greška", "Došlo je do greške!", "OK");
                }
            }
        }

    }
}