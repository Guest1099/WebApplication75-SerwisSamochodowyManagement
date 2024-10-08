using Application.Services.Abs;
using Data;
using Domain.Models;
using Domain.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IUserAccountService _userAccountService;

        public HomeController(IUnityOfWork unityOfWork, IUserAccountService userAccountService)
        {
            _unityOfWork = unityOfWork;
            _userAccountService = userAccountService;
        }

        public async Task<IActionResult> Index()
        {
            NI.Navigation = Navigation.HomeIndex;

            ViewData["IloscKont"] = (await _userAccountService.GetAll()).Count;


            return View();
        }
    }
}
