using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB150218_PCL.Model
{
  public  class Ocjene
    {
        public int OcjenaID { get; set; }
        public int ProizvodID { get; set; }
        public int KupacID { get; set; }
        public System.DateTime Datum { get; set; }
        public int Ocjena { get; set; }
    }
}
