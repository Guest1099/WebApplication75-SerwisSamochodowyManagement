namespace Data.Repos
{
    public class SprzedazeRepository //: ISprzedazeRepository
    {
        /*private readonly ApplicationDbContext _context;
        public SprzedazeRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sprzedaz>> GetAll ()
        {
            return await _context.Sprzedaze
                .Include(i => i.Towar)
                .Include (i => i.PhotosSprzedaz)
                .ToListAsync();
        }

        public async Task<Sprzedaz> Get (string id)
        {
            return await _context.Sprzedaze
                .Include(i => i.Towar)
                .Include(i => i.PhotosSprzedaz)
                .FirstOrDefaultAsync(f => f.SprzedazId == id);
        }


        public async Task<SprzedazViewModel> Create (SprzedazViewModel model)
        {
            if (model != null)
            {
                try
                {

                    Sprzedaz sprzedaz = new Sprzedaz ()
                    {
                        SprzedazId = Guid.NewGuid ().ToString (), 
                        CenaSprzedazyBrutto22 = model.Sprzedaz.CenaSprzedazyBrutto22,
                        CenaSprzedazyNetto22 = model.Sprzedaz.CenaSprzedazyNetto22, 
                        DataSprzedazy = model.Sprzedaz.DataSprzedazy,
                        ZyskBrutto = model.Sprzedaz.ZyskBrutto,
                        ZyskNetto = model.Sprzedaz.ZyskNetto,
                        FirmaId = model.Sprzedaz.FirmaId,
                        KupujacyId = model.Sprzedaz.KupujacyId,
                        RodzajTowaruId = model.Sprzedaz.RodzajTowaruId
                    };
                    _context.Sprzedaze.Add(sprzedaz);
                    await _context.SaveChangesAsync();
                    model.Success = true;


                    // Dodanie zdjęcia
                    await CreateNewPhoto(model.Files, sprzedaz.SprzedazId);

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



        public async Task<SprzedazViewModel> Update (SprzedazViewModel model)
        {
            if (model != null)
            {
                try
                {
                    var sprzedaz = await _context.Sprzedaze.FirstOrDefaultAsync (f=> f.SprzedazId == model.Sprzedaz.SprzedazId);
                    if (sprzedaz != null)
                    {   
                        sprzedaz.CenaSprzedazyBrutto22 = model.Sprzedaz.CenaSprzedazyBrutto22;
                        sprzedaz.CenaSprzedazyNetto22 = model.Sprzedaz.CenaSprzedazyNetto22;
                        sprzedaz.DataSprzedazy = model.Sprzedaz.DataSprzedazy;
                        sprzedaz.ZyskBrutto = model.Sprzedaz.ZyskBrutto;
                        sprzedaz.ZyskNetto = model.Sprzedaz.ZyskNetto;
                        sprzedaz.FirmaId = model.Sprzedaz.FirmaId;
                        sprzedaz.KupujacyId = model.Sprzedaz.KupujacyId;
                        sprzedaz.RodzajTowaruId = model.Sprzedaz.RodzajTowaruId;

                        _context.Entry(sprzedaz).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

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



        public async Task<bool> Delete (string id)
        {
            bool deleteResult = false;
            try
            {
                var sprzedaz = await _context.Sprzedaze.FirstOrDefaultAsync (f=> f.SprzedazId == id);
                if (sprzedaz != null)
                {
                    // usunięcie zdjec
                    var photosSprzedaz = await _context.PhotosSprzedaz.Where (w=> w.SprzedazId == sprzedaz.SprzedazId).ToListAsync ();
                    foreach (var photoSprzedaz in photosSprzedaz)
                        _context.PhotosSprzedaz.Remove (photoSprzedaz);

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



        private async Task CreateNewPhoto (List<IFormFile> files, string sprzedazId)
        {
            try
            {
                if (files != null && files.Count > 0 && !string.IsNullOrEmpty(sprzedazId))
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            byte [] photoData;
                            using (var stream = new MemoryStream())
                            {
                                file.CopyTo(stream);
                                photoData = stream.ToArray();

                                PhotoSprzedaz photoSprzedaz = new PhotoSprzedaz ()
                                {
                                    PhotoSprzedazId = Guid.NewGuid ().ToString (),
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
*/
    }
}
