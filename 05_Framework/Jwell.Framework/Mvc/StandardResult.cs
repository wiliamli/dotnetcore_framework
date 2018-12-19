namespace Jwell.Framework.Mvc
{
    public class StandardResult : IStandardResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public void Succeed()
        {
            Success = true;
        }

        public void Fail()
        {
            Success = false;
        }

        public void Succeed(string message)
        {
            Success = true;
            Message = message;
        }

        public void Fail(string message)
        {
            Success = false;
            Message = message;
        }
    }

    public class StandardResult<T> : StandardResult, IStandardResult<T>
    {
        public T Data { get; set; }
    }
}
