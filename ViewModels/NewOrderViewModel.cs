﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Camping.ViewModels
{
    public class NewOrderViewModel
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public long ClientsAmount { get; set; }
    }
}