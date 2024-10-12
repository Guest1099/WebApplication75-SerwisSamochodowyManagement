using Application.Services.Abs;
using Data.Services;
using Domain.Models;
using Domain.Models.Enums;
using Domain.ViewModels.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RolesController : Controller
    {
        private readonly IRolesService _rolesService;

        public RolesController (IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index (RolesViewModel model)
        {
            NI.Navigation = Navigation.RolesIndex;
            var roles = await _rolesService.GetAll();
            return View(new RolesViewModel()
            {
                Roles = roles,
                Paginator = Paginator<ApplicationRole>.CreateAsync(roles, model.PageIndex, model.PageSize),
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
        public async Task<IActionResult> Index (string s, RolesViewModel model)
        {
            NI.Navigation = Navigation.RolesIndex;
            var roles = await _rolesService.GetAll();

            // Wyszukiwanie
            if (!string.IsNullOrEmpty(model.q))
                roles = roles.Where(w => w.Name.Contains(model.q, StringComparison.OrdinalIgnoreCase)).ToList();


            // Sortowanie 
            switch (model.SortowanieOption)
            {
                case "Nazwa A-Z":
                    roles = roles.OrderBy(o => o.Name).ToList();
                    break;

                case "Nazwa Z-A":
                    roles = roles.OrderByDescending(o => o.Name).ToList();
                    break;
            }

            model.Roles = roles;
            model.Paginator = Paginator<ApplicationRole>.CreateAsync(roles, model.PageIndex, model.PageSize);
            return View(model);
        }



        [HttpGet]
        public IActionResult Create ()
        {
            NI.Navigation = Navigation.RolesCreate;
            return View(new CreateRoleViewModel() { Result = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (CreateRoleViewModel model)
        {
            NI.Navigation = Navigation.RolesCreate;
            /*if (ModelState.IsValid)
            {*/
                if (!(await _rolesService.Create(model)).IsExists)
                    return RedirectToAction("Index", "Roles");
            /*}*/

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit (string roleId)
        {
            NI.Navigation = Navigation.RolesEdit;

            if (string.IsNullOrEmpty(roleId))
                return View("NotFound");

            var role = await _rolesService.Get (roleId);

            if (role == null)
                return View("NotFound");

            return View(new EditRoleViewModel()
            {
                RoleId = role.Id,
                Name = role.Name,
                Result = ""
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (EditRoleViewModel model)
        {
            NI.Navigation = Navigation.RolesEdit;
            if (string.IsNullOrEmpty(model.RoleId))
                return View("NotFound");

            if (ModelState.IsValid)
            {
                if (!(await _rolesService.Update(model)).IsExists)
                    return RedirectToAction("Index", "Roles");
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete (string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            var role = await _rolesService.Get (id);

            if (role == null)
                return View("NotFound");

            return View(role);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("NotFound");

            await _rolesService.Delete(id);

            return RedirectToAction("Index", "Roles");
        } 



    }
}
