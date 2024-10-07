using Domain.Models;

namespace Domain.ViewModels.Firmy
{
    public class FirmyViewModel : BaseViewModel<Owner>
    {
        public List<Owner> Firmy { get; set; }
    }
}
