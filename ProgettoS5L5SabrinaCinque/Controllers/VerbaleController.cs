using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgettoS5L5SabrinaCinque.DAO;
using ProgettoS5L5SabrinaCinque.Models;

namespace ProgettoS5L5SabrinaCinque.Controllers
{
    [Authorize(Policy = "ComandantePolicy")]

    public class VerbaleController : Controller
    {
        private readonly IVerbaleDao _verbaleDao;

        public VerbaleController(IVerbaleDao verbaleDao)
        {
            _verbaleDao = verbaleDao;
        }

        public IActionResult Index()
        {
            var verbali = _verbaleDao.GetAll();
            return View(verbali);
        }

        public IActionResult Details(int id)
        {
            var verbale = _verbaleDao.GetById(id);
            if (verbale == null)
            {
                return NotFound();
            }
            return View(verbale);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Verbale verbale)
        {
            if (ModelState.IsValid)
            {
                _verbaleDao.Add(verbale);
                return RedirectToAction(nameof(Index));
            }
            return View(verbale);
        }

        public IActionResult Edit(int id)
        {
            var verbale = _verbaleDao.GetById(id);
            if (verbale == null)
            {
                return NotFound();
            }
            return View(verbale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Verbale verbale)
        {
            if (id != verbale.IdVerbale)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _verbaleDao.Update(verbale);
                return RedirectToAction(nameof(Index));
            }
            return View(verbale);
        }
    }
}
