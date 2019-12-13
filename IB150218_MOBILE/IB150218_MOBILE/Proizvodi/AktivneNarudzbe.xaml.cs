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

namespace IB150218_MOBILE.Proizvodi
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AktivneNarudzbe : ContentPage
	{
        
        private WebAPIHelper narudzbaService = new WebAPIHelper("http://192.168.1.8/", "api/Narudzbe");

        private WebAPIHelper narudzbaStavkesService = new WebAPIHelper("http://192.168.1.8/", "api/NarudzbaStavkes");
        public AktivneNarudzbe ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            if (Global.aktivnaNarudzba != null)
            {

                List<IB150218_PCL.Model.Proizvodi> proizvodis= new List<IB150218_PCL.Model.Proizvodi>();
                IB150218_PCL.Model.Proizvodi p;
                decimal iznos = 0;

                foreach (NarudzbaStavke item in Global.aktivnaNarudzba.NarudzbaStavke)
                {
                    iznos += item.Proizvodi.Cijena * item.Kolicina;
                    p = new IB150218_PCL.Model.Proizvodi();
                    p.Cijena = item.Proizvodi.Cijena;
                    p.Naziv = item.Proizvodi.Naziv;
                    p.Sifra = item.Proizvodi.Sifra;
                    proizvodis.Add(p);
                    p=null;
                }
                MenuItemsListView.ItemsSource = proizvodis;
                IznosLabel.Text = "Ukupan iznos: " + Math.Round(iznos, 2) + "KM";
            }
            else
            {
                DisplayAlert( "Informacija","Trenutno nemate aktivnih narudzbi","OK");
                this.Navigation.PushModalAsync(new Navigation.MasterDetailPage1(new Proizvodi.Odabir()));
            }
            base.OnAppearing();
        }

        private void Pretraga_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new Navigation.MasterDetailPage1(new Proizvodi.Pregledproizvoda()));
        }

        private void Zakljuci_Clicked(object sender, EventArgs e)
        {
            if (Global.aktivnaNarudzba != null)
            {
               
                    HttpResponseMessage response = narudzbaService.PostResponse(Global.aktivnaNarudzba);

                    if (response.IsSuccessStatusCode)
                    {
                    HttpResponseMessage response2 = narudzbaService.GetActionResponse("NarudzbaLast");

                    if (response2.IsSuccessStatusCode)
                    {
                        var jsonResult = response2.Content.ReadAsStringAsync();

                    Narudzbe narudzba = JsonConvert.DeserializeObject<Narudzbe>(jsonResult.Result);
                        foreach (NarudzbaStavke item in Global.aktivnaNarudzba.NarudzbaStavke)
                        {
                            item.NarudzbaID = narudzba.NarudzbaID;
                            HttpResponseMessage response1 = narudzbaStavkesService.PostResponse(item);

                            if (response1.IsSuccessStatusCode)
                            {
                                DisplayAlert("Informacija", "Uspješno ste zaključili narudžbu.", "OK");
                                this.Navigation.PushModalAsync(new Navigation.MasterDetailPage1(new Proizvodi.Odabir()));

                                Global.aktivnaNarudzba = null;


                            }
                        }
        
                    }
                  
                    }
                    else
                    {
                        foreach (NarudzbaStavke n in Global.aktivnaNarudzba.NarudzbaStavke)
                        {
                            DisplayAlert("Informacija", "Niste zaključili narudžbu.", "OK");

                        }

                        Global.aktivnaNarudzba = null;
                    }
                
            }
        }
    }
}