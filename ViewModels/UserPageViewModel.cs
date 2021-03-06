﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Camping.ViewModels
{
    public class UserPageViewModel
    {
        public long IdUserPage { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<OrderViewModel> LastUserOrders { get; set; } 
    }
}