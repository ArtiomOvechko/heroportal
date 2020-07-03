namespace Core
{
    struct ProfileUpdateModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string ImageUrl { get; set; }
        public short? Race { get; set; }   
    }

    struct ProfileModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string ImageUrl { get; set; }
        public short? Race { get; set; }
        public short Rank { get; set; }
        public int GoldCoins { get; set; }
        public string Email { get; set; }
     }
}