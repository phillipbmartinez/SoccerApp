namespace SoccerAppBackend.Data
{
    public class UsersService : IUsersService
    {
        private readonly IDatabaseService databaseSerivce;

        public UsersService(IDatabaseService databaseSerivce)
        {
            this.databaseSerivce = databaseSerivce;
        }
    }
}
