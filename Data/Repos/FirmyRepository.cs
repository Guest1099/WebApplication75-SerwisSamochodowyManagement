using Data.Repos.Abs;
using Domain.Models;
using Domain.ViewModels.Firmy;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class FirmyRepository : IFirmyRepository
    {

        private readonly ApplicationDbContext _context;
        public FirmyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Owner>> GetAll()
        {
            return await _context.Firmy.ToListAsync();
        }

        public async Task<Owner> Get(string id)
        {
            return await _context.Firmy.FirstOrDefaultAsync(f => f.FirmaId == id);
        }


        public async Task<FirmaViewModel> Create(FirmaViewModel model)
        {
            if (model != null)
            {
                try
                {
                    Client daneOsobowe = new Client()
                    {
                        Imie = model.Imie_DaneOsobowe,
                        Nazwisko = model.Nazwisko_DaneOsobowe,
                        Telefon = model.Telefon_DaneOsobowe,
                        Ulica = model.Ulica_DaneOsobowe,
                        NumerUlicy = model.NumerUlicy_DaneOsobowe,
                        Miejscowosc = model.Miejscowosc_DaneOsobowe,
                        Pesel = model.Pesel_DaneOsobowe,
                        Kraj = model.Kraj_DaneOsobowe,
                        KodPocztowy = model.KodPocztowy_DaneOsobowe,
                        DataUrodzenia = model.DataUrodzenia_DaneOsobowe,
                        Plec = model.Plec_DaneOsobowe,
                        Email = model.Email_DaneOsobowe,
                        RodzajTransakcji = model.RodzajTransakcji_DaneOsobowe,
                        DataDodania = DateTime.Now.ToString ()
                    };
                    _context.DaneOsobowe.Add(daneOsobowe);
                    await _context.SaveChangesAsync();


                    Owner firma = new Owner()
                    {
                        FirmaId = Guid.NewGuid().ToString(),
                        Kraj = model.Kraj,
                        Miasto = model.Miasto,
                        NazwaFirmy = model.NazwaFirmy,
                        NIP = model.NIP,
                        Ulica = model.Ulica,
                        NumerUlicy = model.NumerUlicy,
                        Powiat = model.Powiat,
                        Regon = model.Regon,
                        WlascicielId = model.WlascicielId
                    };
                    _context.Firmy.Add(firma);
                    await _context.SaveChangesAsync();
                    model.Success = true;


                    // Dodanie zdjęcia
                    await CreateNewPhoto(model.Files, firma.FirmaId);

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



        public async Task<FirmaViewModel> Update(FirmaViewModel model)
        {
            if (model != null)
            {
                try
                {
                    var firma = await _context.Firmy.FirstOrDefaultAsync(f => f.FirmaId == model.FirmaId);
                    if (firma != null)
                    {
                        var daneOsobowe = await _context.DaneOsobowe.FirstOrDefaultAsync(f => f.DaneOsoboweId == firma.WlascicielId);
                        if (daneOsobowe != null)
                        {
                            daneOsobowe.Imie = model.Imie_DaneOsobowe;
                            daneOsobowe.Nazwisko = model.Nazwisko_DaneOsobowe;
                            daneOsobowe.Telefon = model.Telefon_DaneOsobowe;
                            daneOsobowe.Ulica = model.Ulica_DaneOsobowe;
                            daneOsobowe.NumerUlicy = model.NumerUlicy_DaneOsobowe;
                            daneOsobowe.Miejscowosc = model.Miejscowosc_DaneOsobowe;
                            daneOsobowe.Pesel = model.Pesel_DaneOsobowe;
                            daneOsobowe.Kraj = model.Kraj_DaneOsobowe;
                            daneOsobowe.KodPocztowy = model.KodPocztowy_DaneOsobowe;
                            daneOsobowe.DataUrodzenia = model.DataUrodzenia_DaneOsobowe;
                            daneOsobowe.Plec = model.Plec_DaneOsobowe;
                            daneOsobowe.Email = model.Email_DaneOsobowe;
                            daneOsobowe.RodzajTransakcji = model.RodzajTransakcji_DaneOsobowe;


                            _context.Entry(firma).State = EntityState.Modified;
                            await _context.SaveChangesAsync();
                        }


                        firma.Kraj = model.Kraj;
                        firma.Miasto = model.Miasto;
                        firma.NazwaFirmy = model.NazwaFirmy;
                        firma.NIP = model.NIP;
                        firma.Ulica = model.Ulica;
                        firma.NumerUlicy = model.NumerUlicy;
                        firma.Powiat = model.Powiat;
                        firma.Regon = model.Regon;

                        _context.Entry(firma).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        // Dodanie zdjęcia
                        await CreateNewPhoto(model.Files, firma.FirmaId);

                        model.Success = true;
                    }
                    else
                    {
                        model.Result = "PhotoClient is null.";
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
                var firma = await _context.Firmy.FirstOrDefaultAsync(f => f.FirmaId == id);
                if (firma != null)
                {
                    // usunięcie danych osobowych
                    var daneOsobowe = await _context.DaneOsobowe.FirstOrDefaultAsync(f => f.DaneOsoboweId == firma.WlascicielId);
                    _context.DaneOsobowe.Remove(daneOsobowe);

                    _context.Firmy.Remove(firma);

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



        private async Task CreateNewPhoto(List<IFormFile> files, string firmaId)
        {
            try
            {
                if (files != null && files.Count > 0 && !string.IsNullOrEmpty(firmaId))
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

                                PhotoOwner photoFirma = new PhotoOwner()
                                {
                                    PhotoFirmaId = Guid.NewGuid().ToString(),
                                    PhotoData = photoData,
                                    FirmaId = firmaId
                                };
                                _context.PhotosFirma.Add(photoFirma);
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
