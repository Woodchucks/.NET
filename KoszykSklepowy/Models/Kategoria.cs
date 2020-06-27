using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Labo7_vol2.Models
{
    public class Kategoria
    {
        [Key]
        public int Id { get; set; }
        protected String _nazwa;
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Nazwa jest wymagana.")]
        public String Nazwa
        {
            get { return _nazwa; }
            set { _nazwa = value; }
        }
        virtual public ICollection<Przepis> Przepis { get; set; }
        public int? NadKategoriaId { get; set; }
        [ForeignKey("NadKategoriaId")]
        virtual public Kategoria NadKategoria { get; set; }
        public virtual List<Kategoria> PodKategorie { get; set; }
    }
}
