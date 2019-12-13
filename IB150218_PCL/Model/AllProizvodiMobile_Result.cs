using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB150218_PCL.Model
{
   public class AllProizvodiMobile_Result
    {
        public int ProizvodID { get; set; }
        public string Naziv { get; set; }
        public decimal Cijena { get; set; }
        public string Sifra { get; set; }
        public byte[] Slika { get; set; }
        public byte[] SlikaThumb { get; set; }
        public string Vrsta { get; set; }
        public string Vrsta1 { get; set; }
        public string Mjera { get; set; }
    }
}
