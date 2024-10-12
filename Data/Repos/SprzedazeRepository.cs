using Data.Repos.Abs;
using Domain.Models;
using Domain.ViewModels.Sprzedaze;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class SprzedazeRepository : ISprzedazeRepository
    {
        private readonly ApplicationDbContext _context;
        public SprzedazeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sprzedaz>> GetAll()
        {
            return await _context.Sprzedaze
                .Include(i => i.Towar)
                .Include(i => i.PhotosSprzedaz)
                .ToListAsync();
        }

        public async Task<Sprzedaz> Get(string id)
        {
            return await _context.Sprzedaze
                .Include(i => i.Client)
                    .ThenInclude (t=> t.DaneOsobowe)
                .Include(i => i.Towar)
                .Include(i => i.PhotosSprzedaz)
                .FirstOrDefaultAsync(f => f.SprzedazId == id);
        }


        public async Task<SprzedazViewModel> Create(SprzedazViewModel model)
        {
            if (model != null)
            {
                try
                {

                    // znalezienie danych ownera
                    Owner owner = await _context.Owners
                        .Include(i => i.DaneOsobowe)
                        .FirstOrDefaultAsync(f => f.DaneOsobowe.Firma_Nazwa == "FirmaWlasciciel");

                    if (owner != null && owner.DaneOsobowe != null)
                    {

                        // stworzenie danych kupującego
                        DaneOsobowe daneOsoboweClient = new DaneOsobowe()
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
                        _context.DaneOsobowe.Add(daneOsoboweClient);
                        await _context.SaveChangesAsync();



                        // dodanie Clienta do bazy razem danymi osobowymi
                        Client clientKupujacy = new Client()
                        {
                            ClientId = Guid.NewGuid().ToString(),
                            DaneOsoboweId = daneOsoboweClient.DaneOsoboweId
                        };
                        _context.Clients.Add(clientKupujacy);
                        await _context.SaveChangesAsync();


                        // znalezienie towaru
                        var towar = await _context.Towary.FirstOrDefaultAsync(f => f.TowarId == model.TowarId);
                        if (towar != null)
                        {

                            Sprzedaz sprzedaz = new Sprzedaz()
                            {
                                SprzedazId = Guid.NewGuid().ToString(),
                                Opis = model.Sprzedaz.Opis,
                                CenaSprzedazyBrutto23 = model.Sprzedaz.CenaSprzedazyBrutto23,
                                CenaSprzedazyNetto23 = model.Sprzedaz.CenaSprzedazyNetto23,
                                CenaZakupu = model.Sprzedaz.CenaZakupu,
                                VatBrutto23 = model.Sprzedaz.VatBrutto23,
                                VatNetto23 = model.Sprzedaz.VatNetto23,
                                ZyskBrutto = model.Sprzedaz.ZyskBrutto,
                                ZyskNetto = model.Sprzedaz.ZyskNetto,
                                Sztuk = model.Sprzedaz.Sztuk,
                                Rabat = model.Sprzedaz.Rabat,
                                OwnerId = owner.OwnerId,
                                ClientId = clientKupujacy.ClientId,
                                TowarId = towar.TowarId,
                                DodatkoweInformacje = model.Sprzedaz.DodatkoweInformacje,
                                DataSprzedazy = model.Sprzedaz.DataSprzedazy,
                            };
                            _context.Sprzedaze.Add(sprzedaz);
                            await _context.SaveChangesAsync();



                            // Dodanie zdjęcia
                            await CreateNewPhoto(model.Files, sprzedaz.SprzedazId);


                            model.Success = true;
                        }
                        else
                        {
                            model.Result = "Towar nie został znaleziony";
                        }
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



        public async Task<SprzedazViewModel> Update(SprzedazViewModel model)
        {
            if (model != null)
            {
                try
                {
                    var sprzedaz = await _context.Sprzedaze
                        .Include(i => i.Client)
                            .ThenInclude(t => t.DaneOsobowe)
                        .Include(i => i.Towar)
                        .FirstOrDefaultAsync(f => f.SprzedazId == model.Sprzedaz.SprzedazId);

                    if (sprzedaz != null)
                    {

                        var client = sprzedaz.Client;

                        sprzedaz.Opis = model.Sprzedaz.Opis;
                        sprzedaz.CenaSprzedazyBrutto23 = model.Sprzedaz.CenaSprzedazyBrutto23;
                        sprzedaz.CenaZakupu = model.Sprzedaz.CenaZakupu;
                        sprzedaz.VatBrutto23 = model.Sprzedaz.VatBrutto23;
                        sprzedaz.VatNetto23 = model.Sprzedaz.VatNetto23;
                        sprzedaz.ZyskBrutto = model.Sprzedaz.ZyskNetto;
                        sprzedaz.ZyskNetto = model.Sprzedaz.ZyskNetto;
                        sprzedaz.Sztuk = model.Sprzedaz.Sztuk;
                        sprzedaz.Rabat = model.Sprzedaz.Rabat;
                        sprzedaz.OwnerId = model.Sprzedaz.OwnerId;
                        sprzedaz.ClientId = model.Sprzedaz.ClientId;
                        sprzedaz.TowarId = model.Sprzedaz.TowarId;
                        sprzedaz.DodatkoweInformacje = model.Sprzedaz.DodatkoweInformacje;
                        sprzedaz.DataSprzedazy = model.Sprzedaz.DataSprzedazy;

                        _context.Entry(sprzedaz).State = EntityState.Modified;
                        await _context.SaveChangesAsync();


                        var towar = sprzedaz.Towar;
                        if (towar != null)
                        {

                        }

                        // Dodanie zdjęcia
                        await CreateNewPhoto(model.Files, sprzedaz.SprzedazId);

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
                var sprzedaz = await _context.Sprzedaze.FirstOrDefaultAsync(f => f.SprzedazId == id);
                if (sprzedaz != null)
                {
                    // usunięcie zdjec
                    var photosSprzedaz = await _context.PhotosSprzedaz.Where(w => w.SprzedazId == sprzedaz.SprzedazId).ToListAsync();
                    foreach (var photoSprzedaz in photosSprzedaz)
                        _context.PhotosSprzedaz.Remove(photoSprzedaz);

                    _context.Sprzedaze.Remove(sprzedaz);
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



        private async Task CreateNewPhoto(List<IFormFile> files, string sprzedazId)
        {
            try
            {
                if (files != null && files.Count > 0 && !string.IsNullOrEmpty(sprzedazId))
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

                                PhotoSprzedaz photoSprzedaz = new PhotoSprzedaz()
                                {
                                    PhotoSprzedazId = Guid.NewGuid().ToString(),
                                    PhotoData = photoData,
                                    SprzedazId = sprzedazId
                                };

                                _context.PhotosSprzedaz.Add(photoSprzedaz);
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
