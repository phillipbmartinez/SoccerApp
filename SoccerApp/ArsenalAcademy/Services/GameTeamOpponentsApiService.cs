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

        public async Task<ViewGameTeamOpponentViewModel> GetGameById(int gameId)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("SoccerAppApi");
            ViewGameTeamOpponentViewModel game = new ViewGameTeamOpponentViewModel();

            try
            {
                game = await httpClient.GetFromJsonAsync<ViewGameTeamOpponentViewModel>($"games/{gameId}");

                return game;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from ArsenalAcademy => GameTeamOpponentsApiService => GetGamesByTeamId: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            return game;
        }

        public async Task<List<ViewGameTeamOpponentViewModel>> GetUpcomingGames()
        {
            HttpClient httpClient = httpClientFactory.CreateClient("SoccerAppApi");
            List<ViewGameTeamOpponentViewModel> upcomingGames = new List<ViewGameTeamOpponentViewModel>();

            try
            {
                upcomingGames = await httpClient.GetFromJsonAsync<List<ViewGameTeamOpponentViewModel>>("games/upcoming");

                return upcomingGames;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from ArsenalAcademy => GameTeamOpponentsApiService => GetUpcomingGames: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            return upcomingGames;
        }

        public async Task<List<ViewGameTeamOpponentViewModel>> GetPreviousGames()
        {
            HttpClient httpClient = httpClientFactory.CreateClient("SoccerAppApi");
            List<ViewGameTeamOpponentViewModel> previousGames = new List<ViewGameTeamOpponentViewModel>();

            try
            {
                previousGames = await httpClient.GetFromJsonAsync<List<ViewGameTeamOpponentViewModel>>("games/previous");

                return previousGames;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from ArsenalAcademy => GameTeamOpponentsApiService => GetPreviousGames: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            return previousGames;
        }

        public async Task<List<ViewGameTeamOpponentViewModel>> GetGamesByTeamId(int teamId)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("SoccerAppApi");
            List<ViewGameTeamOpponentViewModel> teamGames = new List<ViewGameTeamOpponentViewModel>();

            try
            {
                teamGames = await httpClient.GetFromJsonAsync<List<ViewGameTeamOpponentViewModel>>($"games/team/{teamId}");

                return teamGames;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from ArsenalAcademy => GameTeamOpponentsApiService => GetGamesByTeamId: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            return teamGames;
        }
    }
}
