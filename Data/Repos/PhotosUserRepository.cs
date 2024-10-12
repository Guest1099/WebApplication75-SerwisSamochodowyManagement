using Data.Repos.Abs;
using Domain.Models;
using Domain.ViewModels.PhotosUser;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class PhotosUserRepository : IPhotosUserRepository
    {
        private readonly ApplicationDbContext _context;
        public PhotosUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PhotoUser>> GetAll()
        {
            return await _context.PhotosUser.ToListAsync();
        }

        public async Task<PhotoUser> Get(string id)
        {
            return await _context.PhotosUser.FirstOrDefaultAsync(f => f.PhotoUserId == id);
        }


        public async Task<CreatePhotoUserViewModel> Create(CreatePhotoUserViewModel model)
        {
            if (model != null)
            {
                try
                {
                    PhotoUser photoClient = new PhotoUser()
                    {
                        PhotoUserId = Guid.NewGuid().ToString(),
                        PhotoData = model.PhotoData,
                        UserId = model.UserId
                    };
                    _context.PhotosUser.Add(photoClient);
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



        public async Task<EditPhotoUserViewModel> Update(EditPhotoUserViewModel model)
        {
            if (model != null)
            {
                try
                {
                    var photoClient = await _context.PhotosUser.FirstOrDefaultAsync(f => f.PhotoUserId == model.PhotoUserId);
                    if (photoClient != null)
                    {
                        photoClient.PhotoData = model.PhotoData;

                        _context.Entry(photoClient).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
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
                var photoClient = await _context.PhotosUser.FirstOrDefaultAsync(f => f.PhotoUserId == id);
                if (photoClient != null)
                {
                    _context.PhotosUser.Remove(photoClient);
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
