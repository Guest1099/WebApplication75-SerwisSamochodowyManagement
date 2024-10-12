using Application.Services.Abs;
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
    public class UserAccountService : IUserAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public UserAccountService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<List<ApplicationUser>> GetAll()
        {
            return await _context.Users
                .Include(i => i.PhotosUser)
                .OrderBy(o => o.DataDodania)
                .ToListAsync();
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _context.Users
                .Include(i => i.PhotosUser)
                .FirstOrDefaultAsync(f => f.Id == userId);
        }

        public async Task<ApplicationUser> GetUserByEmail(string userEmail)
        {
            return await _context.Users
                .Include(i => i.PhotosUser)
                .FirstOrDefaultAsync(f => f.Email == userEmail);
        }

        public async Task<ApplicationUser> GetUserByName(string userName)
        {
            return await _context.Users
                .Include(i => i.PhotosUser)
                .FirstOrDefaultAsync(f => f.UserName == userName);
        }



        public async Task<LoginViewModel> Login(LoginViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Email))
            {
                ApplicationUser user = await _context.Users.FirstOrDefaultAsync(f => f.Email == model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        model.UserIsAdmin = await _userManager.IsInRoleAsync(user, "Administrator");
                        model.Result = string.Concat(await _userManager.GetRolesAsync(user));
                        model.Success = true;
                    }
                    else
                    {
                        model.Result = "Nie udało się zalogować.";
                        model.Success = false;
                    }
                }
                else
                {
                    model.Result = "Użytkownik nie istnieje.";
                    model.Success = false;
                }
            }
            return model;
        }



        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }





        public async Task<ChangeEmailViewModel> ChangeEmail(ChangeEmailViewModel model)
        {
            // Sprawdza czy pola nie są puste
            if (!string.IsNullOrEmpty(model.UserName) && !string.IsNullOrEmpty(model.NewEmail))
            {
                // wyszukuja użytkownika na podstawie emaila
                if ((await _context.Users.FirstOrDefaultAsync(f => f.Email == model.NewEmail)) == null)
                {
                    ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);
                    if (user != null)
                    {
                        string token = await _userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);
                        var result = await _userManager.ChangeEmailAsync(user, model.NewEmail, token);
                        if (result.Succeeded)
                        {
                            model.Result = "Email zmieniony poprawnie. Zaloguj się ponownie aby zatwierdzić zmiany.";

                            // zaktualizowanie nazwy użytkownika 
                            user.UserName = model.NewEmail;
                            await _userManager.UpdateAsync(user);
                            await _signInManager.SignOutAsync();
                            model.Success = true;
                        }
                        else
                        {
                            model.Result = "Coś poszło nie tak.";
                        }
                    }
                }
                else
                {
                    model.Result = "Użytkownik o takim adresie email już istnieje.";
                }
            }
            else
            {
                model.Result = "Some fileds is null.";
            }
            return model;
        }



        public async Task<ChangePasswordViewModel> ChangePassword(ChangePasswordViewModel model)
        {
            if (!string.IsNullOrEmpty(model.UserName))
            {
                ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        model.Result = "Hasło zmienione poprawnie.";
                        model.Success = true;

                        // wylogowanie
                        await _signInManager.SignOutAsync();
                    }
                    else
                    {
                        model.Result = "Błędne hasło.";
                    }
                }
                else
                {
                    model.Result = "Wskazany użytkownik nie istnieje.";
                }
            }
            else
            {
                model.Result = "UserName is null.";
            }
            return model;
        }



        public async Task<UpdateAccountViewModel> UpdateAccount(UpdateAccountViewModel model)
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

        public async Task<UpdateAccountViewModel> UpdateAccountSingle(UpdateAccountViewModel model)
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


                        if (user.Email != "admin@admin.pl")
                        {
                            // Usunięcie ze wszystkich rół
                            foreach (var role in await _context.Roles.ToListAsync())
                                await _userManager.RemoveFromRoleAsync(user, role.Name);


                            // Przypisanie nowych ról
                            foreach (var selectedRole in model.SelectedRoles)
                            {
                                await _userManager.AddToRoleAsync(user, selectedRole);
                                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, selectedRole));
                            }
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




        public async Task<CreateUserViewModel> CreateUserAccount(CreateUserViewModel model)
        {
            if ((await _context.Users.FirstOrDefaultAsync(f => f.Email == model.Email)) == null)
            {
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
                    EmailConfirmed = true,
                    IloscZalogowan = 0,
                    NormalizedUserName = model.Email.ToUpper(),
                    NormalizedEmail = model.Email.ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
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

        public async Task<EditUserViewModel> UpdateUserAccount(EditUserViewModel model)
        {
            if (!string.IsNullOrEmpty(model.UserId))
            {
                ApplicationUser user = await _context.Users
                    .Include(i => i.PhotosUser)
                    .FirstOrDefaultAsync(f => f.Id == model.UserId);
                if (user != null)
                {
                    //user.Email = model.Email; 
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


                        // jeżeli żadna rola nie jest wybrana podczas aktualizacji użytkownika, wtedy dodawana jest standardowa rola
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



        public async Task<bool> DeleteAccountByUserId(string userId)
        {
            bool deleteResult = false;
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    ApplicationUser user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        // usunięcie zdjęć użytkownika
                        var photosUser = await _context.PhotosUser.Where(w => w.UserId == user.Id).ToListAsync();
                        foreach (var photoUser in photosUser)
                            _context.PhotosUser.Remove(photoUser);


                        // usunięcie logów użytkownika
                        var loggingErrors = await _context.LoggingErrors.Where(w => w.UserId == user.Id).ToListAsync();
                        foreach (var loggingError in loggingErrors)
                            _context.LoggingErrors.Remove(loggingError);



                        var result = await _userManager.DeleteAsync(user);
                        if (result.Succeeded)
                        {
                            // Wylogowanie użytkownika
                            //await _signInManager.SignOutAsync();
                            deleteResult = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return deleteResult;
        }




        public async Task<bool> DeleteAccountByEmail(string email)
        {
            bool deleteResult = false;
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    ApplicationUser user = await _userManager.FindByEmailAsync(email);
                    if (user != null)
                    {
                        // usunięcie zdjęć użytkownika
                        var photosUser = await _context.PhotosUser.Where(w => w.UserId == user.Id).ToListAsync();
                        foreach (var photoUser in photosUser)
                            _context.PhotosUser.Remove(photoUser);


                        // usunięcie logów użytkownika
                        var loggingErrors = await _context.LoggingErrors.Where(w => w.UserId == user.Id).ToListAsync();
                        foreach (var loggingError in loggingErrors)
                            _context.LoggingErrors.Remove(loggingError);


                        // usunięcie użytkownika
                        var result = await _userManager.DeleteAsync(user);
                        if (result.Succeeded)
                        {
                            // Wylogowanie użytkownika
                            //await _signInManager.SignOutAsync();
                            deleteResult = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return deleteResult;
        }



        public async Task<bool> DeleteAccountByName(string userName)
        {
            bool deleteResult = false;
            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    ApplicationUser user = await _userManager.FindByNameAsync(userName);
                    if (user != null)
                    {
                        // usunięcie zdjęć użytkownika
                        var photosUser = await _context.PhotosUser.Where(w => w.UserId == user.Id).ToListAsync();
                        foreach (var photoUser in photosUser)
                            _context.PhotosUser.Remove(photoUser);


                        // usunięcie logów użytkownika
                        var loggingErrors = await _context.LoggingErrors.Where(w => w.UserId == user.Id).ToListAsync();
                        foreach (var loggingError in loggingErrors)
                            _context.LoggingErrors.Remove(loggingError);


                        // usunięcie użytkownika
                        var result = await _userManager.DeleteAsync(user);
                        if (result.Succeeded)
                        {
                            // Wylogowanie użytkownika
                            //await _signInManager.SignOutAsync();
                            deleteResult = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return deleteResult;
        }




        public async Task<bool> DeleteAccountWithoutLogout(string userId)
        {
            bool deleteResult = false;
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    deleteResult = true;
                }
            }
            return deleteResult;
        }


        /// <summary>
        /// Usuwa użytkownika z roli
        /// </summary>
        public async Task<bool> RemoveFromRole(string userName, string roleName)
        {
            bool deleteResult = false;
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                IdentityResult result = await _userManager.RemoveFromRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    deleteResult = true;
                }
            }
            return deleteResult;
        }

        /// <summary>
        /// Dodaje użytkownika do roli
        /// </summary>
        public async Task<bool> AddToRole(string userName, string roleName)
        {
            bool result = false;
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                IdentityResult res = await _userManager.AddToRoleAsync(user, roleName);
                if (res.Succeeded)
                {
                    result = true;
                }
            }
            return result;
        }

        /*public async Task<bool> AddToRole (string userName, string roleName)
        {
            bool result = false;
            ApplicationUser user = await _context.Users.FirstOrDefaultAsync (f=> f.UserName == userName);
            if (user != null)
            {
                var role = await _context.Roles.FirstOrDefaultAsync (f=> f.Name == roleName);
                if (role != null)
                {
                    ApplicationUserRole applicationUserRole = new ApplicationUserRole ()
                    {
                        UserId = user.Id,
                        RoleId = role.Id
                    };
                    _context.UserRoles.Add(applicationUserRole);
                    await _context.SaveChangesAsync();
                }
            }
            return result;
        }*/

        /*public async Task<bool> RemoveFromRole (string userName, string roleName)
        {
            bool result = false;
            ApplicationUser user = await _context.Users.FirstOrDefaultAsync (f=> f.UserName == userName);
            ApplicationRole role = await _context.Roles.FirstOrDefaultAsync (f=> f.Name == roleName);
            if (user != null && role != null)
            {
                var applicationUserRole = await _context.UserRoles.FirstOrDefaultAsync (f=> f.UserId == user.Id && f.RoleId == role.Id);
                _context.UserRoles.Remove (applicationUserRole);
                await _context.SaveChangesAsync ();
            }
            return result;
        }*/


        public async Task<bool> AddClaim(string userName, string roleName)
        {
            bool result = false;
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                IdentityResult res = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, roleName));
                if (res.Succeeded)
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Pobiera wszystkie role danego użytkownika
        /// </summary>
        public async Task<List<string>> GetUserRoles(string userName)
        {
            List<string> userRoles = new List<string>();
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                userRoles = (await _userManager.GetRolesAsync(user)).ToList();
            }
            return userRoles;
        }



        /// <summary>
        /// Pobiera wszystkich użytkowników będących w danej roli
        /// </summary>
        public async Task<List<ApplicationUser>> GetUsersInRole(string roleName)
        {
            return (await _userManager.GetUsersInRoleAsync(roleName)).ToList();
        }


        /// <summary>
        /// Sprawdza czy zalogowany user jest administratorem, jeśli tak to przekierowuje go do panelu administratora
        /// </summary>
        public async Task<bool> LoggedUserIsAdmin(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(f => f.Email == email);
            if (user != null)
            {
                return await _userManager.IsInRoleAsync(user, "Administrator");
            }
            else
                return false;
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
