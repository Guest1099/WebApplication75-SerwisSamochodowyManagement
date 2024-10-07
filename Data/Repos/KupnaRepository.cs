using Domain.Models;
using Domain.ViewModels.Kupna;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class KupnaRepository //: IKupnaRepository
    {
/*
        private readonly ApplicationDbContext _context;
        public KupnaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Kupno>> GetAll()
        {
            return await _context.Kupna.ToListAsync();
        }

        public async Task<Kupno> Get(string id)
        {
            return await _context.Kupna.FirstOrDefaultAsync(f => f.KupnoId == id);
        }


        public async Task<KupnoViewModel> Create(KupnoViewModel model)
        {
            if (model != null)
            {
                try
                {
                    Firma firmaKupujacy = await _context.Firmy
                        .Include(i => i.Wlasciciel)
                        .FirstOrDefaultAsync(f => f.NazwaFirmy == "FirmaWlasciciel");
                    DaneOsobowe daneOsoboweKupujacy = firmaKupujacy.Wlasciciel;


                    DaneOsobowe daneOsoboweSprzedajacy = new DaneOsobowe()
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
                    _context.DaneOsobowe.Add(daneOsoboweSprzedajacy);
                    await _context.SaveChangesAsync();

                    Firma firmaSprzedajacy = new Firma()
                    {
                        FirmaId = Guid.NewGuid().ToString(),
                        Kraj = model.Kupno.FirmaSprzedajacy.Kraj,
                        Miasto = model.Kupno.FirmaSprzedajacy.Miasto,
                        NazwaFirmy = model.Kupno.FirmaSprzedajacy.NazwaFirmy,
                        NIP = model.Kupno.FirmaSprzedajacy.NIP,
                        Ulica = model.Kupno.FirmaSprzedajacy.Ulica,
                        NumerUlicy = model.Kupno.FirmaSprzedajacy.NumerUlicy,
                        Powiat = model.Kupno.FirmaSprzedajacy.Powiat,
                        Regon = model.Kupno.FirmaSprzedajacy.Regon,
                        WlascicielId = model.Kupno.FirmaSprzedajacy.WlascicielId
                    };
                    _context.Firmy.Add(firmaSprzedajacy);
                    await _context.SaveChangesAsync();



                    Kupno kupno = new Kupno()
                    {
                        KupnoId = Guid.NewGuid().ToString(),
                        FirmaKupujacyId = firmaKupujacy.FirmaId,
                        FirmaSprzedajacyId = firmaSprzedajacy.FirmaId,
                        DaneOsoboweSprzedajacyId = daneOsoboweSprzedajacy.DaneOsoboweId,
                        TowarId = model.Kupno.TowarId,
                        DataZakupu = DateTime.Now
                    };

                    _context.Kupna.Add(kupno);
                    await _context.SaveChangesAsync();
                    model.Success = true;


                    // Dodanie zdjęcia
                    await CreateNewPhoto(model.Files, kupno.KupnoId);

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



        public async Task<KupnoViewModel> Update(KupnoViewModel model)
        {
            if (model != null)
            {
                try
                {
                    var kupno = await _context.Kupna
                        .Include(i => i.FirmaKupujacy)
                            .ThenInclude(t => t.Wlasciciel)
                        .FirstOrDefaultAsync(f => f.KupnoId == model.Kupno.KupnoId);

                    if (kupno != null)
                    {
                        var firmaSprzedajacy = await _context.Firmy.FirstOrDefaultAsync(f => f.FirmaId == kupno.FirmaSprzedajacyId);
                        if (firmaSprzedajacy != null)
                        {
                            firmaSprzedajacy.Kraj = model.Kupno.FirmaSprzedajacy.Kraj;
                            firmaSprzedajacy.Miasto = model.Kupno.FirmaSprzedajacy.Miasto;
                            firmaSprzedajacy.NazwaFirmy = model.Kupno.FirmaSprzedajacy.NazwaFirmy;
                            firmaSprzedajacy.NIP = model.Kupno.FirmaSprzedajacy.NIP;
                            firmaSprzedajacy.Ulica = model.Kupno.FirmaSprzedajacy.Ulica;
                            firmaSprzedajacy.NumerUlicy = model.Kupno.FirmaSprzedajacy.NumerUlicy;
                            firmaSprzedajacy.Powiat = model.Kupno.FirmaSprzedajacy.Powiat;
                            firmaSprzedajacy.Regon = model.Kupno.FirmaSprzedajacy.Regon;
                            firmaSprzedajacy.WlascicielId = model.Kupno.FirmaSprzedajacy.WlascicielId;
                        }


                        var daneOsoboweSprzedajacy = await _context.DaneOsobowe.FirstOrDefaultAsync(f => f.DaneOsoboweId == kupno.DaneOsoboweSprzedajacyId);
                        if (daneOsoboweSprzedajacy != null)
                        {
                            daneOsoboweSprzedajacy.Imie = model.Imie_DaneOsobowe;
                            daneOsoboweSprzedajacy.Nazwisko = model.Nazwisko_DaneOsobowe;
                            daneOsoboweSprzedajacy.Telefon = model.Telefon_DaneOsobowe;
                            daneOsoboweSprzedajacy.Ulica = model.Ulica_DaneOsobowe;
                            daneOsoboweSprzedajacy.NumerUlicy = model.NumerUlicy_DaneOsobowe;
                            daneOsoboweSprzedajacy.Miejscowosc = model.Miejscowosc_DaneOsobowe;
                            daneOsoboweSprzedajacy.Pesel = model.Pesel_DaneOsobowe;
                            daneOsoboweSprzedajacy.Kraj = model.Kraj_DaneOsobowe;
                            daneOsoboweSprzedajacy.KodPocztowy = model.KodPocztowy_DaneOsobowe;
                            daneOsoboweSprzedajacy.DataUrodzenia = model.DataUrodzenia_DaneOsobowe;
                            daneOsoboweSprzedajacy.Plec = model.Plec_DaneOsobowe;
                            daneOsoboweSprzedajacy.Email = model.Email_DaneOsobowe;
                            daneOsoboweSprzedajacy.RodzajTransakcji = model.RodzajTransakcji_DaneOsobowe;
                        }


                        kupno.DataZakupu = model.Kupno.DataZakupu;

                        _context.Entry(kupno).State = EntityState.Modified;
                        await _context.SaveChangesAsync();


                        // Dodanie zdjęcia
                        await CreateNewPhoto(model.Files, kupno.FirmaSprzedajacyId);

                        model.Success = true;
                    }
                    else
                    {
                        model.Result = "Kupna is null.";
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
                var kupno = await _context.Kupna.FirstOrDefaultAsync(f => f.KupnoId == id);
                if (kupno != null)
                {
                    // usunięcie danych osobowych
                    var photsKupno = await _context.PhotosKupno.Where(w => w.KupnoId == kupno.KupnoId).ToListAsync();
                    foreach (var photoKupno in photsKupno)
                        _context.PhotosKupno.Remove(photoKupno);

                    _context.Kupna.Remove(kupno);

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



        private async Task CreateNewPhoto(List<IFormFile> files, string kupnoId)
        {
            try
            {
                if (files != null && files.Count > 0 && !string.IsNullOrEmpty(kupnoId))
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

                                PhotoKupno photoKupno = new PhotoKupno()
                                {
                                    PhotoKupnoId = Guid.NewGuid().ToString(),
                                    PhotoData = photoData,
                                    KupnoId = kupnoId
                                };
                                _context.PhotosKupno.Add(photoKupno);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
            catch { }
        }
*/
    }
}
