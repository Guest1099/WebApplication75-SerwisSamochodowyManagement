using Data;
using Domain.Models;
using Domain.ViewModels.Account;
using Domain.ViewModels.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Application.Services
{
    public class UsersService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<ApplicationUser>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }



        public async Task<ApplicationUser> GetUserById(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(f => f.Id == id);
            return user;
        }


        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(f => f.Email == email);
            return user;
        }




        public async Task<CreateUserViewModel> Create(CreateUserViewModel model)
        {
            // warunek sprawdza czy konto istnieje
            if ((await _context.Users.FirstOrDefaultAsync(f => f.Email == model.Email)) == null)
            {
                // jeżeli nie tworzy nowego użytkownika

                ApplicationUser user = new ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = model.Email,
                    UserName = model.Email,
                    Imie = model.Imie,
                    Nazwisko = model.Nazwisko,
                    Ulica = model.Ulica,
                    NumerUlicy = model.NumerUlicy,
                    Miejscowosc = model.Miejscowosc,
                    KodPocztowy = model.KodPocztowy,
                    Kraj = model.Kraj,
                    Telefon = model.Telefon,
                    IloscZalogowan = 0,
                    NormalizedUserName = model.Email.ToUpper(),
                    NormalizedEmail = model.Email.ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    DataDodania = DateTime.Now.ToString()
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    // dodanie zdjęcia
                    await CreateNewPhoto(model.Files, user.Id);


                    // dodanie nowozarejestrowanego użytkownika do ról 

                    // jeżeli żadna rola nie jest wybrana podczas tworzenia nowego użytkownika, wtedy dodawana jest standardowa rola
                    if (model.SelectedRoles.Count == 0)
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));
                    }
                    else
                    {
                        foreach (var selectedRole in model.SelectedRoles)
                        {
                            await _userManager.AddToRoleAsync(user, selectedRole);
                            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, selectedRole));
                        }
                    }

                    model.Result = "Zarejestrowano, sprawdź pocztę aby dokończyć rejestrację";
                    model.Success = true;
                }
                else
                {
                    model.Result = "Nie zarejestrowano";
                }
            }
            else
            {
                model.Result = "Nazwa maila jest już zajęta.";
            }

            return model;
        }






        public async Task<UpdateAccountViewModel> Update(UpdateAccountViewModel model)
        {
            if (!string.IsNullOrEmpty(model.UserId))
            {
                ApplicationUser user = await _context.Users
                    .Include(i => i.PhotosUser)
                    .FirstOrDefaultAsync(f => f.Id == model.UserId);

                if (user != null)
                {
                    user.Email = model.Email;
                    user.Imie = model.Imie;
                    user.Nazwisko = model.Nazwisko;
                    user.Ulica = model.Ulica;
                    user.NumerUlicy = model.NumerUlicy;
                    user.Miejscowosc = model.Miejscowosc;
                    user.KodPocztowy = model.KodPocztowy;
                    user.Kraj = model.Kraj;
                    user.Telefon = model.Telefon;
                    user.DataUrodzenia = model.DataUrodzenia;
                    user.Plec = model.Plec;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        // dodanie zdjęcia
                        await CreateNewPhoto(model.Files, user.Id);


                        // Usunięcie ze wszystkich rół
                        foreach (var role in await _context.Roles.ToListAsync())
                            await _userManager.RemoveFromRoleAsync(user, role.Name);


                        // Przypisanie nowych ról
                        foreach (var selectedRole in model.SelectedRoles)
                        {
                            await _userManager.AddToRoleAsync(user, selectedRole);
                            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, selectedRole));
                        }

                        model.Result = "Dane zostały zaktualizowane poprawnie.";
                        model.Success = true;
                    }
                }
            }
            else
            {
                model.Result = "UserId is null.";
            }

            return model;
        }





        public async Task<bool> Delete(string userId)
        {
            bool deleteResult = false;
            if (!string.IsNullOrEmpty(userId))
            {
                ApplicationUser user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    // usunięcie zdjęć
                    var photosUser = await _context.PhotosUser.Where(w => w.UserId == user.Id).ToListAsync();
                    foreach (var photoUser in photosUser)
                        _context.PhotosUser.Remove(photoUser);


                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        // Wylogowanie użytkownika
                        //await _signInManager.SignOutAsync();
                        deleteResult = true;
                    }
                }
            }
            return deleteResult;
        }




        private async Task CreateNewPhoto(List<IFormFile> files, string userId)
        {
            try
            {
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            byte[] photoData;
                            using (var stream = new MemoryStream())
                            {
                                file.CopyTo(stream);
                                photoData = stream.ToArray();

                                PhotoUser photoUser = new PhotoUser()
                                {
                                    PhotoUserId = Guid.NewGuid().ToString(),
                                    PhotoData = photoData,
                                    UserId = userId
                                };
                                _context.PhotosUser.Add(photoUser);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
            catch { }
        }

    }





}

