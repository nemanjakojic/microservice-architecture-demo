namespace Demo.Microservice.Core.Common.Model
{
    public class PagedResult<T>
    {
        public T Items { get; set; }
        public int Total { get; set; }
    }
}
