using Data.Repos.Abs;
using Domain.Models;
using Domain.ViewModels.Clients;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly ApplicationDbContext _context;
        public ClientsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAll()
        {
            return await _context.Clients
                .Include (i=> i.DaneOsobowe)
                .ToListAsync();
        }

        public async Task<Client> Get(string id)
        {
            return await _context.Clients
                .Include (i=> i.DaneOsobowe)
                .Include (i=> i.Kupna)
                .FirstOrDefaultAsync(f => f.ClientId == id);
        }


        public async Task<ClientViewModel> Create(ClientViewModel model)
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

                    Client client = new Client()
                    {
                        ClientId = Guid.NewGuid().ToString(),
                        DaneOsoboweId = daneOsobowe.DaneOsoboweId
                    };
                    _context.DaneOsobowe.Add(daneOsobowe);
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



        public async Task<ClientViewModel> Update(ClientViewModel model)
        {
            if (model != null)
            {
                try
                {
                    var client = await _context.Clients
                        .Include(i => i.DaneOsobowe)
                        .FirstOrDefaultAsync(f => f.ClientId == model.Client.ClientId);


                    if (client != null)
                    {

                        // aktualizacja danych osobowych klienta

                        var daneOsobowe = client.DaneOsobowe;
                        if (daneOsobowe != null)
                        {

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
                            await _context.SaveChangesAsync();


                            model.Success = true;
                        }
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
                var client = await _context.Clients.FirstOrDefaultAsync(f => f.ClientId == id);
                if (client != null)
                {
                    _context.Clients.Remove(client);
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
