using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IB150218
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void evidencijaZaposlenikaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Evidencijakorisnika form = new Evidencijakorisnika();
            
            form.Show();
        }

        private void pretrageKlijenataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PretragaKlijenata form = new PretragaKlijenata();
         
            form.Show();
        }

        private void evidencijaMeniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dodajmeni form = new Dodajmeni();
        
            form.Show();
        }

        private void evidencijaProizvodaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Proizvodi.Evidencijaproizvoda form = new Proizvodi.Evidencijaproizvoda();
            
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PretragaKlijenata form = new PretragaKlijenata();
            
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dodajmeni form = new Dodajmeni();
        
            form.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Proizvodi.Evidencijaproizvoda form = new Proizvodi.Evidencijaproizvoda();
         
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Evidencijakorisnika form = new Evidencijakorisnika();
      
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Preglednarudzbi form = new Preglednarudzbi();
          
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Report.Rpt form = new Report.Rpt();
         //   IsMdiContainer = true;
        //    form.MdiParent = this;
            form.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void pregledNarudžbiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Preglednarudzbi form = new Preglednarudzbi();

            form.Show();
        }
    }
}
