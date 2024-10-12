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
            if (model != null && model.Towar != null)
            {
                try
                {
                    Towar towar = new Towar()
                    {
                        TowarId = Guid.NewGuid().ToString(),
                        Nazwa = model.Towar.Nazwa,
                        Opis = model.Towar.Opis,
                        Cena = model.Towar.Cena,
                        Ilosc = model.Towar.Ilosc,
                        Rabat = model.Towar.Rabat,
                        Kolor = model.Towar.Kolor,
                        Przebieg = model.Towar.Przebieg,
                        RokProdukcji = model.Towar.RokProdukcji,
                        Szerokosc = model.Towar.Szerokosc,
                        Wysokosc = model.Towar.Wysokosc,
                        Waga = model.Towar.Waga,
                        RodzajTowaruId = model.Towar.RodzajTowaruId,
                        MarkaId = model.Towar.MarkaId,
                        DataDodania = DateTime.Now.ToString ()
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
            if (model != null && model.Towar != null)
            {
                try
                {
                    var towar = await _context.Towary.FirstOrDefaultAsync(f => f.TowarId == model.Towar.TowarId);
                    if (towar != null)
                    {
                        towar.Cena = model.Towar.Cena;
                        towar.Kolor = model.Towar.Kolor;
                        towar.Nazwa = model.Towar.Nazwa;
                        towar.Opis = model.Towar.Opis;
                        towar.Przebieg = model.Towar.Przebieg;
                        towar.Szerokosc = model.Towar.Szerokosc;
                        towar.Ilosc = model.Towar.Ilosc;
                        towar.Waga = model.Towar.Waga;
                        towar.Wysokosc = model.Towar.Wysokosc;
                        towar.RokProdukcji = model.Towar.RokProdukcji;
                        towar.Rabat = model.Towar.Rabat;
                        towar.MarkaId = model.Towar.MarkaId;
                        towar.RodzajTowaruId = model.Towar.RodzajTowaruId;

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
