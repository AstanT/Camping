using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Camping.ViewModels
{
    public class ServicePageViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int ClientMax { get; set; }
        public string Description { get; set; }
        public short Rating { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public decimal Prepaymant { get; set; }
        public string NewComment { get; set; }
        public string NewPhoto { get; set; }
        public long IdUserInSystem { get; set; }
        public List<PhotoViewModel> Photos { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}