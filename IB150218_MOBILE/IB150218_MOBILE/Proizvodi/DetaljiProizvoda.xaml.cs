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
	public partial class DetaljiProizvoda : ContentPage
	{
        int proizvodID;
        private WebAPIHelper proizvodiService = new WebAPIHelper("http://192.168.1.8/", "api/Proizvodi");
        private WebAPIHelper vrsteService = new WebAPIHelper("http://192.168.1.8/", "api/VrsteProizvoda");
        private WebAPIHelper ocjeneService = new WebAPIHelper("http://192.168.1.8/", "api/Ocjene");
        Ocjene a;
        public DetaljiProizvoda (int proizvodID)
		{
			InitializeComponent ();

            this.proizvodID = proizvodID;
		}
        public IB150218_PCL.Model.Proizvodi proizvod { get; set; }
        protected override void OnAppearing()
        {
            BindData();

            base.OnAppearing();
        }

        private void BindData()
        {
            HttpResponseMessage response1 = proizvodiService.GetActionResponse("ProizvodById", proizvodID);
            if (response1.IsSuccessStatusCode)
            {
                var jsonResult = response1.Content.ReadAsStringAsync();
                 proizvod = JsonConvert.DeserializeObject<IB150218_PCL.Model.Proizvodi>(jsonResult.Result);
                odabraniProizvod.BindingContext = proizvod;
               
            }

            HttpResponseMessage response = proizvodiService.GetActionResponse("Recommended", proizvodID);
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = response.Content.ReadAsStringAsync();
                List<Recommended_Result> recommended = JsonConvert.DeserializeObject<List<Recommended_Result>>(jsonResult.Result);
                MenuItemsListView.ItemsSource = recommended;

            }
        }

        private void Naruci_Clicked_1(object sender, EventArgs e)
        {
            if (Global.aktivnaNarudzba == null)
            {

                Global.aktivnaNarudzba = new Narudzbe();
                Global.aktivnaNarudzba.BrojNarudzbe = "1/" + DateTime.Now.Day + "-" + DateTime.Now.Year;
                Global.aktivnaNarudzba.Datum = DateTime.Now;
                Global.aktivnaNarudzba.KupacID = Global.LogiraniKupac.KupacID;

                Global.aktivnaNarudzba.NarudzbaStavke = new List<NarudzbaStavke>();
            }
          

            if (inutKolicina.Text == "")
            {
                DisplayAlert("Upozorenje", "Unesite količinu!", "OK");
            }
            else
            {
                string message = "Uspjesno ste dodali proizvod u korpu";
               
                    NarudzbaStavke s = new NarudzbaStavke();
                    s.ProizvodID = proizvod.ProizvodID;
                    s.Proizvodi = proizvod;
                    s.Kolicina = Convert.ToInt32(inutKolicina.Text);
                

                Global.aktivnaNarudzba.NarudzbaStavke.Add(s);

                
                DisplayAlert("Upozorenje",message, "OK");
                infoLabel.Text = "Broj naručenih proizvoda:" + Global.aktivnaNarudzba.NarudzbaStavke.Count;
                Zakljuci.IsVisible = true;
            }
        }

        private void MenuItemsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
            proizvodID = ((Recommended_Result)e.Item).ProizvodID;
            HttpResponseMessage response1 = proizvodiService.GetActionResponse("ProizvodById", proizvodID);
            if (response1.IsSuccessStatusCode)
            {
                var jsonResult = response1.Content.ReadAsStringAsync();
                 proizvod = JsonConvert.DeserializeObject<IB150218_PCL.Model.Proizvodi>(jsonResult.Result);
                odabraniProizvod.BindingContext = proizvod;

            }

            HttpResponseMessage response = proizvodiService.GetActionResponse("Recommended", proizvodID);
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = response.Content.ReadAsStringAsync();
                List<Recommended_Result> recommended = JsonConvert.DeserializeObject<List<Recommended_Result>>(jsonResult.Result);
                MenuItemsListView.ItemsSource = recommended;

            }
        }
    

        private void Zvjezdica1_Clicked(object sender, EventArgs e)
        {
            a = new Ocjene();
            a.Datum = DateTime.Now;
            a.Ocjena = 2;
            a.ProizvodID = proizvodID;
            a.KupacID = Global.LogiraniKupac.KupacID;

         

            HttpResponseMessage response1 = ocjeneService.PostResponse(a);
            if (response1.IsSuccessStatusCode)
            {
                DisplayAlert("Uspjeh", "Uspješno ste ocjenuli aktivnost ocjenom 1 !", "OK");
                BindData();
            }
        }

        private void Zvjezdica2_Clicked(object sender, EventArgs e)
        {
            a = new Ocjene();
            a.Datum = DateTime.Now;
            a.Ocjena = 2;
            a.ProizvodID = proizvodID;
            a.KupacID = Global.LogiraniKupac.KupacID;

            HttpResponseMessage response1 = ocjeneService.PostResponse(a);
            if (response1.IsSuccessStatusCode)
            {
                DisplayAlert("Uspjeh", "Uspješno ste ocjenuli aktivnost ocjenom 2 !", "OK");
                BindData();
            }
        }

        private void Zvjezdica3_Clicked(object sender, EventArgs e)
        {
            a = new Ocjene();
            a.Datum = DateTime.Now;
            a.Ocjena = 2;
            a.ProizvodID = proizvodID;
            a.KupacID = Global.LogiraniKupac.KupacID;

            HttpResponseMessage response1 = ocjeneService.PostResponse(a);
            if (response1.IsSuccessStatusCode)
            {
                DisplayAlert("Uspjeh", "Uspješno ste ocjenuli aktivnost ocjenom 3 !", "OK");
                BindData();
            }
        }

        private void Zvjezdica4_Clicked(object sender, EventArgs e)
        {
            a = new Ocjene();
            a.Datum = DateTime.Now;
            a.Ocjena = 2;
            a.ProizvodID = proizvodID;
            a.KupacID = Global.LogiraniKupac.KupacID;

            HttpResponseMessage response1 = ocjeneService.PostResponse(a);
            if (response1.IsSuccessStatusCode)
            {
                DisplayAlert("Uspjeh", "Uspješno ste ocjenuli aktivnost ocjenom 4 !", "OK");
                BindData();
            }
        }

        private void Zvjezdica5_Clicked(object sender, EventArgs e)
        {
            a = new Ocjene();
            a.Datum = DateTime.Now;
            a.Ocjena = 2;
            a.ProizvodID = proizvodID;
            a.KupacID = Global.LogiraniKupac.KupacID;

            HttpResponseMessage response1 = ocjeneService.PostResponse(a);
            if (response1.IsSuccessStatusCode)
            {
                DisplayAlert("Uspjeh", "Uspješno ste ocjenuli aktivnost ocjenom 5 !", "OK");
                BindData();
            }
        }

        private void Zakljuci_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new Navigation.MasterDetailPage1(new Proizvodi.AktivneNarudzbe()));
        }
    }
}