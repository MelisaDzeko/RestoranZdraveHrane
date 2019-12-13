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

namespace IB150218.Proizvodi
{
    public partial class Pretragaproizvoda : Form
    {
        WebAPIHelper proizvodiService = new WebAPIHelper("http://localhost:54596/", "api/Proizvodi");
        private WebAPIHelper vrsteService = new WebAPIHelper("http://localhost:54596/", "api/VrsteProizvoda");
        public Pretragaproizvoda()
        {
            InitializeComponent();
        }

      

        private void Pretragaproizvoda_Load(object sender, EventArgs e)
        {
            BindData();
            BindVrste();
        }

        private void BindVrste()
        {
            HttpResponseMessage response = vrsteService.GetResponse();

            if (response.IsSuccessStatusCode)
            {
                List<VrsteProizvoda> vrste = response.Content.ReadAsAsync<List<VrsteProizvoda>>().Result;
                vrste.Insert(0, new VrsteProizvoda());
                vrste[0].Naziv = "Odaberite vrstu proizvoda";
                vrsteLista.DataSource = vrste;
                vrsteLista.DisplayMember = "Naziv";
                vrsteLista.ValueMember = "VrstaID";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int vrstaID = Convert.ToInt32(vrsteLista.SelectedValue);
            if (vrstaID == 0)
            {
                BindData();
            }
            else
            {
                HttpResponseMessage response = proizvodiService.GetActionResponse("ByVrstaID", vrstaID);
                if (response.IsSuccessStatusCode)
                {
                    List<esp_Proizvodi_SelectByVrsta_Result> proizvodi = response.Content.ReadAsAsync<List<esp_Proizvodi_SelectByVrsta_Result>>().Result;
                    dataGridView1.DataSource = proizvodi;
                    dataGridView1.AutoGenerateColumns = false;
                    //  dataGridView1.Columns[0].Visible = false;
                }
                else
                {
                    MessageBox.Show("Error Code:" + response.StatusCode + "Message:" + response.ReasonPhrase);
                }
            }
        }

        private void BindData()
        {
            HttpResponseMessage response = proizvodiService.GetResponse();
            if (response.IsSuccessStatusCode)
            {
                List<AllProizvodi_Result> proizvodi = response.Content.ReadAsAsync<List<AllProizvodi_Result>>().Result;
                dataGridView1.DataSource = proizvodi;
                dataGridView1.AutoGenerateColumns = false;
            }
            else
            {
                MessageBox.Show("Error Code:" + response.StatusCode + "Message:" + response.ReasonPhrase);
            }
        }
    }
}
