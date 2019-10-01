namespace RP.App.Common
{
    public class Response<T> where T : class
    {
        public T Value { get; }
        public bool IsSuccess { get; }
        public string FailureMessage { get; }

        private Response(T value, bool isSuccess, string failureMessage)
        {
            Value = value;
            IsSuccess = isSuccess;
            FailureMessage = failureMessage;
        }
        
        public static Response<T> Failed(string reason)
        {
            return new Response<T>(null, false, reason);
        }

        public static Response<T> Success(T value)
        {
            return new Response<T>(value, true, string.Empty);
        }
    }
}