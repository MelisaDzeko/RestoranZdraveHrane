using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB150218_PCL.Model
{
   public class Recommended_Result
    {
        public int ProizvodID { get; set; }
        public string Naziv { get; set; }
        public decimal Cijena { get; set; }
        public string Sifra { get; set; }
        public byte[] Slika { get; set; }
        public byte[] SlikaThumb { get; set; }
        public string Vrsta { get; set; }
        public int VrstaIID { get; set; }
        public int JedinicaMjereID { get; set; }
        public string Mjera { get; set; }
        public Nullable<decimal> ProsjecnaOcjena { get; set; }
    }
}
