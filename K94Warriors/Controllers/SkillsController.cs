using System.Linq;
using System.Web.Mvc;
using K94Warriors.Models;

namespace K94Warriors.Controllers
{
    public partial class SkillsController : Controller
    {
        //
        // GET: /Skills/

        public virtual ActionResult Index()
        {
            IRepository<DogSkill> repo = RepoResolver.GetRepository<DogSkill>();
            IQueryable<DogSkill> skills = repo.GetAll();
            return View(skills);
        }

        [HttpPost]
        public ActionResult CreateOrUpdateSkill(DogSkill dogSkill)
        {
            IRepository<DogSkill> repo = RepoResolver.GetRepository<DogSkill>();
            repo.Update(dogSkill);
            return RedirectToAction("GetSkill", "Skills");
        }

        public ActionResult GetSkill(int id)
        {
            IRepository<DogSkill> repo = RepoResolver.GetRepository<DogSkill>();
            repo.GetById(id);
            return View(id);
        }

        [HttpPost]
        public ActionResult DeleteSkill(int id)
        {
            IRepository<DogSkill> repo = RepoResolver.GetRepository<DogSkill>();
            repo.Delete(id);
            return RedirectToAction("Index", "Skills");
        }
    }
}
