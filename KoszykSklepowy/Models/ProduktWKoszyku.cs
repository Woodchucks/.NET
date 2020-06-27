using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Labo7_vol2.Models
{
    public class ProduktWKoszyku
    {
        [Key]
        public int Id { get; set; }
        public Produkt Produkt { get; set; }
        public int Ilosc { get; set; }
        public int PrzepisId { get; set; }
        [ForeignKey("PrzepisId")]
        virtual public Przepis Przepis { get; set; }
    }
}
