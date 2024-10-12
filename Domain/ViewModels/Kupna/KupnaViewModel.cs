using Domain.Models;

namespace Domain.ViewModels.Kupna
{
    public class KupnaViewModel : BaseViewModel<Kupno>
    {
        public List<Kupno> Kupna { get; set; }
    }
}
