using Microsoft.AspNetCore.Mvc;
using MvcCosmosDbSeries.Models;
using MvcCosmosDbSeries.Services;

namespace MvcCosmosDbSeries.Controllers
{
    public class SeriesController : Controller
    {
        private ServiceCosmosDb service;

        public SeriesController(ServiceCosmosDb service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string accion)
        {
            await this.service.CreateDatabaseAsync();
            ViewData["MENSAJE"] = "Recursos creados en Azure Cosmos Db";
            return View();
        }

        public async Task<IActionResult> Series()
        {
            List<Serie> series =
                await this.service.GetSeriesAsync();
            return View(series);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Serie ser)
        {
            await this.service.InsertSerieAsync(ser);
            return RedirectToAction("Series");
        }

        public async Task<IActionResult> Details(string id)
        {
            Serie ser =
                await this.service.FindSerieAsync(id);
            return View(ser);
        }

        public async Task<IActionResult> Edit(string id)
        {
            Serie ser =
                await this.service.FindSerieAsync(id);
            return View(ser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Serie ser)
        {
            await this.service.UpdateSerieAsync(ser);
            return RedirectToAction("Series");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.service.DeleteSerieAsync(id);
            return RedirectToAction("Series");
        }
    }
}
