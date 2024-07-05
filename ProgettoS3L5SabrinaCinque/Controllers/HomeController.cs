using Microsoft.AspNetCore.Mvc;
using ProgettoS3L5SabrinaCinque.Models;
using ProgettoS3L5SabrinaCinque.Services;
using System.Diagnostics;
using System.Linq;

namespace ProgettoS3L5SabrinaCinque.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVerbaleService _verbaleService;
        private readonly IAnagraficaService _anagraficaService;

        public HomeController(ILogger<HomeController> logger, IVerbaleService verbaleService, IAnagraficaService anagraficaService)
        {
            _logger = logger;
            _verbaleService = verbaleService;
            _anagraficaService = anagraficaService;
        }

        public IActionResult Index()
        {
            var verbali = _verbaleService.GetAllVerbali();
            return View(verbali);
        }

        public IActionResult Anagrafiche()
        {
            var anagrafiche = _anagraficaService.GetAllAnagrafiche().OrderBy(a => a.Cognome).ThenBy(a => a.Nome);
            return View(anagrafiche);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
