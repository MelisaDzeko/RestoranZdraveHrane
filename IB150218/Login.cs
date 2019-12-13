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
    public partial class Login : Form
    {
        WebAPIHelper korisniciService = new WebAPIHelper("http://localhost:54596/", "api/Korisnici");
        public Login()
        {
            InitializeComponent();
        }

        private void btn_Trazi_Click(object sender, EventArgs e)
        {
            HttpResponseMessage response = korisniciService.GetActionResponse("KorisnikByUserName", txtIme.Text);

            if (response.IsSuccessStatusCode)
            {
                Korisnici k = response.Content.ReadAsAsync<Korisnici>().Result;
                if (k.LozinkaHash == UIHelper.GenerateHash(textBox1.Text, k.LozinkaSalt))
                {
                    DialogResult = DialogResult.OK;
                    Global.TrenutnoPrijavljeni = k;
                    Close();
                }
                else
                    MessageBox.Show("Niste unijeli ispravan password!","Informacija");
                textBox1.Text = String.Empty;
            }
            else
                MessageBox.Show("Error Code " + response.StatusCode + "Message " + response.ReasonPhrase);
        }


    }
}
