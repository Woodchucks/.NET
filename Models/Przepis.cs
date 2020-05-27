using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labo6.Models
{
    public class Przepis
    {
        [Key]
        public int Id { get; set; }
        protected String _nazwa;
        [MaxLength(40)]
        [Display(Name = "Nazwa przepisu")]
        [Required(ErrorMessage = "Nazwa przepisu jest wymagana.")]
        public String Nazwa
        {
            get { return _nazwa; }
            set { _nazwa = value; }
}
        protected String _hasztag;
        [Display(Name = "Hasztag")]
        [Required(ErrorMessage = "Hasztag jest wymagany.")]
        [RegularExpression("(#(?![0-9_])([a-zA-Z0-9_]{1,30}))", ErrorMessage = "Wprowadzony hasztag jest niepoprawny, przykad: #pomidor#mleko")]
        public String Hasztag
        {
            get { return _hasztag; }
            set { _hasztag = value; }
        }
        protected int _ocena;
        [Display(Name = "Ocena")]
        [Required(ErrorMessage = "Ocena jest wymagana.")]
        [Range(1, 5, ErrorMessage = "Ocena musi być liczbą całkowitą z przedziału <1;5>, gdzie 1 - dostateczna, 5 - bardzo dobra")]
        public int Ocena
        {
            get { return _ocena; }
            set { _ocena = value; }
        }
        /*[MaxLength(15)]
        protected String _nazwaPlikuZdjecia1;
        public String NazwaPlikuZdjecia1
        {
            get { return _nazwaPlikuZdjecia1; }
            set { _nazwaPlikuZdjecia1 = value; }
        }
        [MaxLength(50)]
        public String _sciezkaZdjecia1;
        protected String SciezkaZdjecia1
        {
            get { return _sciezkaZdjecia1; }
            set { _sciezkaZdjecia1 = value; }
        }
        [Display(Name = "Zdjecie")]
        //protected IFormFile _plikZdjecia1;
        protected HttpPostedFileBase _plikZdjecia1;
        //public IFormFile PlikZdjecia1
        public HttpPostedFileBase PlikZdjecia1
        {
            get { return _plikZdjecia1; }
            set { _plikZdjecia1 = value; }
        }*/
        [Required(ErrorMessage = "Instrukcja wykonania przepisu jest wymagana.")]
        [Display(Name = "Instrukcja")]
        [MaxLength(1000)]
        protected String _tresc;
        public String Tresc
        {
            get { return _tresc; }
            set { _tresc = value; }
        }
        public int KategoriaId { get; set; }
        [ForeignKey("KategoriaId")]
        virtual public Kategoria Kategoria { get; set; }
        //virtual public ICollection<PrzepisProdukt> Produkty { get; set; }
    }
}
