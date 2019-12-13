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
    public partial class Dodajmeni : Form
    {
        WebAPIHelper meniService = new WebAPIHelper("http://localhost:54596/", "api/Meni");
        private Meni m { get; set; }
        bool search=false;
        serachByNazivmeni_Result d { get; set; }
        public Dodajmeni()
        {
            InitializeComponent();
            this.AutoValidate = AutoValidate.Disable;
        }

        private void Dodajmeni_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            HttpResponseMessage response = meniService.GetResponse();
            if (response.IsSuccessStatusCode)
            {
                List<Meni> korisnici = response.Content.ReadAsAsync<List<Meni>>().Result;
                dataGridView1.DataSource = korisnici;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
            }
            else
            {
                MessageBox.Show("Error Code:" + response.StatusCode + "Message:" + response.ReasonPhrase);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(m==null)
                m= new Meni();
            if (search)
            {
                m.MenidID = d.MenidID;
                m.Naziv = textBox6.Text;
                m.Opis = textBox5.Text;
                m.KorisnikID = Global.TrenutnoPrijavljeni.KorisnikID;
                search = false;
            }
            else
            {
                m.Naziv = textBox6.Text;
                m.Opis = textBox5.Text;
                m.KorisnikID = Global.TrenutnoPrijavljeni.KorisnikID;
            }
            HttpResponseMessage response;
            if (m.MenidID == 0)
            {

               response= meniService.PostResponse(m);

            }
            else
            {
                response = meniService.PutResponse(m.MenidID,m);
            }
            if (response.IsSuccessStatusCode)
            {

                const string message =
"Meni uspješno dodana!";
                const string caption = "Informacija";

                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox5.Text = "";
                textBox6.Text = "";
                LoadData();
                dataGridView1.AutoGenerateColumns = false;
            }
            else
            {
                MessageBox.Show("Error Code:" + response.StatusCode + "Message:" + response.ReasonPhrase);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                LoadData();
            }
            else
            {
                HttpResponseMessage response = meniService.GetActionResponse("SerachMeni", textBox1.Text);
                if (response.IsSuccessStatusCode)
                {
                    List<serachByNazivmeni_Result> meni = response.Content.ReadAsAsync<List<serachByNazivmeni_Result>>().Result;

                    dataGridView1.DataSource = meni;
                    dataGridView1.AutoGenerateColumns = false;
                    search = true;
                }
                else
                {
                    MessageBox.Show("Error Code:" + response.StatusCode + "Message:" + response.ReasonPhrase);
                }
            }
        }
        int meniID;
        private void button4_Click(object sender, EventArgs e)
        {
         
           
            if (search)
            {
                 d = (serachByNazivmeni_Result)dataGridView1.CurrentRow.DataBoundItem;
               
               
                textBox6.Text = d.Naziv;
                textBox5.Text = d.Opis;
        
            }
            else
            {
                m = (Meni)dataGridView1.CurrentRow.DataBoundItem;
                textBox6.Text = m.Naziv;
                textBox5.Text = m.Opis;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            m = (Meni)dataGridView1.CurrentRow.DataBoundItem;
            if (m == null)
            {
                serachByNazivmeni_Result m = (serachByNazivmeni_Result)dataGridView1.CurrentRow.DataBoundItem;
               
            }
            HttpResponseMessage response = meniService.DeleteResponse(m.MenidID);
            if (response.IsSuccessStatusCode || response.StatusCode==0)
            {
                const string message =
"Meni obrisan!";
                const string caption = "Informacija";

                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
                dataGridView1.AutoGenerateColumns = false;
            }

            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            meniID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
        }

        private void textBox6_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox6.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(textBox6, "Naziv je obavezan.");
            }
        }

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox5.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(textBox5, "Opis je obavezan.");
            }
        }
    }
}
