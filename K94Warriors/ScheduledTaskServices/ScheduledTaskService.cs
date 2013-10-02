using System;
using System.Threading.Tasks;

namespace K94Warriors.ScheduledTaskServices
{
    public class ScheduledTaskService : IScheduledTaskService
    {
        private readonly IScheduledTaskFactory _factory;

        public ScheduledTaskService(IScheduledTaskFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");
            _factory = factory;
        }

        public async Task<bool> RunTaskForKey(string taskKey)
        {
            var task = _factory.GetTask(taskKey);
            if (task != null)
                return await task.Run();
            return await new Task<bool>(() => false);
        }
    }
}