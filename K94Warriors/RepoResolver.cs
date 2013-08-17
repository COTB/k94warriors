namespace K94Warriors
{
    public static class RepoResolver
    {
        public static IRepository<T> GetRepository<T>() where T : class
        {
            var dbContext = new K9DbContext("K9");
            return new EFRepository<T>(dbContext);
        }
    }
}