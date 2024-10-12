using Application.Services.Abs;
using Data;
using Domain.Models;
using Domain.Models.Enums;
using Domain.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IUnityOfWork _unityOfWork;

        public AccountController(IUserAccountService userAccountService, IUnityOfWork unityOfWork)
        {
            _userAccountService = userAccountService;
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            NI.Navigation = Navigation.AccountEdit;
            var user = await _userAccountService.GetUserByEmail(User.Identity.Name);

            if (user == null)
                return View("NotFound");

            var userRoles = await _userAccountService.GetUserRoles(user.UserName);

            if (userRoles == null)
                return View("NotFound");


            return View(new UpdateAccountViewModel()
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
        public async Task<IActionResult> Edit(UpdateAccountViewModel model)
        {
            NI.Navigation = Navigation.AccountEdit;
            if (string.IsNullOrEmpty(model.UserId))
                return View("NotFound");


            if (ModelState.IsValid)
            {
                if ((await _userAccountService.UpdateAccountSingle(model)).Success)
                    return RedirectToAction("Edit", "Account");
            }

            return View(model);
        }



        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            return View();
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            await _userAccountService.DeleteAccountByName(User.Identity.Name);

            return RedirectToAction("Index", "Account");
        }



        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
            => View(new LoginViewModel() { Result = "" });

        /*
                [AllowAnonymous]
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Login(LoginViewModel model)
                {
                    if (ModelState.IsValid)
                    {
                        var login = await _userAccountService.Login(model);
                        if (login.Success)
                        {

                            // sprawdza czy user jest adminem jeśli tak to przekierowywuje go do panelu administratora
                            if (login.UserIsAdmin)
                                return RedirectToAction("Index", "Home", new { area = "Admin" });
                            else
                                return RedirectToAction("Index", "Home", new { area = "" });

                        }
                    }

                    return View(model);
                }
        */


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var login = await _userAccountService.Login(model);
            if (login.Success)
            {

                return RedirectToAction("Index", "Home");

            }
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _userAccountService.Logout();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult ChangePassword()
        {
            NI.Navigation = Navigation.AccountChangePassword;
            return View(new ChangePasswordViewModel() { Result = "" });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            NI.Navigation = Navigation.AccountChangePassword;
            if (ModelState.IsValid)
            {
                model.UserName = User.Identity.Name;
                if ((await _userAccountService.ChangePassword(model)).Success)
                    return RedirectToAction("Index", "Home");
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> ChangeEmail()
        {
            NI.Navigation = Navigation.AccountChangeEmail;
            var user = await _userAccountService.GetUserByName(User.Identity.Name);
            if (user == null)
                return View("NotFound");

            return View(new ChangeEmailViewModel() { ObecnyEmail = user.Email, Result = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            NI.Navigation = Navigation.AccountChangeEmail;
            if (ModelState.IsValid)
            {
                model.UserName = User.Identity.Name;
                if ((await _userAccountService.ChangeEmail(model)).Success)
                    return RedirectToAction("Index", "Home");
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> DeletePhotoUser(string clientId, string photoClientId)
        {
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(photoClientId))
                return View("NotFound");

            await _unityOfWork.PhotosUserRepository.Delete(photoClientId);

            return RedirectToAction("Edit", "Account");
        }


        public IActionResult TEST()
        {
            return View();
        }





    }
}
