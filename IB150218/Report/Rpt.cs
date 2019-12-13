using IB150218.Util;
using IB150218_APII.Models;
using Microsoft.Reporting.WinForms;
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

namespace IB150218.Report
{
    public partial class Rpt : Form
    {
        WebAPIHelper narudzbeService = new WebAPIHelper("http://localhost:54596/", "api/Narudzbe");
        public Rpt()
        {
            InitializeComponent();
        }

        private void Rpt_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
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
                List<esp_AllNarudzbe_DateOdDateDo_Result> NARUDZBE = response.Content.ReadAsAsync<List<esp_AllNarudzbe_DateOdDateDo_Result>>().Result;
                Microsoft.Reporting.WinForms.ReportParameter p1 = new ReportParameter("datumOd", "  " + datumOd);
                Microsoft.Reporting.WinForms.ReportParameter p2 = new ReportParameter("datumDo", "  " + datumDo);

                ReportDataSource rpt = new ReportDataSource("DataSet1", NARUDZBE);

                reportViewer1.LocalReport.ReportEmbeddedResource = "IB150218.Report.Report1.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rpt);
                this.reportViewer1.LocalReport.SetParameters(p1);
                this.reportViewer1.LocalReport.SetParameters(p2);
                NARUDZBE = null;

            }
            datumOd = datumDo = "";
            this.reportViewer1.RefreshReport();
        }
    }
}
