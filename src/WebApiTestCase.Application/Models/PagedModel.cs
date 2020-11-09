namespace WebApiTestCase.Application.Models
{
    public class PagedModel
    {
        public int CurrentPage { get; set; } = 0;
        public int PageSize { get; set; } = 5;
    }
}