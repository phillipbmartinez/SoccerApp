using System.Net.Http;
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

        public async Task<List<CoachViewModel>> GetAllCoaches()
        {
            HttpClient httpClient = httpClientFactory.CreateClient("SoccerAppApi");
            List<CoachViewModel> coaches = new List<CoachViewModel>();

            try
            {
                coaches = await httpClient.GetFromJsonAsync<List<CoachViewModel>>("coaches");

                return coaches;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from ArsenalAcademy => CoachesApiService => GetAllCoaches: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            return coaches;
        }

        public async Task<CoachViewModel> GetCoachById(int coachId)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("SoccerAppApi");
            CoachViewModel coach = new CoachViewModel();

            try
            {
                coach = await httpClient.GetFromJsonAsync<CoachViewModel>($"coaches/{coachId}");

                return coach;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from ArsenalAcademy => CoachesApiService => GetAllCoaches: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            return coach;
        }
    }
}
