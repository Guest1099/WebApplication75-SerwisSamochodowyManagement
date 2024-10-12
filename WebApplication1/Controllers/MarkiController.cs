using Data.Services;
using Data;
using Domain.Models;
using Domain.ViewModels.Marki;
using Microsoft.AspNetCore.Mvc;
using Domain.Models.Enums;

namespace WebApplication1.Controllers
{
    public class MarkiController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;

        public MarkiController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index(MarkiViewModel model)
        {
            NI.Navigation = Navigation.MarkiIndex;
            var marki = await _unityOfWork.MarkiRepository.GetAll();
            return View(new MarkiViewModel()
            {
                Marki = marki,
                Paginator = Paginator<Marka>.CreateAsync(marki, model.PageIndex, model.PageSize),
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
        public async Task<IActionResult> Index(string s, MarkiViewModel model)
        {
            NI.Navigation = Navigation.MarkiIndex;
            var marki = await _unityOfWork.MarkiRepository.GetAll();

            // Wyszukiwanie
            if (!string.IsNullOrEmpty(model.q))
                marki = marki.Where(w => w.Name.Contains(model.q, StringComparison.OrdinalIgnoreCase)).ToList();


            // Sortowanie 
            switch (model.SortowanieOption)
            {
                case "Nazwa A-Z":
                    marki = marki.OrderBy(o => o.Name).ToList();
                    break;

                case "Nazwa Z-A":
                    marki = marki.OrderByDescending(o => o.Name).ToList();
                    break;
            }

            model.Marki = marki;
            model.Paginator = Paginator<Marka>.CreateAsync(marki, model.PageIndex, model.PageSize);
            return View(model);
        }



        [HttpGet]
        public IActionResult Create()
        {
            NI.Navigation = Navigation.MarkiCreate;
            return View(new MarkaViewModel() { Result = "" });
        }
        /*
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Create(MarkaViewModel model)
                {
                    NI.Navigation = Navigation.MarkiCreate;
                    if (ModelState.IsValid)
                    {
                        if ((await _unityOfWork.MarkiRepository.Create(model)).Success)
                            return RedirectToAction("Index", "Marki");
                    }

                    return View(model);
                }
        */


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MarkaViewModel model)
        {
            NI.Navigation = Navigation.MarkiCreate;

            if (ModelState.IsValid)
            {
                var createMarkaViewModel = await _unityOfWork.MarkiRepository.Create(model);
                if (createMarkaViewModel.Success)
                    return RedirectToAction("Index", "Marki");
            }
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            NI.Navigation = Navigation.MarkiEdit;

            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            var marka = await _unityOfWork.MarkiRepository.Get(id);

            if (marka == null)
                return View("NotFound");

            return View(new MarkaViewModel()
            {
                Marka = marka
            });
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MarkaViewModel model)
        {
            NI.Navigation = Navigation.MarkiEdit;

            if (model.Marka == null || string.IsNullOrEmpty(model.Marka.MarkaId))
                return View("NotFound");

            if (ModelState.IsValid)
            {
                if ((await _unityOfWork.MarkiRepository.Update(model)).Success)
                    return RedirectToAction("Index", "Marki");
            }

            return View(model);
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MarkaViewModel model)
        {
            NI.Navigation = Navigation.MarkiEdit;

            if (model.Marka == null || string.IsNullOrEmpty(model.Marka.MarkaId))
                return View("NotFound");

            if (ModelState.IsValid)
            {
                await _unityOfWork.MarkiRepository.Update(model);
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            var marka = await _unityOfWork.MarkiRepository.Get(id);

            if (marka == null)
                return View("NotFound");

            return View(marka);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            bool result = await _unityOfWork.MarkiRepository.Delete(id);
            if (result)
                return RedirectToAction("Index", "Marki");

            return View(result);
        }
    }
}
