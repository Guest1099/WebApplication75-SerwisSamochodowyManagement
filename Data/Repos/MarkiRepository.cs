using Data.Repos.Abs;
using Domain.Models;
using Domain.ViewModels.Marki;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Marki.ToListAsync();
        }

        public async Task<Marka> Get(string id)
        {
            return await _context.Marki.FirstOrDefaultAsync(f => f.MarkaId == id);
        }


        public async Task<MarkaViewModel> Create(MarkaViewModel model)
        {
            if (model != null)
            {
                try
                {
                    Marka makra = new Marka()
                    {
                        MarkaId = Guid.NewGuid().ToString(),
                        Name = model.Name
                    };

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



        public async Task<MarkaViewModel> Update(MarkaViewModel model)
        {
            if (model != null)
            {
                try
                {
                    var marka = await _context.Marki.FirstOrDefaultAsync(f => f.MarkaId == model.MarkaId);
                    if (marka != null)
                    {
                        marka.Name = model.Name;

                        _context.Entry(marka).State = EntityState.Modified;
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
                var marka = await _context.Marki.FirstOrDefaultAsync(f => f.MarkaId == id);
                if (marka != null)
                {
                    _context.Marki.Remove(marka);

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
