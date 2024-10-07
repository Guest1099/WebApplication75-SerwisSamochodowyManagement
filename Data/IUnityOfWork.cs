using Data.Repos.Abs;

namespace Data
{
    public interface IUnityOfWork
    {
        IPhotosUserRepository PhotosUserRepository { get; set; }
        IFirmyRepository FirmyRepository { get; set; }
        IKupnaRepository KupnaRepository { get; set; }
        ISprzedazeRepository SprzedazeRepository { get; set; }
        ITowaryRepository TowaryRepository { get; set; }

        Task SaveChangesAsync();
    }
}
