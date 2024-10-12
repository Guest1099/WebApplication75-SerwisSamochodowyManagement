using Application.Services.Abs;
using Data;
using Data.Services;
using Domain.Models;
using Domain.Models.Enums;
using Domain.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly IUserAccountService _accountService;
        private readonly IUnityOfWork _unityOfWork;

        public UsersController(IUserAccountService accountService, IUnityOfWork unityOfWork)
        {
            _accountService = accountService;
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index(UsersViewModel model)
        {
            NI.Navigation = Navigation.UsersIndex;
            var users = await _accountService.GetAll();

            return View(new UsersViewModel()
            {
                Users = users,
                Paginator = Paginator<ApplicationUser>.CreateAsync(users, model.PageIndex, model.PageSize),
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
        public async Task<IActionResult> Index(string s, UsersViewModel model)
        {
            var users = await _accountService.GetAll();

            try
            {
                // Wyszukiwanie
                if (!string.IsNullOrEmpty(model.q))
                    users = users.Where(w => w.Nazwisko.Contains(model.q, StringComparison.OrdinalIgnoreCase) ||
                                             w.Email.Contains(model.q, StringComparison.OrdinalIgnoreCase)).ToList();


                // Sortowanie 
                switch (model.SortowanieOption)
                {
                    case "Użytkownicy":
                        users = await _accountService.GetUsersInRole("User");
                        break;

                    case "Administratorzy":
                        users = await _accountService.GetUsersInRole("Administrator");
                        break;

                    case "Zarząd":
                        users = await _accountService.GetUsersInRole("Zarząd");
                        break;

                    case "Marketing":
                        users = await _accountService.GetUsersInRole("Marketing");
                        break;

                    case "Wszyscy":
                        break;
                }
            }
            catch { }

            model.Users = users;
            model.Paginator = Paginator<ApplicationUser>.CreateAsync(users, model.PageIndex, model.PageSize);
            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            NI.Navigation = Navigation.UsersCreate;
            return View(new CreateUserViewModel() { SelectedRoles = new List<string>() { }, Result = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            NI.Navigation = Navigation.UsersCreate;
            if (ModelState.IsValid)
            {
                model.Password = "SDG%$@5423sdgagSDert";
                if ((await _accountService.CreateUserAccount(model)).Success)
                    return RedirectToAction("Index", "Users");
            }

            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            NI.Navigation = Navigation.UsersEdit;
            if (string.IsNullOrEmpty(userId))
                return View("NotFound");

            var user = await _accountService.GetUserById(userId);
            var userRoles = await _accountService.GetUserRoles(user.UserName);

            if (user == null || user.PhotosUser == null)
                return View("NotFound");


            return View(new EditUserViewModel()
            {
                UserId = user.Id,
                Email = user.Email,
                Imie = user.Imie,
                Nazwisko = user.Nazwisko,
                Ulica = user.Ulica,
                NumerUlicy = user.NumerUlicy,
                Miejscowosc = user.Miejscowosc,
                Kraj = user.Kraj,
                KodPocztowy = user.KodPocztowy,
                Telefon = user.Telefon,
                Plec = user.Plec,
                DataUrodzenia = user.DataUrodzenia,
                SelectedRoles = userRoles,
                PhotosUser = user.PhotosUser,
                DataDodania = user.DataDodania,
                Result = ""
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            NI.Navigation = Navigation.UsersEdit;
            if (string.IsNullOrEmpty(model.UserId))
                return View("NotFound");

            if (ModelState.IsValid)
            {
                if ((await _accountService.UpdateUserAccount(model)).Success)
                    return RedirectToAction("Index", "Users");
            }

            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            var user = await _accountService.GetUserById(id);

            if (user == null)
                return View("NotFound");

            return View(user);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            await _accountService.DeleteAccountByUserId(id);

            return RedirectToAction("Index", "Users");
        }

        /*
                [HttpGet]
                public async Task<IActionResult> DeletePhotoUser (string userId, string photoUserId)
                {
                    if (string.IsNullOrEmpty (userId) || string.IsNullOrEmpty(photoUserId))
                        return View("NotFound");

                    await _unityOfWork.PhotosUserRepository.Delete (photoUserId);

                    return RedirectToAction("Edit", "Users", new { userId = user.Id });
                }*/

        [HttpGet]
        public async Task<IActionResult> DeletePhotoUser(string userId, string photoUserId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(photoUserId))
                return View("NotFound");

            await _unityOfWork.PhotosUserRepository.Delete(photoUserId);

            return RedirectToAction("Edit", "Users", new { userId = userId });
        }



    }
}
