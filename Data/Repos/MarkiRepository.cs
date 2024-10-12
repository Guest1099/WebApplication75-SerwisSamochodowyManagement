using Data.Repos.Abs;
using Domain.Models;
using Domain.ViewModels.Marki;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Data.Repos
{
    public class MarkiRepository : IMarkiRepository
    {
        private readonly ApplicationDbContext _context;
        public MarkiRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Marka>> GetAll()
        {
            return await _context.Marki
                .Include(i => i.Towary)
                .ToListAsync();
        }

        public async Task<Marka> Get(string id)
        {
            return await _context.Marki.FirstOrDefaultAsync(f => f.MarkaId == id);
        }


        public async Task<MarkaViewModel> Create(MarkaViewModel model)
        {
            if (model != null && model.Marka != null)
            {
                try
                {

                    // sprawdza po nazwie czy marka już istnieje, jeżeli nie to tworzy nową markę jeśli tak to wyświetla komunikat
                    var marka = await _context.Marki.FirstOrDefaultAsync(f => f.Name == model.Marka.Name);
                    if (marka == null)
                    {
                        Marka makra = new Marka()
                        {
                            MarkaId = Guid.NewGuid().ToString(),
                            Name = model.Marka.Name
                        };
                        _context.Marki.Add(makra);
                        await _context.SaveChangesAsync();

                        model.Success = true;
                    }
                    else
                    {
                        model.Success = false;
                        model.Result = "Wskazana nazwa marki jest już zajęta. Spróbuj podać inną nazwę";
                    }
                }
                catch (Exception ex)
                {
                    model.Success = false;
                    model.Result = "Catch exception." + ex.Message;
                }
            }
            else
            {
                model.Success = false;
                model.Result = "Model is null.";
            }
            return model;
        }



        /*public async Task<MarkaViewModel> Update(MarkaViewModel model)
        {
            if (model != null && model.Marka != null)
            {
                try
                {
                    var marka = await _context.Marki.FirstOrDefaultAsync(f => f.MarkaId == model.Marka.MarkaId);
                    if (marka != null)
                    {
                        if (marka.Name != model.Marka.Name)
                        {
                            marka.Name = model.Marka.Name;

                            _context.Entry(marka).State = EntityState.Modified;
                            await _context.SaveChangesAsync();


                            model.Success = true;
                        } 
                        else
                        {
                            model.Success = false;
                            model.Result = "Nazwa marki jest już zajęta. Spróbuj podać inną nazwę";
                        }
                    }
                    else
                    {
                        model.Result = "Data is null.";
                        model.Success = false;
                    }
                }
                catch (Exception ex)
                {
                    model.Result = "Catch exception.";
                    model.Success = false;
                }
            }
            else
            {
                model.Result = "Model is null.";
                model.Success = false;
            }
            return model;
        }*/

        public async Task<MarkaViewModel> Update(MarkaViewModel model)
        {
            if (model != null && model.Marka != null)
            {
                try
                {
                    var marka = await _context.Marki.FirstOrDefaultAsync(f => f.MarkaId == model.Marka.MarkaId);
                    if (marka != null)
                    {
                        marka.Name = model.Marka.Name;

                        _context.Entry(marka).State = EntityState.Modified;
                        await _context.SaveChangesAsync();


                        model.Success = true;
                    }
                    else
                    {
                        model.Result = "Data is null.";
                        model.Success = false;
                    }
                }
                catch (Exception ex)
                {
                    model.Result = "Catch exception.";
                    model.Success = false;
                }
            }
            else
            {
                model.Result = "Model is null.";
                model.Success = false;
            }
            return model;
        }



        /*
                public async Task<MarkaViewModel> Delete(string id)
                {
                    MarkaViewModel result = new MarkaViewModel() { Success = true, Result = "" };
                    try
                    {
                        var marka = await _context.Marki.FirstOrDefaultAsync(f => f.MarkaId == id);
                        if (marka == null)
                        {
                            result.Success = false;
                            result.Result = "Model Marka was null";
                        }
                        else
                        {
                            if (marka.Towary != null && marka.Towary.Any())
                            {
                                result.Success = false;
                                result.Result = "Nie można usunąć rekordu poniważ do marki przypisane są relacje";
                            }
                            else
                            {
                                _context.Marki.Remove(marka);

                                await _context.SaveChangesAsync();
                                result.Success = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Result = "Catch exception";
                        result.Success = false;
                    }
                    return result;
                }
        */



        public async Task<bool> Delete(string id)
        {
            bool result;
            try
            {
                var marka = await _context.Marki.FirstOrDefaultAsync(f => f.MarkaId == id);
                if (marka != null)
                {
                    _context.Marki.Remove(marka);
                    await _context.SaveChangesAsync();

                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }



    }
}
