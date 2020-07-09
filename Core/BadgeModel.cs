using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public struct BadgeModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        public DateTime sendDate { get; set; }
        public int number { get; set; }
        public int cost { get; set; }
    }
}
