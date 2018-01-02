namespace GamePool.PL.MVC.Models.Shared
{
    public class PageInfo
    {
        public int CurrentPage { get; set; }

        public int StartIndex { get; set; }

        public int PageLinksCount { get; set; }

        public int TotalPages { get; set; }
    }
}