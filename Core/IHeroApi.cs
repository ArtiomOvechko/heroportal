using System;
using System.Threading.Tasks;


namespace Core
{
    interface IHeroApi 
    {
        delegate void LoginEventHandler(object sender, string response);

        event EventHandler LoggedOut;

        event LoginEventHandler LoggedIn;

        Task<bool> IsLoggedIn();

        Task<bool> LogOut();

        Task LogIn(LoginModel model);
        
        Task<NameModel[]> GetRanks();

        Task<NameModel[]> GetRaces();

        Task<string[]> GetAvatars();

        Task<ProfileModel> GetProfile(int? profileId = null);

        Task<ProfileModel> UpdateProfile(ProfileUpdateModel model);

        Task<TeamModel[]> GetTeam();

        Task<SkillModel[]> GetSkills();

        Task<LeaderModel[]> GetTopLeaders();
        Task<BadgeModel[]> GetBadges();
        Task<AchievementModel[]> GetAchievements();
    }
}