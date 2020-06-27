using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Labo7_vol2.Models
{
    public class ProduktyIOperacje
    {
        public List<ProduktWKoszyku> Produkty{ get; set; } = new List<ProduktWKoszyku>();
        public void Usun(int id)
        {
            if (id >= 0 && id < Rozmiar()) Produkty.RemoveAt(id);
        }
        public bool Dodaj(Produkt produkt)
        {
            if (produkt == null) return false;
            int id = Produkty.FindIndex(m => m.Produkt.Id == produkt.Id);
            if (id < 0) Produkty.Add(new ProduktWKoszyku() { Produkt = produkt, Ilosc = 1 });
            else Produkty[id].Ilosc++;
            return true;
        }
        public int Rozmiar()
        {
            return Produkty.Count;
        }
        public double SumarycznaLiczba()
        {
            double suma = 0;
            foreach (ProduktWKoszyku produkt in Produkty)
                suma += produkt.Ilosc;
            return suma;
        }
        public double SumarycznyKoszt()
        {
            double suma = 0;
            foreach (ProduktWKoszyku produkt in Produkty)
                suma += produkt.Ilosc * produkt.Produkt.Cena;
            return suma;
        }
    }
}
