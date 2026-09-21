using System.Net.Http;
using System.Net.Http.Json;
using ArsenalAcademy.Models;

namespace ArsenalAcademy.Services
{
    public class CoachesApiService : ICoachesApiService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public CoachesApiService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<List<ViewCoachViewModel>> GetAllCoaches()
        {
            HttpClient httpClient = httpClientFactory.CreateClient("SoccerAppApi");
            List<ViewCoachViewModel> coaches = new List<ViewCoachViewModel>();

            try
            {
                coaches = await httpClient.GetFromJsonAsync<List<ViewCoachViewModel>>("coaches");

                return coaches;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from ArsenalAcademy => CoachesApiService => GetAllCoaches: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            return coaches;
        }

        public async Task<ViewCoachViewModel> GetCoachById(int coachId)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("SoccerAppApi");
            ViewCoachViewModel coach = new ViewCoachViewModel();

            try
            {
                var response = await httpClient.GetAsync($"coaches/{coachId}");
                var contentResponse = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Response: {contentResponse}");

                coach = await httpClient.GetFromJsonAsync<ViewCoachViewModel>($"coaches/{coachId}");

                return coach;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from ArsenalAcademy => CoachesApiService => GetCoachById: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            return coach;
        }

        public async Task<CreateCoachViewModel> CreateCoach(CreateCoachViewModel coachToCreate)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("SoccerAppApi");
            CreateCoachViewModel newCoachRecord = new CreateCoachViewModel();
            UserViewModel userViewModel = new UserViewModel()
            {
                FirstName = coachToCreate.FirstName,
                LastName = coachToCreate.LastName,
                Email = coachToCreate.Email,
                DateOfBirth = coachToCreate.DateOfBirth,
            };

            CoachViewModel coachViewModel = new CoachViewModel()
            {
                CoachingLicense = coachToCreate.CoachingLicense,
                StartedCoachingDate = coachToCreate.StartedCoachingDate,
            };

            try
            {
                HttpResponseMessage userHttpResponse = await httpClient.PostAsJsonAsync<UserViewModel>("users", userViewModel);
                userHttpResponse.EnsureSuccessStatusCode();

                string newUserData = await userHttpResponse.Content.ReadAsStringAsync();

                HttpResponseMessage coachHttpResponse = await httpClient.PostAsJsonAsync<CoachViewModel>("coaches", coachViewModel);
                coachHttpResponse.EnsureSuccessStatusCode();

                string newCoachData = await coachHttpResponse.Content.ReadAsStringAsync();

                // Need to update the new coach record to have the new userid, so need a put action

                if (!string.IsNullOrWhiteSpace(newUserData) && !string.IsNullOrWhiteSpace(newCoachData))
                {
                    if (newUserData.Contains("userId") && newCoachData.Contains("coachId"))
                    {
                        string userId = newUserData.Split(",")[0].Split(":")[1].Trim();
                        string coachId = newCoachData.Split(",")[0].Split(":")[1].Trim();

                        HttpResponseMessage response = await httpClient.PutAsJsonAsync<CreateCoachViewModel>($"coaches/{coachId}/{userId}", newCoachRecord);

                        newCoachRecord = await response.Content.ReadFromJsonAsync<CreateCoachViewModel>();
                    }
                }

                return newCoachRecord;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from ArsenalAcademy => CoachesApiService => CreateCoach: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            return newCoachRecord;
        }
    }
}
