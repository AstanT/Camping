using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Camping.ViewModels
{
    public class ListServisesViewModel
    {
        public string Type { get; set; }
        public List<ServicesViewModel> SeervicesList { get; set; }
    }
}