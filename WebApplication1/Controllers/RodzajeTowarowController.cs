using Data.Services;
using Data;
using Domain.Models;
using Domain.ViewModels.Marki;
using Domain.ViewModels.Owners;
using Microsoft.AspNetCore.Mvc;
using Domain.ViewModels.RodzajeTowarow;
using Domain.Models.Enums;

namespace WebApplication1.Controllers
{
    public class RodzajeTowarowController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;

        public RodzajeTowarowController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index(RodzajeTowarowViewModel model)
        {
            NI.Navigation = Navigation.RodzajeTowarowIndex;
            var rodzajeTowarow = await _unityOfWork.RodzajeTowarowRepository.GetAll();
            return View(new RodzajeTowarowViewModel()
            {
                RodzajeTowarow = rodzajeTowarow,
                Paginator = Paginator<RodzajTowaru>.CreateAsync(rodzajeTowarow, model.PageIndex, model.PageSize),
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
        public async Task<IActionResult> Index(string s, RodzajeTowarowViewModel model)
        {
            NI.Navigation = Navigation.RodzajeTowarowIndex;
            var rodzajeTowarow = await _unityOfWork.RodzajeTowarowRepository.GetAll();


            // Wyszukiwanie
            if (!string.IsNullOrEmpty(model.q))
                rodzajeTowarow = rodzajeTowarow.Where(w =>
                    w.Name.Contains(model.q, StringComparison.OrdinalIgnoreCase)
                    ).ToList();


            // Sortowanie 
            switch (model.SortowanieOption)
            {
                case "Nazwa A-Z":
                    rodzajeTowarow = rodzajeTowarow.OrderBy(o => o.Name).ToList();
                    break;

                case "Nazwa Z-A":
                    rodzajeTowarow = rodzajeTowarow.OrderByDescending(o => o.Name).ToList();
                    break;
            }

            model.RodzajeTowarow = rodzajeTowarow;
            model.Paginator = Paginator<RodzajTowaru>.CreateAsync(rodzajeTowarow, model.PageIndex, model.PageSize);
            return View(model);
        }



        [HttpGet]
        public IActionResult Create()
        {
            NI.Navigation = Navigation.RodzajeTowarowCreate;
            return View(new RodzajTowaruViewModel() { Result = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RodzajTowaruViewModel model)
        {
            NI.Navigation = Navigation.RodzajeTowarowCreate;
            if (ModelState.IsValid)
            {
                if (!(await _unityOfWork.RodzajeTowarowRepository.Create(model)).Success)
                    return RedirectToAction("Index", "RodzajeTowarow");
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            NI.Navigation = Navigation.RodzajeTowarowEdit;

            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            var rodzajTowaru = await _unityOfWork.RodzajeTowarowRepository.Get(id);

            if (rodzajTowaru == null)
                return View("NotFound");

            return View(new RodzajTowaruViewModel()
            {
                RodzajTowaru = rodzajTowaru
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RodzajTowaruViewModel model)
        {
            NI.Navigation = Navigation.RodzajeTowarowEdit;

            if (string.IsNullOrEmpty(model.RodzajTowaru.RodzajTowaruId))
                return View("NotFound");

            if (ModelState.IsValid)
            {
                if (!(await _unityOfWork.RodzajeTowarowRepository.Update(model)).Success)
                    return RedirectToAction("Index", "RodzajeTowarow");
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            var rodzajTowaru = await _unityOfWork.RodzajeTowarowRepository.Get(id);

            if (rodzajTowaru == null)
                return View("NotFound");

            return View(rodzajTowaru);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            if (await _unityOfWork.RodzajeTowarowRepository.Delete(id))
                return RedirectToAction("Index", "RodzajeTowarow");

            return View("NotFound");
        }
    }
}
