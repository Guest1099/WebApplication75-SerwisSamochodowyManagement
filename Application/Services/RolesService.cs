using Application.Services.Abs;
using Data;
using Domain.Models;
using Domain.ViewModels.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RolesService : IRolesService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager <ApplicationUser> _userManager;
        public RolesService (ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }

        public async Task<List<ApplicationRole>> GetAll ()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<ApplicationRole> Get (string id)
        {
            return await _context.Roles.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<CreateRoleViewModel> Create (CreateRoleViewModel model)
        {
            if (model != null)
            {
                try
                {
                    // sprawdza czy rola nie istnieje
                    if ((await _context.Roles.FirstOrDefaultAsync (f=> f.Name == model.Name)) == null)
                    {
                        ApplicationRole role = new ApplicationRole ()
                        {
                            Id = Guid.NewGuid ().ToString (),
                            Name = model.Name,
                            NormalizedName = model.Name,
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        };

                        _context.Roles.Add(role);
                        await _context.SaveChangesAsync(); 
                    }
                    else
                    {
                        model.IsExists = true;
                        model.Result = "Podana nazwa roli już istnieje.";
                    }
                }
                catch (Exception ex)
                {
                    model.Result = "Catch exception."; 
                }
            }
            else
            {
                model.Result = "Model is null.";
            }
            return model;
        }

        public async Task Delete (string id)
        {
            try
            {
                var model = await _context.Roles.FirstOrDefaultAsync (f=>f.Id == id);
                if (model != null)
                {
                    // przypisanie użytkowników do roli
                    var userInRoles = (await _userManager.GetUsersInRoleAsync (model.Name)).ToList ();
                    foreach (var userInRole in userInRoles)
                        await _userManager.AddToRoleAsync (userInRole, "User");

                    // usunięcie roli
                    _context.Roles.Remove(model);
                    await _context.SaveChangesAsync();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }
        }


        public async Task<EditRoleViewModel> Update (EditRoleViewModel model)
        {
            if (model != null)
            {
                try
                {
                    // sprawdzenie czy podana nazwa roli już istnieje
                    if ((await _context.Roles.FirstOrDefaultAsync(f => f.Name == model.Name)) == null)
                    {
                        var role = _context.Roles.FirstOrDefault (f=> f.Id == model.RoleId);

                        role.Name = model.Name;
                        _context.Entry(role).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                    }
                    else
                    {
                        model.IsExists = true;
                        model.Result = "Taka nazwa już istnieje.";
                    }
                }
                catch (Exception ex)
                {
                    model.Result = "Catch exception.";
                }
            }
            else
            {
                model.Result = "Model is null.";
            }

            return model;
        }

        public async Task <List <ApplicationUser>> UsersInRole (string roleName)
        {
            return (await _userManager.GetUsersInRoleAsync (roleName)).ToList ();
        }
    }
}
