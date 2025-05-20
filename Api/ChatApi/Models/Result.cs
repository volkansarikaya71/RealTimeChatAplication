namespace ChatApi.Models
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> ErrorMessages { get; set; }

        public Result(T data)
        {
            Success = true;
            Data = data;
        }

        public Result(List<string> errorMessages)
        {
            Success = false;
            ErrorMessages = errorMessages;
        }
    }
}
