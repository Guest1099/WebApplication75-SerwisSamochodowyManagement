using Domain.Models;

namespace Domain.ViewModels.Sprzedaze
{
    public class SprzedazeViewModel : BaseViewModel<Sprzedaz>
    {
        public List<Sprzedaz> Sprzedaze { get; set; }
    }
}
