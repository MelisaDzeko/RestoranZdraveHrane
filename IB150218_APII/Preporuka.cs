using IB150218_APII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB150218_APII
{
    public class Preporuka
    {
        Dictionary<int?, List<Ocjene>> proizvod = new Dictionary<int?, List<Ocjene>>();
        private eProdajaEntities db = new eProdajaEntities();


        public List<Recommended_Result> GetSlicniProizvodi(int proizvodID)
        {
            UcitajKupce(proizvodID);
            List<Ocjene> ocjene = db.Ocjenes.Where(x => x.ProizvodID == proizvodID).OrderBy(x => x.KupacID).ToList();

            List<Ocjene> zajednickeOcjene1 = new List<Ocjene>();
            List<Ocjene> zajednickeOcjene2 = new List<Ocjene>();

            List<Recommended_Result> preporuceno = new List<Recommended_Result>();


            foreach (var item in proizvod)
            {
                foreach (Ocjene o in ocjene)
                {

                    if (item.Value.Where(x => x.KupacID == o.KupacID).Count() > 0)
                    {
                        zajednickeOcjene1.Add(o);
                        zajednickeOcjene2.Add(item.Value.Where(x => x.KupacID == o.KupacID).First());
                    }
                }


                double slicnost = GetSlicnost(zajednickeOcjene1, zajednickeOcjene2);
                if (slicnost > 0.6)
                    preporuceno.Add(db.Recommended(item.Key).FirstOrDefault());


                zajednickeOcjene1.Clear();
                zajednickeOcjene2.Clear();
            }


            return preporuceno;
        }


        private void UcitajKupce(int ProizvodID)
        {
            List<Proizvodi> aktivnaProizvodi= db.Proizvodis.Where(x => x.ProizvodID != ProizvodID).ToList();

            List<Ocjene> ocjene;
            foreach (Proizvodi k in aktivnaProizvodi)
            {
                ocjene = db.Ocjenes.Where(x => x.ProizvodID == k.ProizvodID).OrderBy(x => x.KupacID).ToList();
                if (ocjene.Count > 0)
                    proizvod.Add(k.ProizvodID, ocjene);

            }
        }


        double GetSlicnost(List<Ocjene> ocjene1, List<Ocjene> ocjene2)
        {
            if (ocjene1.Count != ocjene2.Count)
                return 0;

            int? brojnik = 0;
            int? int1 = 0;
            int? int2 = 0;

            for (int i = 0; i < ocjene1.Count; i++)
            {
                brojnik += ocjene1[i].Ocjena * ocjene2[i].Ocjena;
                int1 += ocjene1[i].Ocjena * ocjene1[i].Ocjena;
                int2 += ocjene2[i].Ocjena * ocjene2[i].Ocjena;
            }

            double int11 = Math.Sqrt(Convert.ToDouble(int1));
            double int22 = Math.Sqrt(Convert.ToDouble(int2));

            double nazivnik = int11 * int22;
            double brojnik1 = Convert.ToDouble(brojnik);
            if (nazivnik != 0)
                return brojnik1 / nazivnik;

            return 0;

        }
    }
}