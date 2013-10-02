namespace K94Warriors.ScheduledTaskServices
{
    public interface IScheduledTaskFactory
    {
        IScheduledTask GetTask(string key);
    }
}