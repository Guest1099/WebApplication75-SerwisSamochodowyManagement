using Data;
using Data.Services;
using Domain.Models;
using Domain.Models.Enums;
using Domain.ViewModels.Towary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace WebApplication1.Controllers
{
    public class TowaryController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;

        public TowaryController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index(TowaryViewModel model)
        {
            NI.Navigation = Navigation.RolesIndex;
            var towary = await _unityOfWork.TowaryRepository.GetAll();
            return View(new TowaryViewModel()
            {
                Towary = towary,
                Paginator = Paginator<Towar>.CreateAsync(towary, model.PageIndex, model.PageSize),
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
        public async Task<IActionResult> Index(string s, TowaryViewModel model)
        {
            NI.Navigation = Navigation.RolesIndex;
            var towary = await _unityOfWork.TowaryRepository.GetAll();

            // Wyszukiwanie
            if (!string.IsNullOrEmpty(model.q))
                towary = towary.Where(w => w.Nazwa.Contains(model.q, StringComparison.OrdinalIgnoreCase)).ToList();


            // Sortowanie 
            switch (model.SortowanieOption)
            {
                case "Nazwa A-Z":
                    towary = towary.OrderBy(o => o.Nazwa).ToList();
                    break;

                case "Nazwa Z-A":
                    towary = towary.OrderByDescending(o => o.Nazwa).ToList();
                    break;
            }

            model.Towary = towary;
            model.Paginator = Paginator<Towar>.CreateAsync(towary, model.PageIndex, model.PageSize);
            return View(model);
        }



        [HttpGet]
        public async Task <IActionResult> Create()
        {
            NI.Navigation = Navigation.RolesCreate;

            ViewData["markiIdList"] = new SelectList(await _unityOfWork.MarkiRepository.GetAll(), "MarkaId", "Name");
            ViewData["rodzajeTowarowIdList"] = new SelectList(await _unityOfWork.RodzajeTowarowRepository.GetAll(), "RodzajTowaruId", "Name");
            
            return View(new TowarViewModel() { Result = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TowarViewModel model)
        {
            NI.Navigation = Navigation.RolesCreate;
            /*if (ModelState.IsValid)
            {*/
                if (!(await _unityOfWork.TowaryRepository.Create(model)).Success)
                    return RedirectToAction("Index", "Towary");
            /*}*/
            ViewData["markiIdList"] = new SelectList(await _unityOfWork.MarkiRepository.GetAll(), "MarkaId", "Name", model.Towar.MarkaId);
            ViewData["rodzajeTowarowIdList"] = new SelectList(await _unityOfWork.RodzajeTowarowRepository.GetAll(), "RodzajTowaruId", "Name", model.Towar.RodzajTowaruId);

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string towarId)
        {
            NI.Navigation = Navigation.RolesEdit;

            if (string.IsNullOrEmpty(towarId))
                return View("NotFound");

            var towar = await _unityOfWork.TowaryRepository.Get(towarId);

            if (towar == null)
                return View("NotFound");

            ViewData["markiIdList"] = new SelectList(await _unityOfWork.MarkiRepository.GetAll(), "MarkaId", "Name", towar.TowarId);
            ViewData["rodzajeTowarowIdList"] = new SelectList(await _unityOfWork.RodzajeTowarowRepository.GetAll(), "RodzajTowaruId", "Name", towar.RodzajTowaruId);

            return View(new TowarViewModel()
            {
                Towar = towar
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TowarViewModel model)
        {
            NI.Navigation = Navigation.RolesEdit;
            if (model.Towar == null || string.IsNullOrEmpty(model.Towar.TowarId))
                return View("NotFound");

            /*if (ModelState.IsValid)
            {*/
                if (!(await _unityOfWork.TowaryRepository.Update(model)).Success)
                    return RedirectToAction("Index", "Towary");
            /*}*/

            ViewData["markiIdList"] = new SelectList(await _unityOfWork.MarkiRepository.GetAll(), "MarkaId", "Name", model.Towar.TowarId);
            ViewData["rodzajeTowarowIdList"] = new SelectList(await _unityOfWork.RodzajeTowarowRepository.GetAll(), "RodzajTowaruId", "Name", model.Towar.RodzajTowaruId);

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            var role = await _unityOfWork.TowaryRepository.Get(id);

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

            if (await _unityOfWork.TowaryRepository.Delete(id))
                return RedirectToAction("Index", "Roles");

            return View("NotFound");
        }

    }
}
