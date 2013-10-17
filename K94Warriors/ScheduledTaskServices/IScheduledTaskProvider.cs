namespace K94Warriors.ScheduledTaskServices
{
    public interface IScheduledTaskProvider
    {
        IScheduledTask GetTask(string key);
    }
}