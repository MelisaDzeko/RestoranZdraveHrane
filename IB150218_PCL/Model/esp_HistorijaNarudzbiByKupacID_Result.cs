using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB150218_PCL.Model
{
  public  class esp_HistorijaNarudzbiByKupacID_Result
    {
        public string DatumNaruczbe { get; set; }
        public string Naziv { get; set; }
        public byte[] Slika { get; set; }
        public byte[] SlikaThumb { get; set; }
        public Nullable<decimal> Iznos { get; set; }
    }
}
