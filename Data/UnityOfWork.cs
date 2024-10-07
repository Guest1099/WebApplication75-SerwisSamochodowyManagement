using Data.Repos;
using Data.Repos.Abs;

namespace Data
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly ApplicationDbContext _context;

        public IPhotosUserRepository PhotosUserRepository { get; set; }
        public IFirmyRepository FirmyRepository { get; set; }
        public IKupnaRepository KupnaRepository { get; set; }
        public ISprzedazeRepository SprzedazeRepository { get; set; }
        public ITowaryRepository TowaryRepository { get; set; }


        public UnityOfWork(ApplicationDbContext context)
        {
            _context = context;
/*
            PhotosUserRepository = new PhotosUserRepository(_context);
            FirmyRepository = new FirmyRepository(_context);
            KupnaRepository = new KupnaRepository(_context);
            SprzedazeRepository = new SprzedazeRepository(_context);
            TowaryRepository = new TowaryRepository(_context);*/
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
