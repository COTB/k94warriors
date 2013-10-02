using System.Threading.Tasks;

namespace K94Warriors.ScheduledTaskServices
{
    public interface IScheduledTask
    {
        Task<bool> Run();
    }
}