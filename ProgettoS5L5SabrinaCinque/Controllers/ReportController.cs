using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgettoS5L5SabrinaCinque.DAO;
using ProgettoS5L5SabrinaCinque.Models;

namespace ProgettoS5L5SabrinaCinque.Controllers
{
    [Authorize(Policy = "SubordinatoPolicy")] //questo può vederlo anche il subordinato oltre che al comandante
    public class ReportController : Controller
    {

        private readonly IVerbaleDao _verbaleDao;

        public ReportController(IVerbaleDao verbaleDao)
        {
            _verbaleDao = verbaleDao;
        }

        public IActionResult Index()
        {
            var viewModel = new ReportViewModel
            {
                VerbaliByTrasgressore = _verbaleDao.GetVerbaliByTrasgressore(),
                PuntiDecurtatiByTrasgressore = _verbaleDao.GetPuntiDecurtatiByTrasgressore(),
                ViolazioniConPiuDiDieciPunti = _verbaleDao.GetViolazioniConPiuDiDieciPunti(),
                ViolazioniConImportoMaggioreDi150 = _verbaleDao.GetViolazioniConImportoMaggioreDi150()
            };

            return View(viewModel);
        }
    }
}
