using Data.Repos.Abs;
using Domain.Models;
using Domain.ViewModels.Towary;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class TowaryRepository : ITowaryRepository
    {
        private readonly ApplicationDbContext _context;
        public TowaryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Towar>> GetAll()
        {
            return await _context.Towary.ToListAsync();
        }

        public async Task<Towar> Get(string id)
        {
            return await _context.Towary.FirstOrDefaultAsync(f => f.TowarId == id);
        }


        public async Task<TowarViewModel> Create(TowarViewModel model)
        {
            if (model != null)
            {
                try
                {
                    Towar towar = new Towar()
                    {
                        TowarId = Guid.NewGuid().ToString(),
                        Cena = model.Cena,
                        Kolor = model.Kolor,
                        Nazwa = model.Nazwa,
                        Opis = model.Opis,
                        Przebieg = model.Przebieg,
                        Szerokosc = model.Szerokosc,
                        Ilosc = model.Ilosc,
                        Waga = model.Waga,
                        Wysokosc = model.Wysokosc,
                        RokProdukcji = model.RokProdukcji,
                        Rabat = model.Rabat,
                        MarkaId = model.MarkaId,
                        RodzajTowaruId = model.RodzajTowaruId,
                        DataDodania = model.DataDodania
                    };
                    _context.Towary.Add(towar);
                    await _context.SaveChangesAsync();
                    model.Success = true;

                    // Dodanie zdjęcia
                    await CreateNewPhoto(model.Files, towar.TowarId);

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



        public async Task<TowarViewModel> Update(TowarViewModel model)
        {
            if (model != null)
            {
                try
                {
                    var towar = await _context.Towary.FirstOrDefaultAsync(f => f.TowarId == model.TowarId);
                    if (towar != null)
                    {
                        towar.Cena = model.Cena;
                        towar.Kolor = model.Kolor;
                        towar.Nazwa = model.Nazwa;
                        towar.Opis = model.Opis;
                        towar.Przebieg = model.Przebieg;
                        towar.Szerokosc = model.Szerokosc;
                        towar.Ilosc = model.Ilosc;
                        towar.Waga = model.Waga;
                        towar.Wysokosc = model.Wysokosc;
                        towar.RokProdukcji = model.RokProdukcji;
                        towar.Rabat = model.Rabat;
                        towar.MarkaId = model.MarkaId;
                        towar.RodzajTowaruId = model.RodzajTowaruId;

                        _context.Entry(towar).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        // Dodanie zdjęcia
                        await CreateNewPhoto(model.Files, towar.TowarId);

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
                var towar = await _context.Towary.FirstOrDefaultAsync(f => f.TowarId == id);
                if (towar != null)
                {
                    // usunięcie zdjęc
                    var photosTowar = await _context.PhotosTowar.Where(w => w.TowarId == towar.TowarId).ToListAsync();
                    foreach (var photoTowar in photosTowar)
                        _context.PhotosTowar.Remove(photoTowar);

                    _context.Towary.Remove(towar);

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



        private async Task CreateNewPhoto(List<IFormFile> files, string towarId)
        {
            try
            {
                if (files != null && files.Count > 0 && !string.IsNullOrEmpty(towarId))
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

                                PhotoTowar photoTowar = new PhotoTowar()
                                {
                                    PhotoTowarId = Guid.NewGuid().ToString(),
                                    PhotoData = photoData,
                                    TowarId = towarId
                                };

                                _context.PhotosTowar.Add(photoTowar);
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
