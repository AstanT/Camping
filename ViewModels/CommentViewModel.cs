using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Camping.ViewModels
{
    public class CommentViewModel
    {
        public long Id { get; set; }
        public long IdUser { get; set; }
        public long IdService { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string PhotoUser { get; set; }
        public DateTime Date { get; set; }
    }
}