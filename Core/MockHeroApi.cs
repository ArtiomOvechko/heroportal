using System;
using System.Threading.Tasks;
using static Core.IHeroApi;

namespace Core
{
    class MockHeroApi : IHeroApi
    {
        readonly IInteropService _interopService;
        readonly ILogger _logger;

        public event EventHandler LoggedOut;

        public event LoginEventHandler LoggedIn;

        public MockHeroApi(
            IInteropService interopService,
            ILogger logger) 
        {
            _interopService = interopService;
            _logger = logger;
        }

        public async Task<bool> IsLoggedIn() 
        {
            string authData = await _interopService.GetLocalStorageItem("auth");
            await _logger.Info($"auth data internals: {authData}");

            return !string.IsNullOrEmpty(authData) && !string.Equals(authData, "null");
        }

        public async Task<bool> LogOut() 
        {
            await _logger.Info("Logging out...");

            TaskCompletionSource<bool> result = new TaskCompletionSource<bool>();

            result.SetResult(true);

            await _logger.Info("Set auth to null");
            await _interopService.SetLocalStorageItem("auth", null);

            LoggedOut?.Invoke(this, new EventArgs());

            return await result.Task;
        }

        public Task<string[]> GetAvatars()
        {
            TaskCompletionSource<string[]> source = new TaskCompletionSource<string[]>();
            
            source.SetResult(new string[] 
            {
                "https://9hdcgw.db.files.1drv.com/y4pwvj9th9NMcncsHQjiJ0qnJQjOOEGVpDoQqTROVBxlIGzu8dv1F7sIlXln7NQhAR63XdHxDrKwNsr7KxxAnWGy7evH0_o_MCLcPY_QraN-Z4jnMTC541fTiw5Ik26tzyCSdr7WpKDpbsJ_QGLwAx9uSMBK0ZfkuM1cZBUFJP2dsLorpFqfoXiocOBzppFUjnDYdNspIdoUsIPBXKmakGV_g/Dwarf.png?psid=1",
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
                ImageUrl = "https://9hdcgw.db.files.1drv.com/y4pwvj9th9NMcncsHQjiJ0qnJQjOOEGVpDoQqTROVBxlIGzu8dv1F7sIlXln7NQhAR63XdHxDrKwNsr7KxxAnWGy7evH0_o_MCLcPY_QraN-Z4jnMTC541fTiw5Ik26tzyCSdr7WpKDpbsJ_QGLwAx9uSMBK0ZfkuM1cZBUFJP2dsLorpFqfoXiocOBzppFUjnDYdNspIdoUsIPBXKmakGV_g/Dwarf.png?psid=1",
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

            return result.Task;        
        }

        public async Task<ProfileModel> LogIn(LoginModel model)
        {
            Task loggingTask = _logger.Info("Logging in...");

            await Task.Delay(2000);

            return await loggingTask.ContinueWith(async (task) => 
            {
                TaskCompletionSource<ProfileModel> result = new TaskCompletionSource<ProfileModel>();
                ProfileModel response = new ProfileModel()
                {
                    Id = 1,
                    Name = "Artem Ovechko",
                    Nickname = "AJ",
                    ImageUrl = "https://1drv.ms/u/s!Au2NFUrUjmCoi6ZofLY4zWDRh7aPrA",
                    Race = 1,
                    Rank = 1,
                    GoldCoins = 999,
                    Email = "ovechko.a@dbbest.com"
                };
                result.SetResult(response);
                await _logger.Info("Set auth to OK");
                await _interopService.SetLocalStorageItem("auth", "OK");

                LoggedIn?.Invoke(this, response);

                return await result.Task;
            }).Unwrap();
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