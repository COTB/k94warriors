namespace K94Warriors
{
    public static class RepoResolver
    {
        public static IRepository<T> GetRepository<T>() where T : class
        {
            var dbContext = new K9DbContext("DefaultConnection");
            return new EFRepository<T>(dbContext);
        }

        public static IRepository<T> GetRepository<T>(string connectionName) where T : class
        {
            var dbContext = new K9DbContext(connectionName);
            return new EFRepository<T>(dbContext);
        }
    }
}