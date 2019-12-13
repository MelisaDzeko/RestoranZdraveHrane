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
    public partial class Vjezba1 : Form
    {

        WebAPIHelper proizvodiService = new WebAPIHelper("http://localhost:54596/", "api/Proizvodi");

        public Vjezba1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 0)
            {
                int proizvodID = Convert.ToInt32(comboBox1.SelectedValue);
                if (proizvodID == 0)
                {
                    Melisa();
                }
                else
                {
                    HttpResponseMessage response = proizvodiService.GetActionResponse("ProizvodById", proizvodID);
                    if (response.IsSuccessStatusCode)
                    {
                        List<ProizvodById_Result> proizvodi = response.Content.ReadAsAsync<List<ProizvodById_Result>>().Result;
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

        private void Melisa()
        {
            HttpResponseMessage response = proizvodiService.GetResponse();
            if (response.IsSuccessStatusCode)
            {
                List<AllProizvodi_Result> proizvodi = response.Content.ReadAsAsync<List<AllProizvodi_Result>>().Result;
                dataGridView1.DataSource = proizvodi;
             //   dataGridView1.AutoGenerateColumns = false;
                //  dataGridView1.Columns[0].Visible = false;
            }
            else
            {
                MessageBox.Show("Error Code:" + response.StatusCode + "Message:" + response.ReasonPhrase);
            }
        }

        private void Vjezba1_Load(object sender, EventArgs e)
        {
            Melisa();
            LoadProizvodi();
        }

        private void LoadProizvodi()
        {
            HttpResponseMessage response = proizvodiService.GetResponse();

            if (response.IsSuccessStatusCode)
            {
                List<AllProizvodiVjezba1_Result> proizvodi = response.Content.ReadAsAsync<List<AllProizvodiVjezba1_Result>>().Result;
                proizvodi.Insert(0, new AllProizvodiVjezba1_Result());
                proizvodi[0].NazivProizvoda = "Melisa cao";
                comboBox1.DataSource = proizvodi;
                comboBox1.DisplayMember = "NazivProizvoda";
                comboBox1.ValueMember = "ProizvodID";
            }
        }
    }
}
