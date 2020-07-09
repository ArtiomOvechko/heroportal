using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public struct AchievementModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int typeId { get; set; }
        public string imageUrl { get; set; }
        public DateTime achievedDate { get; set; }
    }
}
