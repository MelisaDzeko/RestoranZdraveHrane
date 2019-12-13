using IB150218_PCL.Model;
using IB150218_PCL.Util;
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
	public partial class IzmjenaPodatakaOKlijentu : ContentPage
	{
        private WebAPIHelper klijentiService = new WebAPIHelper("http://192.168.1.8/", "api/Kupci");

        Kupci k;
        public IzmjenaPodatakaOKlijentu ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            Kupci k = Global.LogiraniKupac;

            inputUsername.Text = k.KorisnickoIme;
            inputIme.Text = k.Ime;
            inputPrezime.Text = k.Prezime;
            inputMail.Text = k.Email;
            inputPassword.Text = null;
            base.OnAppearing();
        }
        private void BtnIzmjeni_Clicked(object sender, EventArgs e)
        {
            k = Global.LogiraniKupac;
            k.Ime = inputIme.Text;
            k.Prezime = inputPrezime.Text;
            k.KorisnickoIme = inputUsername.Text;
            k.Email = inputMail.Text;
            k.DatumRegistracije = DateTime.Now;
            string salt = UIHelper.GenerateSalt();
            k.LozinkaSalt = salt;
          string hash= UIHelper.GenerateHash(inputPassword.Text, k.LozinkaSalt);
            k.LozinkaHash = hash;
            k.Status = true;
            if (k.Ime == null || k.Prezime == null || k.KorisnickoIme == null || k.Email == null || k.LozinkaSalt == null || inputPassword.Text == null)
            {

                DisplayAlert("Upozorenje", "Za izmjenu je potrebno popuniti sva polja!", "OK");

            }
            else
            {
                HttpResponseMessage response = klijentiService.PutResponse(k.KupacID, k);
                if (response.IsSuccessStatusCode)
                {
                    DisplayAlert("Uspjeh", "Uspješno ste izmjenuli vaše podatke!", "OK");
                    App.Current.MainPage = new Login();

                }
                else
                {
                    DisplayAlert("Greška", "Došlo je do greške!", "OK");
                }
            }
        }
    }
}