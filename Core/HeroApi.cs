using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Utf8Json;
using static Core.IHeroApi;
using Blazored;
using System.Collections.Generic;

namespace Core
{
    class HeroApi : IHeroApi
    {
        readonly IInteropService _interopService;
        readonly ILogger _logger;
        readonly HttpClient _httpClient;
        readonly Blazored.LocalStorage.ILocalStorageService _LocalStore;

        const string BaseUrl = "http://192.168.85.151:8888/api";

        public event EventHandler LoggedOut;

        public event LoginEventHandler LoggedIn;

        private Dictionary<string, string> users = new Dictionary<string, string>() 
        { 
            {"seliukov.d@dbbest.com", "2" },
            {"seliukova.s@dbbest.com", "3" },
            {"ovechko.a@dbbest.com",  "4"},
            {"lavrov.a@dbbest.com","6" },
            {"karpenko.k@dbbest.com" ,"7"},
            {"garbuz.i@dbbest.com","8"},
            {"zhernovaya.e@dbbest.com","9"},
            {"miahkov.a@dbbest.com","10"},
            {"koryagin.a@dbbest.com","11"},
            {"kluban.a@dbbest.com","12"},
            {"arkhanhelskyi.d@dbbest.com","13"},
            {"polushyna.e@dbbest.com","14"},
            {"bohdan.y@dbbest.com","15"},
            {"andrieiev.s@dbbest.com","16"}
        };

        public HeroApi(
            IInteropService interopService,
            ILogger logger,
            HttpClient httpClient,
            Blazored.LocalStorage.ILocalStorageService LocalStore)
        {
            _interopService = interopService;
            _logger = logger;
            _httpClient = httpClient;
            _LocalStore = LocalStore;
        }

        public Task<string[]> GetAvatars()
        {
            throw new NotImplementedException();
        }

        public async Task<ProfileModel> GetProfile(int? profileId = null)
        {
            Task loggingTask = _logger.Info("Get Profile");

            return await loggingTask.ContinueWith(async (task) =>
            {
                int id = 0;
                if (!profileId.HasValue || profileId == 0)
                {
                    var localStorageValues = await _interopService.GetLocalStorageItem("auth");
                    if (localStorageValues == null)
                    {
                        localStorageValues = "0";
                    }
                    id = int.Parse(localStorageValues);
                }
                else
                {
                    id = profileId.Value;
                }

                HttpResponseMessage response = await
                    _httpClient.GetAsync($"{BaseUrl}/Profile/{id}");
                await _logger.Info(response.StatusCode.ToString());
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Stream responseStream = await response.Content.ReadAsStreamAsync();
                    ProfileModel result = await Utf8Json.JsonSerializer.DeserializeAsync<ProfileModel>(responseStream);
                    result.nextlevel = result.level + 1;
                    return result;
                }
                else
                {
                    throw new Exception("Get Profile failed");
                }

            }).Unwrap();
        }

