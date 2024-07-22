using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgettoS5L5SabrinaCinque.DAO;
using ProgettoS5L5SabrinaCinque.Models;

namespace ProgettoS5L5SabrinaCinque.Controllers
{
    [Authorize(Policy = "ComandantePolicy")]

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
