using Microsoft.AspNetCore.Mvc;
using ProgettoS5L5SabrinaCinque.DAO;
using ProgettoS5L5SabrinaCinque.Models;

namespace ProgettoS5L5SabrinaCinque.Controllers
{
    public class TipoViolazioneController : Controller
    {
        private readonly ITipoViolazioneDao _tipoViolazioneDao;

        public TipoViolazioneController(ITipoViolazioneDao tipoViolazioneDao)
        {
            _tipoViolazioneDao = tipoViolazioneDao;
        }

        public IActionResult Index()
        {
            var violazioni = _tipoViolazioneDao.GetAll();
            return View(violazioni);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TipoViolazione violazione)
        {
            if (ModelState.IsValid)
            {
                _tipoViolazioneDao.Add(violazione);
                return RedirectToAction(nameof(Index));
            }
            return View(violazione);
        }

        public IActionResult Edit(int id)
        {
            var violazione = _tipoViolazioneDao.GetById(id);
            if (violazione == null)
            {
                return NotFound();
            }
            return View(violazione);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TipoViolazione violazione)
        {
            if (ModelState.IsValid)
            {
                _tipoViolazioneDao.Update(violazione);
                return RedirectToAction(nameof(Index));
            }
            return View(violazione);
        }

       
    }
}
