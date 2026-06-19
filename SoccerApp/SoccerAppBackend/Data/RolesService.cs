namespace SoccerAppBackend.Data
{
    public class RolesService : IRolesService
    {
        private readonly IDatabaseService databaseService;

        public RolesService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }
    }
}
