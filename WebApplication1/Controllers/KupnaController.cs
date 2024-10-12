using Data;
using Data.Services;
using Domain.Models;
using Domain.Models.Enums;
using Domain.ViewModels.Kupna;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class KupnaController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;

        public KupnaController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index(KupnaViewModel model)
        {
            NI.Navigation = Navigation.KupnaIndex;
            var kupna = await _unityOfWork.KupnaRepository.GetAll();
            return View(new KupnaViewModel()
            {
                Kupna = kupna,
                Paginator = Paginator<Kupno>.CreateAsync(kupna, model.PageIndex, model.PageSize),
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
        public async Task<IActionResult> Index(string s, KupnaViewModel model)
        {
            NI.Navigation = Navigation.KupnaIndex;
            var kupna = await _unityOfWork.KupnaRepository.GetAll();

            // Wyszukiwanie
            if (!string.IsNullOrEmpty(model.q))
                kupna = kupna.Where(w =>
                    w.Towar.Nazwa.Contains(model.q, StringComparison.OrdinalIgnoreCase) ||
                    w.Client.DaneOsobowe.Nazwisko.Contains(model.q, StringComparison.OrdinalIgnoreCase) ||
                    w.DataZakupu.Contains(model.q, StringComparison.OrdinalIgnoreCase)
                ).ToList();


            // Sortowanie 
            switch (model.SortowanieOption)
            {
                case "Nazwa towaru A-Z":
                    kupna = kupna.OrderBy(o => o.Towar.Nazwa).ToList();
                    break;

                case "Nazwa towaru Z-A":
                    kupna = kupna.OrderByDescending(o => o.Towar.Nazwa).ToList();
                    break;

                case "Nazwa klienta A-Z":
                    kupna = kupna.OrderBy(o => o.Client.DaneOsobowe.Nazwisko).ToList();
                    break;

                case "Nazwa klienta Z-A":
                    kupna = kupna.OrderByDescending(o => o.Client.DaneOsobowe.Nazwisko).ToList();
                    break;

                case "Data zakupu rosnąco":
                    kupna = kupna.OrderBy(o => o.DataZakupu).ToList();
                    break;

                case "Data zakupu malejąco":
                    kupna = kupna.OrderByDescending(o => o.DataZakupu).ToList();
                    break;
            }

            model.Kupna = kupna;
            model.Paginator = Paginator<Kupno>.CreateAsync(kupna, model.PageIndex, model.PageSize);
            return View(model);
        }



        [HttpGet]
        public IActionResult Create()
        {
            NI.Navigation = Navigation.RolesCreate;
            return View(new KupnoViewModel() { Result = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KupnoViewModel model)
        {
            NI.Navigation = Navigation.KupnaCreate;
            if (ModelState.IsValid)
            {
                if (!(await _unityOfWork.KupnaRepository.Create(model)).Success)
                    return RedirectToAction("Index", "Kupna");
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string kupnoId)
        {
            NI.Navigation = Navigation.KupnaEdit;

            if (string.IsNullOrEmpty(kupnoId))
                return View("NotFound");

            var kupno = await _unityOfWork.KupnaRepository.Get(kupnoId);

            if (kupno == null)
                return View("NotFound");

            return View(new KupnoViewModel()
            {
                Kupno = kupno
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(KupnoViewModel model)
        {
            NI.Navigation = Navigation.KupnaEdit;

            if (string.IsNullOrEmpty(model.Kupno.TowarId))
                return View("NotFound");

            if (ModelState.IsValid)
            {
                if (!(await _unityOfWork.KupnaRepository.Update(model)).Success)
                    return RedirectToAction("Index", "Kupna");
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            var kupno = await _unityOfWork.KupnaRepository.Get(id);

            if (kupno == null)
                return View("NotFound");

            return View(kupno);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            if (await _unityOfWork.KupnaRepository.Delete(id))
                return RedirectToAction("Index", "Kupna");

            return View("NotFound");
        }
    }
}
