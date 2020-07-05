using System;
using System.Threading.Tasks;


namespace Core
{
    interface IHeroApi 
    {
        delegate void LoginEventHandler(object sender, ProfileModel response);

        event EventHandler LoggedOut;

        event LoginEventHandler LoggedIn;

        Task<bool> IsLoggedIn();

        Task<bool> LogOut();

        Task<ProfileModel> LogIn(LoginModel model);
        
        Task<NameModel[]> GetRanks();

        Task<NameModel[]> GetRaces();

        Task<string[]> GetAvatars();

        Task<ProfileModel> GetProfile();

        Task<ProfileModel> UpdateProfile(ProfileUpdateModel model);
    }
}