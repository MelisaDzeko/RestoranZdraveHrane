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
    public partial class Evidencijakorisnika : Form
    {
        WebAPIHelper korisniciService = new WebAPIHelper("http://localhost:54596/", "api/Korisnici");
        WebAPIHelper ulogeService = new WebAPIHelper("http://localhost:54596/", "api/Uloge");

        Korisnici k;
        public Evidencijakorisnika()
        {
            InitializeComponent();
            this.AutoValidate = AutoValidate.Disable;
        }

        private void Evidencijakorisnika_Load(object sender, EventArgs e)
        {
            BindUloge();
            BindData();
        }

        private void BindData()
        {
            HttpResponseMessage response = korisniciService.GetResponse();

            if (response.IsSuccessStatusCode)
            {
                List<Korisnici> korisnici = response.Content.ReadAsAsync<List<Korisnici>>().Result;
                dataGridView1.DataSource = korisnici;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[12].Visible = false;
            }
        }

        private void BindUloge()
        {
            HttpResponseMessage response = ulogeService.GetResponse();

            if (response.IsSuccessStatusCode)
            {
                List<esp_Uloge_SelectAll_Result> uloge = response.Content.ReadAsAsync<List<esp_Uloge_SelectAll_Result>>().Result;

                chbUlogeList.DataSource = uloge;
                chbUlogeList.DisplayMember = "Naziv";
                chbUlogeList.ValueMember = "UlogaID";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                if(k==null)
                   k = new Korisnici();
                k.Ime = txtIme.Text;
                k.Prezime = txtPrezime.Text;
                k.Email = txtMail.Text;
                k.Telefon = txtTelefon.Text;
                k.KorisnickoIme = txtKorisnickoIme.Text;
                k.Status = true;

                k.LozinkaSalt = UIHelper.GenerateSalt();
                k.LozinkaHash = UIHelper.GenerateHash(txtLozinka.Text, k.LozinkaSalt);
                
                k.UlogaID=Convert.ToInt32( chbUlogeList.SelectedValue);


                HttpResponseMessage response;
                if (k.KorisnikID == 0)
                {
                   response= korisniciService.PostResponse(k);
                }
                else
                {
                   response= korisniciService.PutResponse(k.KorisnikID,k);
                }
                if (response.IsSuccessStatusCode)
                {
                    const string message =
    "Uspješno ste dodali podatke o korisniku!";
                    const string caption = "Informacija";

                    var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInput();
                    BindData();
                }
                else
                {
                    const string message = "Podaci o korisniku nisu sačuvani!";
                    const string caption = "Informacija";

                    var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void ClearInput()
        {
            txtIme.Text = txtPrezime.Text = txtMail.Text = txtTelefon.Text = txtKorisnickoIme.Text = txtLozinka.Text = "";
        }

        private void txtIme_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtIme.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtIme, "Ime je obavezno.");
            }
        }

        private void txtPrezime_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtPrezime.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtPrezime, "Prezime je obavezno.");
            }
        }

        private void txtMail_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtMail.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtMail, "E-mail je obavezan.");
            }
        }

        private void txtTelefon_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtTelefon.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtTelefon, "Telefon je obavezan.");
            }
        }

        private void txtKorisnickoIme_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtKorisnickoIme.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtKorisnickoIme, "Korisničko ime je obavezno.");
            }
        }

    

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            txtLozinka.Visible = false;
            k = (Korisnici)dataGridView1.CurrentRow.DataBoundItem;

            txtIme.Text = k.Ime;
            txtPrezime.Text = k.Prezime;
            txtMail.Text = k.Email;
            txtTelefon.Text = k.Telefon;
            txtKorisnickoIme.Text = k.KorisnickoIme;
         
            int i =Convert.ToInt32( k.UlogaID);
            if (i == 1)
            {
                chbUlogeList.SetItemCheckState(0, CheckState.Checked);
            }
            if (i == 2)
            {
                chbUlogeList.SetItemCheckState(1, CheckState.Checked);
            }
            if (i == 3)
            {
                chbUlogeList.SetItemCheckState(2, CheckState.Checked);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            k = (Korisnici)dataGridView1.CurrentRow.DataBoundItem;
            HttpResponseMessage response = korisniciService.DeleteResponse(k.KorisnikID);
            if (response.IsSuccessStatusCode || response.StatusCode == 0)
            {
                const string message =
"Korisnik obrisan!";
                const string caption = "Informacija";

                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                BindData();
                dataGridView1.AutoGenerateColumns = false;
            }

            BindData();
        }
    }
}
