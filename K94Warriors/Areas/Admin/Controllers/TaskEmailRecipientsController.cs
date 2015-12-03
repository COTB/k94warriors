using System.Linq;
using System.Web.Mvc;
using K94Warriors.Data.Contracts;
using K94Warriors.Filters;
using K94Warriors.Models;

namespace K94Warriors.Areas.Admin.Controllers
{
    [K9Authorize(Roles = "Admin")]
    public partial class TaskEmailRecipientsController : Controller
    {
        private readonly IRepository<TaskEmailRecipient> _taskTypesRepo;

        public TaskEmailRecipientsController(IRepository<TaskEmailRecipient> taskTypesRepo)
        {
            _taskTypesRepo = taskTypesRepo;
        }
        
        public virtual ActionResult Index()
        {
            var model = _taskTypesRepo.GetAll().OrderBy(i => i.TaskType);

            return View(model);
        }

        public virtual ActionResult Create() => View();

        [HttpPost]
        public virtual ActionResult Create(TaskEmailRecipient model)
        {
            var existingEmail = _taskTypesRepo
                .Where(i => i.EmailAddress == model.EmailAddress)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(model.EmailAddress) && existingEmail != null)
                ModelState.AddModelError("EmailAddress", "Email address already exists, please try another.");

            if (!ModelState.IsValid)
                return View(model);

            _taskTypesRepo.Insert(model);

            return RedirectToAction("Index");
        }

        public virtual ActionResult Edit(int id)
        {
            var model = _taskTypesRepo.GetById(id);

            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Edit(TaskEmailRecipient model)
        {
            var existingEmail = _taskTypesRepo
                .Where(i => i.EmailAddress == model.EmailAddress)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(model.EmailAddress) && existingEmail != null)
                ModelState.AddModelError("EmailAddress", "Email address already exists, please try another.");

            if (!ModelState.IsValid)
                return View(model);

            _taskTypesRepo.Update(model);

            return RedirectToAction("Index");
        }

        public virtual ActionResult Delete(int id)
        {
            var model = _taskTypesRepo.GetById(id);

            if (model == null)
                return HttpNotFound();

            _taskTypesRepo.Delete(model);

            return RedirectToAction("Index");
        }
    }
}