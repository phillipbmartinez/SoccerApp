namespace SoccerAppBackend.Data
{
    public class TeamsService : ITeamsService
    {
        private readonly IDatabaseService databaseService;

        public TeamsService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }
    }
}
