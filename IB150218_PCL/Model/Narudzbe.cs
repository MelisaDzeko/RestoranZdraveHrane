using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB150218_PCL.Model
{
   public class Narudzbe
    {
        public int NarudzbaID { get; set; }
        public string BrojNarudzbe { get; set; }
        public int KupacID { get; set; }
        public System.DateTime Datum { get; set; }
        public bool Status { get; set; }
        public Nullable<bool> Otkazano { get; set; }
        
        public virtual ICollection<NarudzbaStavke> NarudzbaStavke { get; set; }

    }
}
