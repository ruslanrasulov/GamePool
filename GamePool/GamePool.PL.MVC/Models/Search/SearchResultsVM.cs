using GamePool.PL.MVC.Models.Product;
using GamePool.PL.MVC.Models.Shared;

namespace GamePool.PL.MVC.Models.Search
{
    public class SearchResultsVM
    {
        public SearchParametersVM Parameters { get; set; }

        public PagedItems<GamePreviewVM> Items { get; set; }
    }
}