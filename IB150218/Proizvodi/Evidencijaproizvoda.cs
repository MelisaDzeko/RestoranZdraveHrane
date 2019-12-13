using IB150218.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IB150218_APII.Models;
using System.Net.Http;
using System.Drawing.Imaging;

namespace IB150218.Proizvodi
{
    public partial class Evidencijaproizvoda : Form
    {
        WebAPIHelper proizvodiService = new WebAPIHelper("http://localhost:54596/", "api/Proizvodi");
        private WebAPIHelper vrsteService = new WebAPIHelper("http://localhost:54596/", "api/VrsteProizvoda");
        bool vrsta, jedinica=false;
        private WebAPIHelper jedinicamjereService = new WebAPIHelper("http://localhost:54596/", "api/JediniceMjere");
        private IB150218_APII.Models.Proizvodi Proizvod { get; set; }
        AllProizvodiForAdd3_Result p;
        public Evidencijaproizvoda()
        {
            InitializeComponent();
            this.AutoValidate = AutoValidate.Disable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dodajvrstu form = new Dodajvrstu();
            form.Show();
            LoadVrsta();
            LoadJediniceMjere();
            BindGrid();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Pretragaproizvoda form = new Pretragaproizvoda();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {


                if (vrsteLista.SelectedIndex != 0)
                {
                    if (vrsta)
                    {
                        if (Proizvod.ProizvodID != 0)
                        {
                            Proizvod.VrstaID = Convert.ToInt32(vrsteLista.SelectedValue);


                        }
                        else {
                            Proizvod.VrstaID = Convert.ToInt32(vrsteLista.SelectedValue);
                        }
                    }
                    else
                    {
                        Proizvod.VrstaID = Proizvod.VrstaID;
                    }
                    
                }

                if (comboBox1.SelectedIndex != 0)
                {
                    if (jedinica)
                    {
                        if (Proizvod.ProizvodID != 0)
                        {
                            Proizvod.JedinicaMjereID = Convert.ToInt32(comboBox1.SelectedValue);


                        }
                        else
                        {
                            Proizvod.JedinicaMjereID = Convert.ToInt32(comboBox1.SelectedValue);
                        }
                    }
                    else
                    {
                        Proizvod.JedinicaMjereID = Proizvod.JedinicaMjereID;
                    }
                }

                Proizvod.Sifra = txtSifra.Text;
                Proizvod.Naziv = txtNaziv.Text;

                Proizvod.Cijena = Convert.ToDecimal(txtCijena.Text);
              //  Proizvod.Status = true;

                HttpResponseMessage response;
                if (Proizvod.ProizvodID != 0)
                    response = proizvodiService.PutResponse(Proizvod.ProizvodID, Proizvod);
                else
                    response = proizvodiService.PostResponse(Proizvod);
                Proizvodi.Evidencijaproizvoda d = new Proizvodi.Evidencijaproizvoda();


                if (response.IsSuccessStatusCode)
                {

                    MessageBox.Show("Uspješno ste dodali podatke o novom proizvodu", "Informacija");
                    Clear();
                    Close();
                    d.Show();
                }
                else
                {
                    MessageBox.Show("Došlo je do greške prilikom dodavanja novog proizvoda");
                }

                BindGrid();
                Clear();
            }
            }

        private void BindGrid()
        {
            HttpResponseMessage response = proizvodiService.GetActionResponse("AllProizvodiForAdd3");

            if (response.IsSuccessStatusCode)
            {
                List<IB150218_APII.Models.AllProizvodiForAdd3_Result> proizvodi = response.Content.ReadAsAsync<List<IB150218_APII.Models.AllProizvodiForAdd3_Result>>().Result;
                dataGridView1.DataSource = proizvodi;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;

            }
        }

        private void Clear()
        {
            txtSifra.Text = "";
            txtNaziv.Text = "";
            txtCijena.Text = "";
            comboBox1.SelectedIndex = 0;
            vrsteLista.SelectedIndex = 0;
            txtSlika.Text = "";
            pictureBox1.Image = null;
        }

        private void btnDodajSliku_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (p == null)
                {
                    Proizvod = new IB150218_APII.Models.Proizvodi();
                }
                else {
                    Proizvod = new IB150218_APII.Models.Proizvodi();
                    Proizvod.ProizvodID = p.ProizvodID;
                }
                txtSlika.Text = openFileDialog1.FileName;
                Image orgImage = Image.FromFile(openFileDialog1.FileName);
                MemoryStream ms = new MemoryStream();
                orgImage.Save(ms, ImageFormat.Jpeg);
                Proizvod.Slika = ms.ToArray();


