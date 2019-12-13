using IB150218.Util;
using IB150218_APII.Models;
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
    public partial class PretragaKlijenata : Form
    {
        WebAPIHelper klijentiService = new WebAPIHelper("http://localhost:54596/", "api/Kupci");
        public PretragaKlijenata()
        {
            InitializeComponent();
        }

        private void PretragaKlijenata_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            HttpResponseMessage response = klijentiService.GetResponse();
            if (response.IsSuccessStatusCode)
            {
                List<esp_KupciAll_Result> korisnici = response.Content.ReadAsAsync<List<esp_KupciAll_Result>>().Result;
                dataGridView1.DataSource = korisnici;
            }
            else
            {
                MessageBox.Show("Error Code:" + response.StatusCode + "Message:" + response.ReasonPhrase);
            }
        }

        private void btn_Trazi_Click(object sender, EventArgs e)
        {
            if (txtIme.Text == "")
            {

                const string message =
"Za pretragu je potrebno unijeti korisničko ime!";
                const string caption = "Informacija";

                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();

            }
            else
            {
                HttpResponseMessage response = klijentiService.GetActionResponse("SelectByKorisnickoIme", txtIme.Text.Trim());
                if (response.IsSuccessStatusCode)
                {
                    List<esp_KupciSelectByKorisnickoIme_Result> korisnici = response.Content.ReadAsAsync<List<esp_KupciSelectByKorisnickoIme_Result>>().Result;
                    dataGridView1.DataSource = korisnici;
                }
                else
                {
                    MessageBox.Show("Korisničko ime ne postoji u bazi podataka!");
                }
            }
        }
    }
}
