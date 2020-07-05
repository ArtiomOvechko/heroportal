using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using static Core.IHeroApi;

namespace Core
{
    class HeroApi : IHeroApi
    {
        readonly IInteropService _interopService;
        readonly ILogger _logger;
        readonly HttpClient _httpClient;

        const string BaseUrl = "http://ec2-52-29-137-234.eu-central-1.compute.amazonaws.com";

        public event EventHandler LoggedOut;

        public event LoginEventHandler LoggedIn;

        public HeroApi(
            IInteropService interopService,
            ILogger logger,
            HttpClient httpClient)
        {
            _interopService = interopService;
            _logger = logger;
            _httpClient = httpClient;
        }

        public Task<string[]> GetAvatars()
        {
            throw new NotImplementedException();
        }

        public Task<ProfileModel> GetProfile()
        {
            throw new NotImplementedException();
        }

        public Task<NameModel[]> GetRaces()
        {
            throw new NotImplementedException();
        }

        public Task<NameModel[]> GetRanks()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsLoggedIn()
        {
            string authData = await _interopService.GetLocalStorageItem("auth");
            await _logger.Info($"auth data internals: {authData}");

            return !string.IsNullOrEmpty(authData) && !string.Equals(authData, "null");
        }

        public async Task<ProfileModel> LogIn(LoginModel model)
        {
            Task loggingTask = _logger.Info("Logging in...");

            return await loggingTask.ContinueWith(async (task) =>
            {
                HttpResponseMessage response = await
                    _httpClient.PostAsync($"{BaseUrl}/Profile/enter", new StringContent(Utf8Json.JsonSerializer.ToJsonString(model)));
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Stream responseStream = await response.Content.ReadAsStreamAsync();
                    ProfileModel result = await Utf8Json.JsonSerializer.DeserializeAsync<ProfileModel>(responseStream);

                    await _interopService.SetLocalStorageItem("auth", result.Id.ToString());

                    LoggedIn?.Invoke(this, result);

                    return result;
                }
                else
                {
                    throw new Exception("Login failed");
                }
            }).Unwrap();
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
    }
}
