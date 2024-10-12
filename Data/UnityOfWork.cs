using Data.Repos;
using Data.Repos.Abs;

namespace Data
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly ApplicationDbContext _context;

        public IOwnersRepository OwnersRepository { get; set; }
        public IPhotosUserRepository PhotosUserRepository { get; set; }
        public ILoggingErrorsRepository LoggingErrorsRepository { get; set; }
        public IKupnaRepository KupnaRepository { get; set; }
        public ISprzedazeRepository SprzedazeRepository { get; set; }
        public ITowaryRepository TowaryRepository { get; set; }
        public IMarkiRepository MarkiRepository { get; set; }
        public IClientsRepository ClientsRepository { get; set; }
        public IRodzajeTowarowRepository RodzajeTowarowRepository { get; set; }


        public UnityOfWork(ApplicationDbContext context)
        {
            _context = context;


            // przypisanie kontekstu do repozytoriów

            OwnersRepository = new OwnersRepository(_context);
            LoggingErrorsRepository = new LoggingErrorsRepository(_context);
            KupnaRepository = new KupnaRepository(_context);
            SprzedazeRepository = new SprzedazeRepository(_context);
            TowaryRepository = new TowaryRepository(_context);
            MarkiRepository = new MarkiRepository(_context);
            ClientsRepository = new ClientsRepository(_context);
            RodzajeTowarowRepository = new RodzajeTowarowRepository(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
