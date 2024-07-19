using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgettoS5L5SabrinaCinque.DAO;
using ProgettoS5L5SabrinaCinque.Models;

namespace ProgettoS5L5SabrinaCinque.Controllers
{
    [Authorize(Policy = "ComandantePolicy")]


    public class AnagraficaController : Controller
    {
        private readonly IAnagraficaDao _anagraficaDao;

        public AnagraficaController(IAnagraficaDao anagraficaDao)
        {
            _anagraficaDao = anagraficaDao;
        }

        public IActionResult Index()
        {
            var anagrafiche = _anagraficaDao.GetAll();
            return View(anagrafiche);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Anagrafica anagrafica)
        {
            if (ModelState.IsValid)
            {
                _anagraficaDao.Add(anagrafica);
                return RedirectToAction(nameof(Index));
            }
            return View(anagrafica);
        }

        public IActionResult Edit(int id)
        {
            var anagrafica = _anagraficaDao.GetById(id);
            if (anagrafica == null)
            {
                return NotFound();
            }
            return View(anagrafica);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Anagrafica anagrafica)
        {
            if (ModelState.IsValid)
            {
                _anagraficaDao.Update(anagrafica);
                return RedirectToAction(nameof(Index));
            }
            return View(anagrafica);
        }


    }
}
