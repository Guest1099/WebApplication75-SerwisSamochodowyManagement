using Data;
using Data.Services;
using Domain.Models;
using Domain.Models.Enums;
using Domain.ViewModels.Clients;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;

        public ClientsController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> Index(ClientsViewModel model)
        {
            NI.Navigation = Navigation.ClientsIndex;
            var clients = await _unityOfWork.ClientsRepository.GetAll();
            return View(new ClientsViewModel()
            {
                Clients = clients,
                Paginator = Paginator<Client>.CreateAsync(clients, model.PageIndex, model.PageSize),
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
        public async Task<IActionResult> Index(string s, ClientsViewModel model)
        {
            NI.Navigation = Navigation.ClientsIndex;
            var clients = await _unityOfWork.ClientsRepository.GetAll();

            // Wyszukiwanie
            if (!string.IsNullOrEmpty(model.q))
                clients = clients.Where(w =>
                    w.DaneOsobowe.Nazwisko.Contains(model.q, StringComparison.OrdinalIgnoreCase) ||
                    w.DaneOsobowe.Firma_Nazwa.Contains(model.q, StringComparison.OrdinalIgnoreCase)
                    ).ToList();


            // Sortowanie 
            switch (model.SortowanieOption)
            {
                case "Nazwisko A-Z":
                    clients = clients.OrderBy(o => o.DaneOsobowe.Nazwisko).ToList();
                    break;

                case "Nazwisko Z-A":
                    clients = clients.OrderByDescending(o => o.DaneOsobowe.Nazwisko).ToList();
                    break;
            }

            model.Clients = clients;
            model.Paginator = Paginator<Client>.CreateAsync(clients, model.PageIndex, model.PageSize);
            return View(model);
        }



        [HttpGet]
        public IActionResult Create()
        {
            NI.Navigation = Navigation.ClientsCreate;
            return View(new ClientViewModel() { Result = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientViewModel model)
        {
            NI.Navigation = Navigation.ClientsCreate;
            if (ModelState.IsValid)
            {
                if (!(await _unityOfWork.ClientsRepository.Create(model)).Success)
                    return RedirectToAction("Index", "Clients");
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string clientId)
        {
            NI.Navigation = Navigation.ClientsEdit;

            if (string.IsNullOrEmpty(clientId))
                return View("NotFound");

            var client = await _unityOfWork.ClientsRepository.Get(clientId);

            if (client == null && client.DaneOsobowe == null)
                return View("NotFound");

            return View(new ClientViewModel()
            {
                Client = client,
                DaneOsobowe = client.DaneOsobowe,
                Result = "",
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClientViewModel model)
        {
            NI.Navigation = Navigation.ClientsEdit;

            if (string.IsNullOrEmpty(model.Client.ClientId))
                return View("NotFound");

            if (ModelState.IsValid)
            {
                if (!(await _unityOfWork.ClientsRepository.Update(model)).Success)
                    return RedirectToAction("Index", "Clients");
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            var client = await _unityOfWork.ClientsRepository.Get(id);

            if (client == null)
                return View("NotFound");

            return View(client);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            if (await _unityOfWork.ClientsRepository.Delete(id))
                return RedirectToAction("Index", "Clients");

            return View("NotFound");
        }
    }

}