                int resizedImageWidth = Convert.ToInt32(ConfigurationManager.AppSettings["resizedImageWidth"]);
                int resizedImageHeight = Convert.ToInt32(ConfigurationManager.AppSettings["resizedImageHeight"]);
                int croppedImageWidth = Convert.ToInt32(ConfigurationManager.AppSettings["croppedImageWidth"]);
                int croppedImageHeight = Convert.ToInt32(ConfigurationManager.AppSettings["croppedImageHeight"]);


                if (orgImage.Width > resizedImageWidth)
                {
                    Image resizedImg = UIHelper.ResizeImage(orgImage, new Size(resizedImageWidth, resizedImageHeight));

                    if (resizedImg.Width > croppedImageWidth && resizedImg.Height > croppedImageHeight)
                    {
                        int croppedXPosition = (resizedImg.Width - croppedImageWidth) / 2;
                        int croppedYPosition = (resizedImg.Height - croppedImageHeight) / 2;

                        Image croppedImg = UIHelper.CropImage(resizedImg, new Rectangle(croppedXPosition, croppedYPosition, croppedImageWidth, croppedImageHeight));
                        pictureBox1.Image = croppedImg;

                        MemoryStream Ms = new MemoryStream();
                        croppedImg.Save(Ms, orgImage.RawFormat);

                        Proizvod.SlikaThumb = Ms.ToArray();

                    }
                    else
                    {
                        //MessageBox.Show(Messages.picture_war + " " + resizedImgWidth + "x" + resizedImgHeight + ".", Messages.warning,
                        //              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Proizvod = null;
                    }



                }
            }
        
        }

        private void Evidencijaproizvoda_Load(object sender, EventArgs e)
        {
            LoadVrsta();
            LoadJediniceMjere();
            BindGrid();
        }

        private void LoadJediniceMjere()
        {
            HttpResponseMessage response = jedinicamjereService.GetResponse();

            if (response.IsSuccessStatusCode)
            {
                List<JediniceMjere> jedinicaMjere = response.Content.ReadAsAsync<List<JediniceMjere>>().Result;
                jedinicaMjere.Insert(0, new JediniceMjere());
                jedinicaMjere[0].Naziv = "Odaberite jedinicu mjere";
                comboBox1.DataSource = jedinicaMjere;
                comboBox1.DisplayMember = "Naziv";
                comboBox1.ValueMember = "JedinicaMjereID";
               
            }
        }

        private void LoadVrsta()
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

        private void button4_Click(object sender, EventArgs e)
        {
             p = new AllProizvodiForAdd3_Result();
            p = (IB150218_APII.Models.AllProizvodiForAdd3_Result)dataGridView1.CurrentRow.DataBoundItem;
          
            vrsteLista.SelectedValue = Convert.ToInt32(p.VrstaIID);
            txtSifra.Text = p.Sifra;
            txtNaziv.Text = p.Naziv;
            txtCijena.Text = p.Cijena.ToString();
            comboBox1.SelectedValue = Convert.ToInt32(p.JedinicaMjereID);
            txtSlika.Text = "Možete odabrati novu sliku za izmjenu";
            var ms = new MemoryStream(p.Slika);
            Image thumbImage = Image.FromStream(ms);
            pictureBox1.Image = thumbImage;
        }


        private void txtSifra_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtSifra.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtSifra, "Sifra je obavezan.");
            }
        }

        private void txtNaziv_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtNaziv.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNaziv, "Naziv je obavezan.");
            }
        }

        private void txtCijena_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtCijena.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCijena, "Cijena je obavezan.");
            }
        }

        private void vrsteLista_Validating(object sender, CancelEventArgs e)
        {
            if (vrsteLista.SelectedIndex == 0)
            {
                errorProvider1.SetError(vrsteLista, "Vrsta je obavezna");
            }
        }

        private void vrsteLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            vrsta = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            jedinica = true;
        }

        private void comboBox1_Validating(object sender, CancelEventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                errorProvider1.SetError(comboBox1, "Vrsta je obavezna");
            }
        }
    }
}
