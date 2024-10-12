using Application.Services.Abs;
using Data;
using Domain.Models;
using Domain.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IUserAccountService _userAccountService;

        public HomeController (IUnityOfWork unityOfWork, IUserAccountService userAccountService)
        {
            _unityOfWork = unityOfWork;
            _userAccountService = userAccountService;
        }

        public async Task <IActionResult> Index ()
        {
            NI.Navigation = Navigation.HomeIndex;
/*
            ViewData["IloscKsiazek"] = (await _unityOfWork.KsiazkiRepository.GetAll()).Count;
            ViewData["IloscWypozyczonych"] = (await _unityOfWork.WypozyczeniaRepository.GetAll()).Where(w => w.StatusWypozyczenia == StatusWypozyczenia.Wypozyczona).ToList().Count;
            ViewData["LacznieWypozyczonych"] = (await _unityOfWork.WypozyczeniaRepository.GetAll()).Count;

            ViewData["IloscKont"] = (await _userAccountService.GetAll()).Count;

            ViewData["IloscWypozyczajacych"] = (await _unityOfWork.WypozyczajacyRepository.GetAll()).Count;
*/

            return View();
        }
    }
}
