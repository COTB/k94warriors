using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using K94Warriors.Data.Contracts;
using K94Warriors.Models;

namespace K94Warriors.Controllers
{
    public partial class HomeController : BaseController
    {
        private readonly IRepository<DogEvent> _dogEventRepo;

        public HomeController(IRepository<DogEvent> dogEventRepo)
        {
            _dogEventRepo = dogEventRepo;
        }

        public virtual ActionResult Index()
        {
            var events = _dogEventRepo
                .Where(f => f.IsComplete == false)
                .OrderBy(i => i.EventDate) // to show expired ones first
                .Include(i => i.DogProfile)
                .ToList();

            return View(events);
        }
    }
}