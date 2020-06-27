using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labo7_vol2.Code;
using Labo7_vol2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labo7_vol2.Controllers
{
    public class ProduktyIOperacjeController : Controller
    {
        public ProduktyIOperacje DajKoszyk()
        {
            if (HttpContext.Session.Get<ProduktyIOperacje>("produktyIOperacje") == null)
                HttpContext.Session.Set<ProduktyIOperacje>("produktyIOperacje",
                    new ProduktyIOperacje());
            return HttpContext.Session.Get<ProduktyIOperacje>("produktyIOperacje");
        }

        public void UaktualnijKoszyk(ProduktyIOperacje koszyk)
        {
            HttpContext.Session.Set<ProduktyIOperacje>("produktyIOperacje", koszyk);
        }
        // GET: ListyPrzepisowZOperacjami
        public ActionResult Index()
        {
            return View(DajKoszyk());
        }
        public ActionResult Delete(int id)
        {
            var koszyk = DajKoszyk();
            koszyk.Usun(id);
            UaktualnijKoszyk(koszyk);
            return RedirectToAction("Index");
        }
        public ActionResult DodajProduktDoKoszyka(int id)
        {
            var koszyk = DajKoszyk();
            koszyk.Produkty[id].Ilosc++;
            UaktualnijKoszyk(koszyk);
            return RedirectToAction("index");
        }
        public ActionResult UsunProduktZKoszyka(int id)
        {
            //if(id>0)
            //{
                var koszyk = DajKoszyk();
                koszyk.Produkty[id].Ilosc--;
                UaktualnijKoszyk(koszyk);
                return RedirectToAction("Index");
            //}
            /*else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
            "alertMessage", @"alert('Nie moze byc ujemna')", true);
            }*/
 
        }
        public ActionResult Details(int id)
        {
            var koszyk = DajKoszyk();
            return RedirectToAction("Details", "Produkty", new 
            { id = koszyk.Produkty[id].Produkt.Id });   
        } 
    }
}