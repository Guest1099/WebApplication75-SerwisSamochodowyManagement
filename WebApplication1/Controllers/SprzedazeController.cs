using Data;
using Data.Services;
using Domain.Models;
using Domain.Models.Enums;
using Domain.ViewModels.Sprzedaze;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class SprzedazeController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;

        public SprzedazeController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index(SprzedazeViewModel model)
        {
            NI.Navigation = Navigation.SprzedazeIndex;
            var sprzedaze = await _unityOfWork.SprzedazeRepository.GetAll();
            return View(new SprzedazeViewModel()
            {
                Sprzedaze = sprzedaze,
                Paginator = Paginator<Sprzedaz>.CreateAsync(sprzedaze, model.PageIndex, model.PageSize),
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                Start = model.Start,
                End = model.End,
                q = model.q,
                SortowanieOption = model.SortowanieOption
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string s, SprzedazeViewModel model)
        {
            NI.Navigation = Navigation.SprzedazeIndex;
            var sprzedaze = await _unityOfWork.SprzedazeRepository.GetAll();

            // Wyszukiwanie
            if (!string.IsNullOrEmpty(model.q))
                sprzedaze = sprzedaze.Where(w => 
                    w.Client.DaneOsobowe.Nazwisko.Contains(model.q, StringComparison.OrdinalIgnoreCase) ||
                    w.Client.DaneOsobowe.Firma_Nazwa.Contains(model.q, StringComparison.OrdinalIgnoreCase)
                ).ToList();


            // Sortowanie 
            switch (model.SortowanieOption)
            {
                case "Data sprzedaży rosnąco":
                    sprzedaze = sprzedaze.OrderBy(o => o.DataSprzedazy).ToList();
                    break;

                case "Data sprzedaży malejąco":
                    sprzedaze = sprzedaze.OrderByDescending(o => o.DataSprzedazy).ToList();
                    break;
            }

            model.Sprzedaze = sprzedaze;
            model.Paginator = Paginator<Sprzedaz>.CreateAsync(sprzedaze, model.PageIndex, model.PageSize);
            return View(model);
        }



        [HttpGet]
        public IActionResult Create()
        {
            NI.Navigation = Navigation.SprzedazeCreate;
            return View(new SprzedazViewModel() { Result = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SprzedazViewModel model)
        {
            NI.Navigation = Navigation.SprzedazeCreate;
            if (ModelState.IsValid)
            {
                if (!(await _unityOfWork.SprzedazeRepository.Create(model)).Success)
                    return RedirectToAction("Index", "Sprzedaze");
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            NI.Navigation = Navigation.SprzedazeEdit;

            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            var sprzedaz = await _unityOfWork.SprzedazeRepository.Get(id);

            if (sprzedaz == null)
                return View("NotFound");

            return View(new SprzedazViewModel()
            {
                Sprzedaz = sprzedaz
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SprzedazViewModel model)
        {
            NI.Navigation = Navigation.SprzedazeEdit;
            if (string.IsNullOrEmpty(model.Sprzedaz.SprzedazId))
                return View("NotFound");

            if (ModelState.IsValid)
            {
                if (!(await _unityOfWork.SprzedazeRepository.Update(model)).Success)
                    return RedirectToAction("Index", "Sprzedaze");
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            var role = await _unityOfWork.SprzedazeRepository.Get(id);

            if (role == null)
                return View("NotFound");

            return View(role);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            if (await _unityOfWork.SprzedazeRepository.Delete(id))
                return RedirectToAction("Index", "Sprzedaze");

            return View("NotFound");
        }
    }
}
