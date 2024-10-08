using Data.Repos.Abs;
using Domain.Models;
using Domain.ViewModels.Owners;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class OwnersRepository : IOwnersRepository
    {
        private readonly ApplicationDbContext _context;
        public OwnersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Owner>> GetAll()
        {
            return await _context.Owners.ToListAsync();
        }

        public async Task<Owner> Get(string id)
        {
            return await _context.Owners.FirstOrDefaultAsync(f => f.OwnerId == id);
        }


        public async Task<OwnerViewModel> Create(OwnerViewModel model)
        {
            if (model != null)
            {
                try
                {
                    DaneOsobowe daneOsobowe = new DaneOsobowe()
                    {
                        DaneOsoboweId = Guid.NewGuid().ToString(),
                        Imie = model.DaneOsobowe.Imie,
                        Nazwisko = model.DaneOsobowe.Nazwisko,
                        Ulica = model.DaneOsobowe.Ulica,
                        NumerUlicy = model.DaneOsobowe.NumerUlicy,
                        Miejscowosc = model.DaneOsobowe.Miejscowosc,
                        Kraj = model.DaneOsobowe.Kraj,
                        Powiat = model.DaneOsobowe.Powiat,
                        KodPocztowy = model.DaneOsobowe.KodPocztowy,
                        Pesel = model.DaneOsobowe.Pesel,
                        DataUrodzenia = model.DaneOsobowe.DataUrodzenia,
                        Email = model.DaneOsobowe.Email,
                        Telefon = model.DaneOsobowe.Telefon,
                        Plec = model.DaneOsobowe.Plec,
                        RodzajOsoby = model.DaneOsobowe.RodzajOsoby,

                        Firma_Nazwa = model.DaneOsobowe.Firma_Nazwa,
                        Firma_NIP = model.DaneOsobowe.Firma_NIP,
                        Firma_Regon = model.DaneOsobowe.Firma_Regon,
                        Firma_Ulica = model.DaneOsobowe.Firma_Ulica,
                        Firma_NumerUlicy = model.DaneOsobowe.Firma_NumerUlicy,
                        Firma_Miejscowosc = model.DaneOsobowe.Firma_Miejscowosc,
                        Firma_KodPocztowy = model.DaneOsobowe.Firma_KodPocztowy,
                        Firma_Powiat = model.DaneOsobowe.Firma_Powiat,
                        Firma_Kraj = model.DaneOsobowe.Firma_Kraj,

                        DataDodania = DateTime.Now.ToString()
                    };
                    _context.DaneOsobowe.Add(daneOsobowe);


                    Owner owner = new Owner()
                    {
                        OwnerId = Guid.NewGuid().ToString(),
                        DaneOsoboweId = daneOsobowe.DaneOsoboweId
                    };
                    _context.Owners.Add(owner);


                    await _context.SaveChangesAsync();

                    model.Success = true;
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



        public async Task<OwnerViewModel> Update(OwnerViewModel model)
        {
            if (model != null)
            {
                try
                {
                    var owner = await _context.Owners
                        .Include(i => i.DaneOsobowe)
                        .FirstOrDefaultAsync(f => f.OwnerId == model.Owner.OwnerId);

                    if (owner != null)
                    {
                        var daneOsobowe = owner.DaneOsobowe;
                        if (daneOsobowe != null)
                        {
                            // aktual
                            daneOsobowe.Imie = model.DaneOsobowe.Imie;
                            daneOsobowe.Nazwisko = model.DaneOsobowe.Nazwisko;
                            daneOsobowe.Ulica = model.DaneOsobowe.Ulica;
                            daneOsobowe.NumerUlicy = model.DaneOsobowe.NumerUlicy;
                            daneOsobowe.Miejscowosc = model.DaneOsobowe.Miejscowosc;
                            daneOsobowe.Kraj = model.DaneOsobowe.Kraj;
                            daneOsobowe.Powiat = model.DaneOsobowe.Powiat;
                            daneOsobowe.KodPocztowy = model.DaneOsobowe.KodPocztowy;
                            daneOsobowe.Pesel = model.DaneOsobowe.Pesel;
                            daneOsobowe.DataUrodzenia = model.DaneOsobowe.DataUrodzenia;
                            daneOsobowe.Email = model.DaneOsobowe.Email;
                            daneOsobowe.Telefon = model.DaneOsobowe.Telefon;
                            daneOsobowe.Plec = model.DaneOsobowe.Plec;
                            daneOsobowe.RodzajOsoby = model.DaneOsobowe.RodzajOsoby;

                            daneOsobowe.Firma_Nazwa = model.DaneOsobowe.Firma_Nazwa;
                            daneOsobowe.Firma_NIP = model.DaneOsobowe.Firma_NIP;
                            daneOsobowe.Firma_Regon = model.DaneOsobowe.Firma_Regon;
                            daneOsobowe.Firma_Ulica = model.DaneOsobowe.Firma_Ulica;
                            daneOsobowe.Firma_NumerUlicy = model.DaneOsobowe.Firma_NumerUlicy;
                            daneOsobowe.Firma_Miejscowosc = model.DaneOsobowe.Firma_Miejscowosc;
                            daneOsobowe.Firma_KodPocztowy = model.DaneOsobowe.Firma_KodPocztowy;
                            daneOsobowe.Firma_Powiat = model.DaneOsobowe.Firma_Powiat;
                            daneOsobowe.Firma_Kraj = model.DaneOsobowe.Firma_Kraj;

                            daneOsobowe.DataDodania = model.DaneOsobowe.DataDodania;


                            _context.Entry(daneOsobowe).State = EntityState.Modified;

                        }

                        _context.Entry(owner).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        model.Success = true;
                    }
                    else
                    {
                        model.Result = "Data is null.";
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



        public async Task<bool> Delete(string id)
        {
            bool deleteResult = false;
            try
            {
                var owner = await _context.Owners.FirstOrDefaultAsync(f => f.OwnerId == id);
                if (owner != null)
                {
                    // usunięcie danych osobowych 
                    var daneOsobowe = await _context.DaneOsobowe.FirstOrDefaultAsync(f => f.DaneOsoboweId == owner.DaneOsoboweId);
                    if (daneOsobowe != null)
                        _context.DaneOsobowe.Remove(daneOsobowe);


                    // usunięcie ownera
                    _context.Owners.Remove(owner);

                    await _context.SaveChangesAsync();
                    deleteResult = true;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }
            return deleteResult;
        }


    }
}
