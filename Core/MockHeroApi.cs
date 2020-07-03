using System;
using System.Threading.Tasks;

namespace Core
{
    class MockHeroApi : IHeroApi
    {
        readonly IInteropService _interopService;

        public event EventHandler LoggedOut;

        public event EventHandler LoggedIn;

        public MockHeroApi(IInteropService interopService) 
        {
            _interopService = interopService;
        }

        public async Task<bool> IsLoggedIn() 
        {
            return !string.IsNullOrEmpty(await _interopService.GetLocalStorageItem("auth"));
        }

        public async Task<bool> LogOut() 
        {
            TaskCompletionSource<bool> result = new TaskCompletionSource<bool>();

            result.SetResult(true);
            await _interopService.SetLocalStorageItem("auth", null);

            LoggedOut?.Invoke(this, new EventArgs());

            return await result.Task;
        }

        public Task<string[]> GetAvatars()
        {
            TaskCompletionSource<string[]> source = new TaskCompletionSource<string[]>();
            
            source.SetResult(new string[] 
            {
                "https://1drv.ms/u/s!Au2NFUrUjmCoi6ZofLY4zWDRh7aPrA",
                "https://1drv.ms/u/s!Au2NFUrUjmCoi6Zrj5EbD5Ycui1yBA",
                "https://1drv.ms/u/s!Au2NFUrUjmCoi6ZqME-mGGT_gli1bA?e=c7KSzX"
            });

            return source.Task;
        }

        public Task<ProfileModel> GetProfile()
        {
            TaskCompletionSource<ProfileModel> result = new TaskCompletionSource<ProfileModel>();

            result.SetResult(new ProfileModel() 
            {
                Id = 1,
                Name = "Artem Ovechko",
                Nickname = "AJ",
                ImageUrl = "https://1drv.ms/u/s!Au2NFUrUjmCoi6ZofLY4zWDRh7aPrA",
                Race = 1,
                Rank = 1,
                GoldCoins = 999,
                Email = "ovechko.a@dbbest.com"
            });

            return result.Task;
        }

        public Task<NameModel[]> GetRaces()
        {
            TaskCompletionSource<NameModel[]> result = new TaskCompletionSource<NameModel[]>();
        
            result.SetResult(new NameModel[] 
            {
                new NameModel() { Id = 1, Name = "Dwarf" },
                new NameModel() { Id = 2, Name = "Elf" },
                new NameModel() { Id = 3, Name = "Mage" }
            });

            return result.Task;
        }

        public Task<NameModel[]> GetRanks()
        {
            TaskCompletionSource<NameModel[]> result = new TaskCompletionSource<NameModel[]>();
        
            result.SetResult(new NameModel[] 
            {
                new NameModel() { Id = 1, Name = "Newbie" },
                new NameModel() { Id = 2, Name = "Scholar" },
                new NameModel() { Id = 3, Name = "Master" },
                new NameModel() { Id = 4, Name = "Champion" }
            });

            return result.Task;        }

        public async Task<ProfileModel> LogIn(LoginModel model)
        {
            await Task.Delay(2000);

            TaskCompletionSource<ProfileModel> result = new TaskCompletionSource<ProfileModel>();
            result.SetResult(new ProfileModel() 
            {
                Id = 1,
                Name = "Artem Ovechko",
                Nickname = "AJ",
                ImageUrl = "https://1drv.ms/u/s!Au2NFUrUjmCoi6ZofLY4zWDRh7aPrA",
                Race = 1,
                Rank = 1,
                GoldCoins = 999,
                Email = "ovechko.a@dbbest.com"
            });
            await _interopService.SetLocalStorageItem("auth", "OK");

            LoggedIn?.Invoke(this, new EventArgs());

            return await result.Task;
        }

        public Task<ProfileModel> UpdateProfile(ProfileUpdateModel model)
        {
            TaskCompletionSource<ProfileModel> result = new TaskCompletionSource<ProfileModel>();
            result.SetResult(new ProfileModel() 
            {
                Id = 1,
                Name = "Artem Ovechko",
                Nickname = "AJ",
                ImageUrl = "https://1drv.ms/u/s!Au2NFUrUjmCoi6ZofLY4zWDRh7aPrA",
                Race = 1,
                Rank = 1,
                GoldCoins = 999,
                Email = "ovechko.a@dbbest.com"
            });

            return result.Task;
        }
    }
}