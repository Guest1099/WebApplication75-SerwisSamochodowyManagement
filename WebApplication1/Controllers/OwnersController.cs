using Data.Services;
using Data;
using Domain.Models;
using Domain.ViewModels.Marki;
using Microsoft.AspNetCore.Mvc;
using Domain.ViewModels.Owners;
using Domain.Models.Enums;

namespace WebApplication1.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;

        public OwnersController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index(OwnersViewModel model)
        {
            NI.Navigation = Navigation.OwnersIndex;
            var owners = await _unityOfWork.OwnersRepository.GetAll();
            return View(new OwnersViewModel()
            {
                Owners = owners,
                Paginator = Paginator<Owner>.CreateAsync(owners, model.PageIndex, model.PageSize),
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
        public async Task<IActionResult> Index(string s, OwnersViewModel model)
        {
            NI.Navigation = Navigation.OwnersIndex;
            var owners = await _unityOfWork.OwnersRepository.GetAll();


            // Wyszukiwanie
            if (!string.IsNullOrEmpty(model.q))
                owners = owners.Where(w => 
                    w.DaneOsobowe.Nazwisko.Contains(model.q, StringComparison.OrdinalIgnoreCase)
                    ).ToList();


            // Sortowanie 
            switch (model.SortowanieOption)
            {
                case "Nazwisko A-Z":
                    owners = owners.OrderBy(o => o.DaneOsobowe.Nazwisko).ToList();
                    break;

                case "Nazwisko Z-A":
                    owners = owners.OrderByDescending(o => o.DaneOsobowe.Nazwisko).ToList();
                    break;
            }

            model.Owners = owners;
            model.Paginator = Paginator<Owner>.CreateAsync(owners, model.PageIndex, model.PageSize);
            return View(model);
        }



        [HttpGet]
        public IActionResult Create()
        {
            NI.Navigation = Navigation.MarkiCreate;
            return View(new MarkaViewModel() { Result = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OwnerViewModel model)
        {
            NI.Navigation = Navigation.OwnersCreate;
            if (ModelState.IsValid)
            {
                if (!(await _unityOfWork.OwnersRepository.Create(model)).Success)
                    return RedirectToAction("Index", "Marki");
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            NI.Navigation = Navigation.OwnersEdit;

            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            var owner = await _unityOfWork.OwnersRepository.Get(id);

            if (owner == null)
                return View("NotFound");

            return View(new OwnerViewModel()
            {
                Owner = owner
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OwnerViewModel model)
        {
            NI.Navigation = Navigation.OwnersEdit;

            if (string.IsNullOrEmpty(model.Owner.OwnerId))
                return View("NotFound");

            if (ModelState.IsValid)
            {
                if (!(await _unityOfWork.OwnersRepository.Update(model)).Success)
                    return RedirectToAction("Index", "Owners");
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            var owner = await _unityOfWork.OwnersRepository.Get(id);

            if (owner == null)
                return View("NotFound");

            return View(owner);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            if (await _unityOfWork.OwnersRepository.Delete(id))
                return RedirectToAction("Index", "Owners");

            return View("NotFound");
        }
    }
}
