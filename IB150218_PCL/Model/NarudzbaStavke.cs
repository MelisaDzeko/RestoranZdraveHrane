using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB150218_PCL.Model
{
 public   class NarudzbaStavke
    {
        public int NarudzbaStavkaID { get; set; }
        public int NarudzbaID { get; set; }
        public int ProizvodID { get; set; }
        public int Kolicina { get; set; }
        
        public virtual Proizvodi Proizvodi { get; set; }
    }
}
