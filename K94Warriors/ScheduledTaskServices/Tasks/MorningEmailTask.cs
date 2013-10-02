using System.Threading.Tasks;

namespace K94Warriors.ScheduledTaskServices.Tasks
{
    public class MorningEmailTask : IScheduledTask
    {
        public async Task<bool> Run()
        {
            return await new Task<bool>(() => false);
        }
    }
}