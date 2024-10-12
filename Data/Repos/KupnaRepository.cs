using Data.Repos.Abs;
using Domain.Models;
using Domain.ViewModels.Kupna;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class KupnaRepository : IKupnaRepository
    {

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
            return await _context.Kupna
                .Include(i => i.Owner)
                .Include(i => i.Client)
                .Include(i => i.Towar)
                .FirstOrDefaultAsync(f => f.KupnoId == id);
        }


        public async Task<KupnoViewModel> Create(KupnoViewModel model)
        {
            if (model != null)
            {
                try
                {
                    // znalezienie danych ownera
                    Owner owner = await _context.Owners
                        .Include(i => i.DaneOsobowe)
                        .FirstOrDefaultAsync(f => f.DaneOsobowe.Firma_Nazwa == "FirmaWlasciciel");

                    if (owner != null)
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



                        Towar towar = new Towar()
                        {
                            TowarId = Guid.NewGuid().ToString(),
                            Nazwa = model.Towar.Nazwa,
                            Opis = model.Towar.Opis,
                            Cena = model.Towar.Cena,
                            Ilosc = model.Towar.Ilosc,
                            Kolor = model.Towar.Kolor,
                            Wysokosc = model.Towar.Wysokosc,
                            Szerokosc = model.Towar.Szerokosc,
                            Waga = model.Towar.Waga,
                            Przebieg = model.Towar.Przebieg,
                            RokProdukcji = model.Towar.RokProdukcji,
                            Rabat = model.Towar.Rabat,
                            MarkaId = model.Towar.MarkaId,
                            RodzajTowaruId = model.Towar.RodzajTowaruId,
                            DataDodania = model.Towar.DataDodania
                        };
                        _context.Towary.Add(towar);
                        await _context.SaveChangesAsync();


                        // dodanie kupna do bazy

                        Kupno kupno = new Kupno()
                        {
                            KupnoId = Guid.NewGuid().ToString(),
                            CenaZakupu = model.Kupno.CenaZakupu,
                            CenaSprzedazy = model.Kupno.CenaSprzedazy,
                            DodatkoweInformacje = model.Kupno.DodatkoweInformacje,
                            OwnerId = owner.OwnerId,
                            ClientId = clientKupujacy.ClientId,
                            TowarId = towar.TowarId,
                            DataZakupu = DateTime.Now.ToString()
                        };
                        _context.Kupna.Add(kupno);
                        await _context.SaveChangesAsync();


                        // Dodanie zdjęcia
                        await CreateNewPhoto(model.Files, kupno.KupnoId);

                        model.Success = true;

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



        public async Task<KupnoViewModel> Update(KupnoViewModel model)
        {
            if (model != null)
            {
                try
                {
                    // pobranie Kupna z bazy danych oraz danych właściciela wraz z danymi osobowymi
                    var kupno = await _context.Kupna
                        .Include(i => i.Owner)
                            .ThenInclude(t => t.DaneOsobowe)
                        .Include(i => i.Client)
                            .ThenInclude(t => t.DaneOsobowe)
                        .FirstOrDefaultAsync(f => f.KupnoId == model.Kupno.KupnoId);

                    if (kupno != null)
                    {
                        // pobranie danych klienta wraz z danymi osobowymi
                        var client = kupno.Client;
                        if (client != null)
                        {
                            var daneOsoboweClient = kupno.Client.DaneOsobowe;
                            if (daneOsoboweClient != null)
                            {
                                // aktualizacja danych osobowych klienta

                                daneOsoboweClient.Imie = model.DaneOsobowe.Imie;
                                daneOsoboweClient.Nazwisko = model.DaneOsobowe.Nazwisko;
                                daneOsoboweClient.Ulica = model.DaneOsobowe.Ulica;
                                daneOsoboweClient.NumerUlicy = model.DaneOsobowe.NumerUlicy;
                                daneOsoboweClient.Miejscowosc = model.DaneOsobowe.Miejscowosc;
                                daneOsoboweClient.Kraj = model.DaneOsobowe.Kraj;
                                daneOsoboweClient.Powiat = model.DaneOsobowe.Powiat;
                                daneOsoboweClient.KodPocztowy = model.DaneOsobowe.KodPocztowy;
                                daneOsoboweClient.Pesel = model.DaneOsobowe.Pesel;
                                daneOsoboweClient.DataUrodzenia = model.DaneOsobowe.DataUrodzenia;
                                daneOsoboweClient.Email = model.DaneOsobowe.Email;
                                daneOsoboweClient.Telefon = model.DaneOsobowe.Telefon;
                                daneOsoboweClient.Plec = model.DaneOsobowe.Plec;
                                daneOsoboweClient.RodzajOsoby = model.DaneOsobowe.RodzajOsoby;

                                daneOsoboweClient.Firma_Nazwa = model.DaneOsobowe.Firma_Nazwa;
                                daneOsoboweClient.Firma_NIP = model.DaneOsobowe.Firma_NIP;
                                daneOsoboweClient.Firma_Regon = model.DaneOsobowe.Firma_Regon;
                                daneOsoboweClient.Firma_Ulica = model.DaneOsobowe.Firma_Ulica;
                                daneOsoboweClient.Firma_NumerUlicy = model.DaneOsobowe.Firma_NumerUlicy;
                                daneOsoboweClient.Firma_Miejscowosc = model.DaneOsobowe.Firma_Miejscowosc;
                                daneOsoboweClient.Firma_KodPocztowy = model.DaneOsobowe.Firma_KodPocztowy;
                                daneOsoboweClient.Firma_Powiat = model.DaneOsobowe.Firma_Powiat;
                                daneOsoboweClient.Firma_Kraj = model.DaneOsobowe.Firma_Kraj;

                                daneOsoboweClient.DataDodania = model.DaneOsobowe.DataDodania;


                                _context.Entry(daneOsoboweClient).State = EntityState.Modified;
                            }
                        }



                        // aktualizacja danych kupna

                        kupno.CenaZakupu = model.Kupno.CenaZakupu;
                        kupno.CenaSprzedazy = model.Kupno.CenaSprzedazy;
                        kupno.DodatkoweInformacje = model.Kupno.DodatkoweInformacje;
                        kupno.DataZakupu = model.Kupno.DataZakupu;
                        _context.Entry(kupno).State = EntityState.Modified;



                        // zapisanie zmodyfikowanych danych w bazie
                        await _context.SaveChangesAsync();


                        // Dodanie nowych zdjęć
                        await CreateNewPhoto(model.Files, kupno.KupnoId);


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

    }
}
