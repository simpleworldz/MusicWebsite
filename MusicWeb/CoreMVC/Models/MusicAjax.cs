using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models
{
    public class MusicAjax
    {
        public string Input { get; set; }
        public string Filter { get; set; }
        public string Type { get; set; }
        public int Page { get; set; }
    }
}
