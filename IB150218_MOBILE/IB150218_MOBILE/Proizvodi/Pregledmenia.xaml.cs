using IB150218_PCL;
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
	public partial class Pregledmenia : ContentPage
	{
        private WebAPIHelper meniService = new WebAPIHelper("http://192.168.1.8//", "api/Meni");
        public Pregledmenia ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            try
            {
              
                HttpResponseMessage response1 = meniService.GetResponse();
                if (response1.IsSuccessStatusCode)
                {
                    var jsonResult = response1.Content.ReadAsStringAsync();
                    List<Allmeni> proizvodi = JsonConvert.DeserializeObject<List<Allmeni>>(jsonResult.Result);
                    MenuItemsListView.ItemsSource = proizvodi;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
            base.OnAppearing();
        }
    }
}