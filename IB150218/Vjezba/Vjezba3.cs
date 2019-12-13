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
    public partial class Vjezba3 : Form
    {
        WebAPIHelper narudzbeService = new WebAPIHelper("http://localhost:54596/", "api/Narudzbe");

        public Vjezba3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime d1 = dateTimePicker1.Value;
            string datumOd = d1.ToString("MM-dd-yyy");
            DateTime d2 = dateTimePicker2.Value;
            string datumDo = d2.ToString("MM-dd-yyy");


            HttpResponseMessage response = narudzbeService.GetActionResponseResponse2("AllNarudzbeDateOdDateDo", datumOd, datumDo);
            if (response.IsSuccessStatusCode)
            {
                List<esp_AllNarudzbe_DateOdDateDo_Result> narudzbe = response.Content.ReadAsAsync<List<esp_AllNarudzbe_DateOdDateDo_Result>>().Result;
                dataGridView1.DataSource = narudzbe;

            }
            else
            {
                MessageBox.Show("Error Code:" + response.StatusCode + "Message:" + response.ReasonPhrase);
            }

        }

        private void Vjezba3_Load(object sender, EventArgs e)
        {
            BindData();

        }

        private void BindData()
        {
            HttpResponseMessage response = narudzbeService.GetResponse();
            if (response.IsSuccessStatusCode)
            {
                List<AllNarudzbe_Result> narudzbe = response.Content.ReadAsAsync<List<AllNarudzbe_Result>>().Result;
                dataGridView1.DataSource = narudzbe;
                //   dataGridView1.AutoGenerateColumns = false;
                //  dataGridView1.Columns[0].Visible = false;
            }
            else
            {
                MessageBox.Show("Error Code:" + response.StatusCode + "Message:" + response.ReasonPhrase);
            }
        }
    }
}
