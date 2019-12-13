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
	public partial class HistorijaNarudzbi : ContentPage
	{

        private WebAPIHelper narudzbaService = new WebAPIHelper("http://192.168.1.8/", "api/Narudzbe");
        public HistorijaNarudzbi ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            BindData();

            base.OnAppearing();
        }

        private void BindData()
        {
            HttpResponseMessage response1 = narudzbaService.GetActionResponse("HistorijaNarudzbi", Global.LogiraniKupac.KupacID);
            if (response1.IsSuccessStatusCode)
            {
                var jsonResult = response1.Content.ReadAsStringAsync();
                List<esp_HistorijaNarudzbiByKupacID_Result>  proizvod = JsonConvert.DeserializeObject<List<esp_HistorijaNarudzbiByKupacID_Result>>(jsonResult.Result);
                MenuItemsListView.ItemsSource = proizvod;

            }
        }
        }
}