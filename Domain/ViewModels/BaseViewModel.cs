using Data.Services;

namespace Domain.ViewModels
{
    public class BaseViewModel<T>
    {

        // Wyszukiwarka  
        public string q { get; set; }
        public string FiltrowanieOption { get; set; }
        public string SortowanieOption { get; set; }


        // Paginator
        public Paginator<T> Paginator { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int IlePokazac { get; set; } = 0;
        public int Start { get; set; } = 0;
        public int End { get; set; } = 0;
    }
}
