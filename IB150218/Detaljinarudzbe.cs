using IB150218_APII.Models;
using IB150218_PCL.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IB150218
{
    public partial class Detaljinarudzbe : Form
    {
        WebAPIHelper izlaziService = new WebAPIHelper("http://localhost:54596/", "api/Izlazi");
        WebAPIHelper narudzbeService = new WebAPIHelper("http://localhost:54596/", "api/Narudzbe");

        private esp_Narudzbe_SelectAktivne_Result narudzba { get; set; }

        public Detaljinarudzbe(esp_Narudzbe_SelectAktivne_Result narudzba)
        {
            InitializeComponent();
            this.narudzba = narudzba;
        }

        private  void btnProcesiraj_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                Izlazi izlaz = new Izlazi();
                izlaz.NarudzbaID = narudzba.NarudzbaID;
                izlaz.IznosSaPDV = (decimal)narudzba.Iznos;
                izlaz.IznosBezPDV = (decimal)narudzba.Iznos / (decimal)1.17;
                izlaz.KorisnikID = Global.TrenutnoPrijavljeni.KorisnikID;

                HttpResponseMessage response = izlaziService.PostResponse(izlaz);
                
                if (response.IsSuccessStatusCode)
                {

                    MessageBox.Show("Uspjesno ste procesirali narudzbu");
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Narudžba nije procesirana");
                }
            }
        }

        private void Detaljinarudzbe_Load(object sender, EventArgs e)
        {
            lblBrojNarudzbe.Text = Convert.ToString(narudzba.BrojNarudzbe);
            lblDatum.Text = narudzba.Datum.ToString();
            lblKupac.Text = narudzba.Kupac;
            lblIznos.Text = narudzba.Iznos.ToString() + " KM";

            HttpResponseMessage response = narudzbeService.GetActionResponse("GetStavkeNarudzbe", narudzba.NarudzbaID.ToString());
            if (response.IsSuccessStatusCode)
            {
                dataGridView1.DataSource = response.Content.ReadAsAsync<List<esp_NarudzbeStavke_SelectByNarudzbaID_Result>>().Result;
                dataGridView1.Columns[0].Visible = false;

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void lblBrojNarudzbe_Click(object sender, EventArgs e)
        {

        }

        private void lblDatum_Click(object sender, EventArgs e)
        {

        }

        private void lblKupac_Click(object sender, EventArgs e)
        {

        }
    }
}