        public async Task<NameModel[]> GetRaces()
        {
            Task loggingTask = _logger.Info("Get races");

            return await loggingTask.ContinueWith(async (task) =>
            {
                HttpResponseMessage response = await
                    _httpClient.GetAsync($"{BaseUrl}/Profile/races");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Stream responseStream = await response.Content.ReadAsStreamAsync();
                    NameModel[] result = await Utf8Json.JsonSerializer.DeserializeAsync<NameModel[]>(responseStream);

                    return result;
                }
                else
                {
                    throw new Exception("Get races failed");
                }
            }).Unwrap();
        }

        public async Task<NameModel[]> GetRanks()
        {
            Task loggingTask = _logger.Info("Get ranks");

            return await loggingTask.ContinueWith(async (task) =>
            {
                HttpResponseMessage response = await
                    _httpClient.GetAsync($"{BaseUrl}/Profile/ranks");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Stream responseStream = await response.Content.ReadAsStreamAsync();
                    NameModel[] result = await Utf8Json.JsonSerializer.DeserializeAsync<NameModel[]>(responseStream);

                    return result;
                }
                else
                {
                    throw new Exception("Get ranks failed");
                }
            }).Unwrap();
        }

        public async Task<TeamModel[]> GetTeam()
        {
            Task loggingTask = _logger.Info("Get team");

            return await loggingTask.ContinueWith(async (task) =>
            {
                HttpResponseMessage response = await
                    _httpClient.GetAsync($"{BaseUrl}/Team");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Stream responseStream = await response.Content.ReadAsStreamAsync();
                    TeamModel[] result = await Utf8Json.JsonSerializer.DeserializeAsync<TeamModel[]>(responseStream);

                    return result;
                }
                else
                {
                    throw new Exception("Get team failed");
                }
            }).Unwrap();
        }

        public async Task<bool> IsLoggedIn()
        {
            string authData = await _interopService.GetLocalStorageItem("auth");
            await _logger.Info($"auth data internals: {authData}");

            return !string.IsNullOrEmpty(authData) && !string.Equals(authData, "null");
        }

        public async Task LogIn(LoginModel model)
        {
            string id = "7";
            users.TryGetValue(model.Email, out id);
            await _LocalStore.SetItemAsync("auth", id);
            LoggedIn?.Invoke(this, id);
            /*Task loggingTask = _logger.Info("Logging in...");

            return await loggingTask.ContinueWith(async (task) =>
            {
                HttpResponseMessage response = await
                    _httpClient.GetAsync($"{BaseUrl}/Profile/byemail/{model.Email}");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Stream responseStream = await response.Content.ReadAsStreamAsync();
                    ProfileModel result = await Utf8Json.JsonSerializer.DeserializeAsync<ProfileModel>(responseStream);
                    await _LocalStore.SetItemAsync("auth", result.id.ToString());
                    //await _interopService.SetLocalStorageItem("auth", result.id.ToString());

                    LoggedIn?.Invoke(this, result);

                    return result;
                }
                else
                {
                    throw new Exception("Login failed");
                }
            }).Unwrap();*/
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

        public Task<ProfileModel> UpdateProfile(ProfileUpdateModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<SkillModel[]> GetSkills()
        {
            Task loggingTask = _logger.Info("Get skills");

            return await loggingTask.ContinueWith(async (task) =>
            {
                HttpResponseMessage response = await
                    _httpClient.GetAsync($"{BaseUrl}/LeaderBoard/skills");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Stream responseStream = await response.Content.ReadAsStreamAsync();
                    SkillModel[] result = await Utf8Json.JsonSerializer.DeserializeAsync<SkillModel[]>(responseStream);

                    return result;
                }
                else
                {
                    throw new Exception("Get skills failed");
                }
            }).Unwrap();
        }

        public async Task<LeaderModel[]> GetTopLeaders()
        {
            Task loggingTask = _logger.Info("Get skills");

            return await loggingTask.ContinueWith(async (task) =>
            {
                HttpResponseMessage response = await
                    _httpClient.GetAsync($"{BaseUrl}/LeaderBoard/top");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Stream responseStream = await response.Content.ReadAsStreamAsync();
                    LeaderModel[] result = await Utf8Json.JsonSerializer.DeserializeAsync<LeaderModel[]>(responseStream);

                    return result;
                }
                else
                {
                    throw new Exception("Get skills failed");
                }
            }).Unwrap();
        }

        public async Task<BadgeModel[]> GetBadges()
        {
            Task loggingTask = _logger.Info("Get badges");
            return await loggingTask.ContinueWith(async (task) =>
            {
                HttpResponseMessage response = await
                    _httpClient.GetAsync($"{BaseUrl}/Badges");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Stream responseStream = await response.Content.ReadAsStreamAsync();
                    BadgeModel[] result = await Utf8Json.JsonSerializer.DeserializeAsync<BadgeModel[]>(responseStream);

                    return result;
                }
                else
                {
                    throw new Exception("Get badges failed");
                }
            }).Unwrap();
        }

        public async Task<AchievementModel[]> GetAchievements()
        {
            Task loggingTask = _logger.Info("Get achievements");
            return await loggingTask.ContinueWith(async (task) =>
            {
                HttpResponseMessage response = await
                    _httpClient.GetAsync($"{BaseUrl}/Achievements");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Stream responseStream = await response.Content.ReadAsStreamAsync();
                    AchievementModel[] result = await Utf8Json.JsonSerializer.DeserializeAsync<AchievementModel[]>(responseStream);

                    return result;
                }
                else
                {
                    throw new Exception("Get achievements failed");
                }
            }).Unwrap();
        }
    }
}
