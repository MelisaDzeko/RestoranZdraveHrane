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
    public partial class Dodajvrstu : Form
    {
        private WebAPIHelper vrsteService = new WebAPIHelper("http://localhost:54596/", "api/VrsteProizvoda");
        VrsteProizvoda v;
        public Dodajvrstu()
        {
            InitializeComponent();
            this.AutoValidate = AutoValidate.Disable;
        }

        private void Dodajvrstu_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                if (v == null)
                    v = new VrsteProizvoda();
                v.Naziv = textBox1.Text;
                HttpResponseMessage response = vrsteService.PostResponse(v);
                
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Uspjesno ste evidentirali vrstu proizvoda!");
                    ClearInput();
                    Close();
                }
                else
                {
                    MessageBox.Show("Greska");
                }
            }
        }

        private void ClearInput()
        {
            textBox1.Text = "";
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(textBox1, "Naziv je obavezan.");
            }
        }
    }
}
