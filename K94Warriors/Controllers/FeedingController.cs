using K94Warriors.Data.Contracts;
using K94Warriors.Models;
using System.Linq;
using System.Web.Mvc;

namespace K94Warriors.Controllers
{
    public partial class FeedingController : BaseController
    {
        private readonly IRepository<DogProfile> _dogProfileRepo;
        private readonly IRepository<DogFeedingEntry> _dogFeedingRepo;

        public FeedingController(IRepository<DogProfile> dogProfileRepo, IRepository<DogFeedingEntry> dogFeedingRepo)
        {
            _dogProfileRepo = dogProfileRepo;
            _dogFeedingRepo = dogFeedingRepo;
        }

        public virtual ActionResult Index(DogProfile dog)
        {
            if (dog == null)
                return HttpNotFound();

            SetDogViewBag(dog);

            var feedingEntries = _dogFeedingRepo.Where(i => i.DogProfileID == dog.ProfileID).ToList();

            return View(feedingEntries);
        }

        [HttpGet]
        public virtual ActionResult Create(DogProfile dog)
        {
            if (dog == null)
                return HttpNotFound();

            SetDogViewBag(dog);

            return View();
        }

        [HttpPost]
        public virtual ActionResult Create(DogFeedingEntry model)
        {
            var dog = _dogProfileRepo.GetById(model.DogProfileID);

            if (dog == null)
                return HttpNotFound();

            if (!ModelState.IsValid)
            {
                SetDogViewBag(dog);
                return View(model);
            }

            _dogFeedingRepo.Insert(model);

            return RedirectToAction("Index", new { dog = model.DogProfileID });
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            var model = _dogFeedingRepo.GetById(id);

            if (model == null)
                return HttpNotFound();

            var dog = _dogProfileRepo.GetById(model.DogProfileID);

            if (dog == null)
                return HttpNotFound();

            SetDogViewBag(dog);

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Edit(DogFeedingEntry model)
        {
            if (model == null)
                return HttpNotFound();

            var dog = _dogProfileRepo.GetById(model.DogProfileID);

            if (dog == null)
                return HttpNotFound();

            if (!ModelState.IsValid)
            {
                SetDogViewBag(dog);
                return View(model);
            }

            _dogFeedingRepo.Update(model);

            return RedirectToAction("Index", new { dog = dog.ProfileID });
        }

        public virtual ActionResult Delete(int id)
        {
            var feeding = _dogFeedingRepo.GetById(id);

            if (feeding == null)
                return HttpNotFound();

            _dogFeedingRepo.Delete(id);

            return RedirectToAction("Index", new { dog = feeding.DogProfileID });
        }
    }
}
