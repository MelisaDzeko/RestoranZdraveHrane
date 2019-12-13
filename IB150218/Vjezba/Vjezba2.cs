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

namespace IB150218.Vjezba
{
    public partial class Vjezba2 : Form
    {
        WebAPIHelper proizvodiService = new WebAPIHelper("http://localhost:54596/", "api/Proizvodi");
        WebAPIHelper jediniceMjereService = new WebAPIHelper("http://localhost:54596/", "api/JediniceMjere");


        public Vjezba2()
        {
            InitializeComponent();
        }

        private void Vjezba2_Load(object sender, EventArgs e)
        {
            BindJedinicaMjere();
            BindData();


        }

        private void BindJedinicaMjere()
        {

            HttpResponseMessage response = jediniceMjereService.GetResponse();

            if (response.IsSuccessStatusCode)
            {
                List<JediniceMjere> jediniceMjere = response.Content.ReadAsAsync<List<JediniceMjere>>().Result;
                jediniceMjere.Insert(0, new JediniceMjere());
                jediniceMjere[0].Naziv = "Odaberite jedinicu mjere";
                comboBox1.DataSource = jediniceMjere;
                comboBox1.DisplayMember = "Naziv";
                comboBox1.ValueMember = "JedinicaMjereID";
            }
        }

        private void BindData()
        {
            HttpResponseMessage response = proizvodiService.GetResponse();
            if (response.IsSuccessStatusCode)
            {
                List<AllProizvodiVjezba1_Result> proizvodi = response.Content.ReadAsAsync<List<AllProizvodiVjezba1_Result>>().Result;
                dataGridView1.DataSource = proizvodi;
                //   dataGridView1.AutoGenerateColumns = false;
                //  dataGridView1.Columns[0].Visible = false;
            }
            else
            {
                MessageBox.Show("Error Code:" + response.StatusCode + "Message:" + response.ReasonPhrase);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex != 0)
            {
                int jedinicaMjereID = Convert.ToInt32(comboBox1.SelectedValue);
                if (jedinicaMjereID == 0)
                {
                    BindData();
                }
                else
                {
                    HttpResponseMessage response = proizvodiService.GetActionResponse("ProizvodByJediniceMjereID", jedinicaMjereID);
                    if (response.IsSuccessStatusCode)
                    {
                        List<Proizvodi_SelectByJedinicaMjere_Result> proizvodi = response.Content.ReadAsAsync<List<Proizvodi_SelectByJedinicaMjere_Result>>().Result;
                        dataGridView1.DataSource = proizvodi;
                        //  dataGridView1.AutoGenerateColumns = false;
                        //  dataGridView1.Columns[0].Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Error Code:" + response.StatusCode + "Message:" + response.ReasonPhrase);
                    }
                }
            }
        }
    }
}
