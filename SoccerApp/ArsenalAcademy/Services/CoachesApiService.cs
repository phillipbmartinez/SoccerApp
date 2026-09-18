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

                HttpResponseMessage coachHttpResponse = await httpClient.PostAsJsonAsync<CoachViewModel>("coaches", coachViewModel);
                coachHttpResponse.EnsureSuccessStatusCode();

                if (coachHttpResponse.Content != null && coachHttpResponse.Content != null)
                {
                    newCoachRecord = await httpClient.GetFromJsonAsync<CreateCoachViewModel>("coaches");
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
