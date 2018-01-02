using GamePool.PL.MVC.Models.Product;
using GamePool.PL.MVC.Models.Shared;

namespace GamePool.PL.MVC.Models.Search
{
    public class SearchResultsVm
    {
        public SearchParametersVm Parameters { get; set; }

        public PagedItems<GamePreviewVm> Items { get; set; }
    }
}