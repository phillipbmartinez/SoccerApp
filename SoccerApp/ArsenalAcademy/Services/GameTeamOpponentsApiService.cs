using ArsenalAcademy.Models;

namespace ArsenalAcademy.Services
{
    public class GameTeamOpponentsApiService : IGameTeamOpponentsApiService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public GameTeamOpponentsApiService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<List<ViewGameTeamOpponentViewModel>> GetGames()
        {
            HttpClient httpClient = httpClientFactory.CreateClient("SoccerAppApi");
            List<ViewGameTeamOpponentViewModel> games = new List<ViewGameTeamOpponentViewModel>();

            try
            {
                games = await httpClient.GetFromJsonAsync<List<ViewGameTeamOpponentViewModel>>("games");

                return games;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from ArsenalAcademy => GameTeamOpponentsApiService => GetGames: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            return games;
        }
    }
}
