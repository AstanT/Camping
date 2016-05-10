using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Camping.ViewModels
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        public long IdService { get; set; }
        public long ClientsAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateOrder { get; set; }
        public bool IsActive { get; set; }
    }
}