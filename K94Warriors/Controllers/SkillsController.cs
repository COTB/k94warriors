using System;
using System.Web.Mvc;
using K94Warriors.Data;
using K94Warriors.Models;

namespace K94Warriors.Controllers
{
    public class SkillsController : Controller
    {
        private readonly IRepository<DogSkill> _dogSkillRepo;
 
        public SkillsController(IRepository<DogSkill> dogSkillRepo)
        {
            if (dogSkillRepo == null)
                throw new ArgumentNullException("dogSkillRepo");
            _dogSkillRepo = dogSkillRepo;
        }

        //
        // GET: /Skills/

        public virtual ActionResult Index()
        {
            var skills = _dogSkillRepo.GetAll();
            return View(skills);
        }

        [HttpPost]
        public ActionResult CreateOrUpdateSkill(DogSkill dogSkill)
        {
            _dogSkillRepo.Update(dogSkill);
            return RedirectToAction("GetSkill", "Skills");
        }

        public ActionResult GetSkill(int id)
        {
            var skill = _dogSkillRepo.GetById(id);
            return View(skill);
        }

        [HttpPost]
        public ActionResult DeleteSkill(int id)
        {
            _dogSkillRepo.Delete(id);
            return RedirectToAction("Index", "Skills");
        }
    }
}