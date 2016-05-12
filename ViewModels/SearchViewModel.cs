using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Camping.ViewModels
{
    public class SearchViewModel
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Rating { get; set; }
        public decimal PriceStart { get; set; }
        public decimal PriceEnd { get; set; }
    }
}