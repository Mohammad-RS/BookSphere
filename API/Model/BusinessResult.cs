namespace App.Model
{
    public class BusinessResult<T>
    {
        public bool Success { get; set; }

        public T Data { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public void SetError(int errorCode, string errorMessage)
        {
            Success = false;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public void SetData(T data)
        {
            Success = true;
            Data = data;
        }
    }
}
