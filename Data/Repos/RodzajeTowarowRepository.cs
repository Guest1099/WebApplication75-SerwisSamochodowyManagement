using Data.Repos.Abs;
using Domain.Models;
using Domain.ViewModels.RodzajeTowarow;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class RodzajeTowarowRepository : IRodzajeTowarowRepository
    {
        private readonly ApplicationDbContext _context;
        public RodzajeTowarowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RodzajTowaru>> GetAll()
        {
            return await _context.RodzajeTowarow.ToListAsync();
        }

        public async Task<RodzajTowaru> Get(string id)
        {
            return await _context.RodzajeTowarow.FirstOrDefaultAsync(f => f.RodzajTowaruId == id);
        }


        public async Task<RodzajTowaruViewModel> Create(RodzajTowaruViewModel model)
        {
            if (model != null)
            {
                try
                {
                    RodzajTowaru rodzajTowaru = new RodzajTowaru()
                    {
                        RodzajTowaruId = Guid.NewGuid().ToString(),
                        Name = model.RodzajTowaru.Name
                    };

                    _context.RodzajeTowarow.Add(rodzajTowaru);
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



        public async Task<RodzajTowaruViewModel> Update(RodzajTowaruViewModel model)
        {
            if (model != null)
            {
                try
                {
                    var rodzajTowaru = await _context.RodzajeTowarow.FirstOrDefaultAsync(f => f.RodzajTowaruId == model.RodzajTowaru.RodzajTowaruId);
                    if (rodzajTowaru != null)
                    {
                        rodzajTowaru.Name = model.RodzajTowaru.Name;

                        _context.Entry(rodzajTowaru).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        model.Success = true;
                    }
                    else
                    {
                        model.Result = "RodzajTowaru is null.";
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
                var rodzajTowaru = await _context.RodzajeTowarow.FirstOrDefaultAsync(f => f.RodzajTowaruId == id);
                if (rodzajTowaru != null)
                {
                    _context.RodzajeTowarow.Remove(rodzajTowaru);
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
