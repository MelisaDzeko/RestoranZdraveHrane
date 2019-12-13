using IB150218.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using IB150218_APII.Models;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IB150218
{
    public partial class Preglednarudzbi : Form
    {
        WebAPIHelper narudzbeService = new WebAPIHelper("http://localhost:54596/", "api/Narudzbe");
        private List<esp_Narudzbe_SelectAktivne_Result> aktivneNarudzbe;
        public Preglednarudzbi()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Detaljinarudzbe detalji = new Detaljinarudzbe(aktivneNarudzbe[e.RowIndex]);
            detalji.ShowDialog();
            this.Close();
        }

        private void Preglednarudzbi_Load(object sender, EventArgs e)
        {
            HttpResponseMessage response = narudzbeService.GetActionResponse("GetAktivneNarudzbe");
            if (response.IsSuccessStatusCode)
            {
                aktivneNarudzbe = response.Content.ReadAsAsync<List<esp_Narudzbe_SelectAktivne_Result>>().Result;
                dataGridView1.DataSource = aktivneNarudzbe;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[2].Visible = false;
            }
            else
            {
                MessageBox.Show("Error Code:" + response.StatusCode + "Message:" + response.ReasonPhrase);

            }
        }
    }
}
