using System;
using System.Threading.Tasks;

namespace K94Warriors.ScheduledTaskServices
{
    public class ScheduledTaskService : IScheduledTaskService
    {
        private readonly IScheduledTaskProvider _provider;

        public ScheduledTaskService(IScheduledTaskProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            _provider = provider;
        }

        public async Task<bool> RunTaskForKey(string taskKey)
        {
            var task = _provider.GetTask(taskKey);
            if (task != null)
                return await task.Run();
            return await new Task<bool>(() => false);
        }
    }
}