using IB150218_MOBILE.ViewModel;
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
	public partial class Pregledproizvoda : ContentPage
	{
        private WebAPIHelper proizvodiService = new WebAPIHelper("http://192.168.1.8/", "api/Proizvodi");
        private WebAPIHelper vrsteService = new WebAPIHelper("http://192.168.1.8/", "api/VrsteProizvoda");
        public List<ViewModel.VrsteProizvoda> vrste;
        public Pregledproizvoda ()
		{
			InitializeComponent ();
            this.BindingContext = new RootModel1
            {
                VrsteList = GetJobsInfo()
            };
        }

        private List<ViewModel.VrsteProizvoda> GetJobsInfo()
        {
            HttpResponseMessage response = vrsteService.GetResponse();
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = response.Content.ReadAsStringAsync();
                vrste = JsonConvert.DeserializeObject<List<ViewModel.VrsteProizvoda>>(jsonResult.Result);
            }
            var db = vrste.ToList();

            return db.ToList();
        }
        protected override void OnAppearing()
        {
            try
            {
              
          
                HttpResponseMessage response1 = proizvodiService.GetActionResponse("AllProizvodiForMobile");
                if (response1.IsSuccessStatusCode)
                {
                    var jsonResult = response1.Content.ReadAsStringAsync();
                    List<AllProizvodiMobile_Result> proizvodi = JsonConvert.DeserializeObject<List<AllProizvodiMobile_Result>>(jsonResult.Result);
                    MenuItemsListView.ItemsSource = proizvodi;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
            base.OnAppearing();
        }
        private void VrstaPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void MenuItemsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (pickerh.SelectedItem != null)
            {
                int proizvodID = ((AllProizvodiByVrstaMobile_Result)e.Item).ProizvodID;

                if (Global.LogiraniKupac.KupacID != 0)
                {

                    this.Navigation.PushModalAsync(new Navigation.MasterDetailPage1(new DetaljiProizvoda(proizvodID)));
               
                }
            }
            else
            {
                int proizvodID = ((AllProizvodiMobile_Result)e.Item).ProizvodID;
              
                if (Global.LogiraniKupac.KupacID != 0)
                {

                    this.Navigation.PushModalAsync(new Navigation.MasterDetailPage1(new DetaljiProizvoda(proizvodID)));
                }
            }
        }

        private void VrstaPicker_SelectedIndexChanged_1(object sender, EventArgs e)
        {

          
        }

        private void Pickerh_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (pickerh.SelectedItem != null)
            {
                int vrstaID = Convert.ToInt32((pickerh.SelectedItem as ViewModel.VrsteProizvoda).VrstaID);
                HttpResponseMessage response = proizvodiService.GetActionResponse("ByVrstaIDMobile", vrstaID.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = response.Content.ReadAsStringAsync();
                    List<AllProizvodiByVrstaMobile_Result> vrsta = JsonConvert.DeserializeObject<List<AllProizvodiByVrstaMobile_Result>>(jsonResult.Result);
                    MenuItemsListView.ItemsSource = vrsta;

                }
            }
        }
    }
}