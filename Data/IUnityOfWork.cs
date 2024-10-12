using Data.Repos.Abs;

namespace Data
{
    public interface IUnityOfWork
    {
        IOwnersRepository OwnersRepository { get; set; }
        IPhotosUserRepository PhotosUserRepository { get; set; }
        ILoggingErrorsRepository LoggingErrorsRepository { get; set; }
        IKupnaRepository KupnaRepository { get; set; }
        ISprzedazeRepository SprzedazeRepository { get; set; }
        ITowaryRepository TowaryRepository { get; set; }
        IMarkiRepository MarkiRepository { get; set; }
        IClientsRepository ClientsRepository { get; set; }
        IRodzajeTowarowRepository RodzajeTowarowRepository { get; set; }


        Task SaveChangesAsync();
    }
}
