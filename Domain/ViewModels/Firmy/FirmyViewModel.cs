using Domain.Models;

namespace Domain.ViewModels.Firmy
{
    public class FirmyViewModel : BaseViewModel<Firma>
    {
        public List<Firma> Firmy { get; set; }
    }
}
