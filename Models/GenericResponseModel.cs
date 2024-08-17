namespace RestFullWebApi.Models
{
    public class GenericResponseModel<T>
    {
        public int StatusCode { get; set; }
        public T Data { get; set; }

    }
}
