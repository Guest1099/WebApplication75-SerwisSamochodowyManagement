using Domain.Models;

namespace Domain.ViewModels.Towary
{
    public class TowaryViewModel : BaseViewModel<Towar>
    {
        public List<Towar> Towary { get; set; }
    }
}
