using ArsenalAcademy.Models;

namespace ArsenalAcademy.Services
{
    public class TeamsCoachesApiService : ITeamsCoachesApiService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public TeamsCoachesApiService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<List<ViewTeamCoachViewModel>> GetTeams()
        {
            HttpClient httpClient = httpClientFactory.CreateClient("SoccerAppApi");
            List<ViewTeamCoachViewModel> teams = new List<ViewTeamCoachViewModel>();

            try
            {
                teams = await httpClient.GetFromJsonAsync<List<ViewTeamCoachViewModel>>("teams");

                return teams;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from ArsenalAcademy => TeamsCoachesApiService => GetTeams: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            return teams;
        }
    }
}
