﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labo6.Models
{
    public class PrzepisProdukt
    {
        public int PrzepisId { get; set; }
        public Przepis Przepis { get; set; }
        public int ProduktId { get; set; }
        public Produkt Produkt { get; set; }
    }
}
