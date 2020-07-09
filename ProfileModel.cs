using System.Collections.Generic;

namespace Core
{
    public struct ProfileUpdateModel 
    {
        public int id { get; set; }
        public string name { get; set; }
        public string nickName { get; set; }
        public string imageUrl { get; set; }
        public short? race { get; set; }   
    }

    public struct ProfileModel 
    {
        public int id { get; set; }
        public string name { get; set; }
        public string nickName { get; set; }
        public string imageUrl { get; set; }
        public short? race { get; set; }
        public short rank { get; set; }
        public int goldCoins { get; set; }
        public string email { get; set; }
        public int level { get; set; }
        public int nextlevel { get; set; }
        public int pointsToNextLevel { get; set; }
        public int levelCompleted { get; set; }
        public List<SkillModel> skills { get; set; }
        public List<AchievementModel> achievements { get; set; }
        public List<BadgeModel> badges { get; set; }
    }
}