using System.Threading.Tasks;

namespace K94Warriors.ScheduledTaskServices
{
    public interface IScheduledTaskService
    {
        Task<bool> RunTaskForKey(string taskKey);
    }
}