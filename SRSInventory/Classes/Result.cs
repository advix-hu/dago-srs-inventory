using System.Text;

namespace SRSInventory.Classes
{
    public enum ResultStates
    {
        Ok,
        Failed
    }

    public interface IResult<TStatus> where TStatus : System.Enum
    {
        TStatus ResultState { get; }
        string Message { get; }
    }

    public class Result<TStatus> : IResult<TStatus> where TStatus : System.Enum
    {
        public TStatus ResultState { get; }
        public string Message { get; }

        public Result(TStatus resultState)
        {
            ResultState = resultState;
        }

        public Result(TStatus resultState, string message)
        {
            ResultState = resultState;
            Message = message;
        }

        public Result(TStatus resultState, Exception ex)
        {
            ResultState = resultState;

            var sb = new StringBuilder();
            var e = ex;
            while (e != null)
            {
                sb.Append(e.Message);
                e = e.InnerException;
            }
            Message = sb.ToString();
        }

        public override string ToString()
        {
            return $"State: {ResultState} Message: {Message}";
        }
    }

    public interface IResult<TStatus, TResult> where TStatus : System.Enum
    {
        TStatus ResultState { get; }
        TResult ResultValue { get; }
        string Message { get; }
    }

    public class Result<TStatus, TResult> : IResult<TStatus, TResult> where TStatus : System.Enum
    {
        public TStatus ResultState { get; }
        public TResult ResultValue { get; }
        public string Message { get; }

        public Result(TStatus resultState, TResult result)
        {
            ResultState = resultState;
            ResultValue = result;
        }

        public Result(TStatus resultState, TResult result, string message)
        {
            ResultState = resultState;
            ResultValue = result;
            Message = message;
        }

        public Result(TStatus resultState, TResult result, Exception ex)
        {
            ResultState = resultState;
            ResultValue = result;

            var sb = new StringBuilder();
            var e = ex;
            while (e != null)
            {
                sb.Append(e.Message);
                e = e.InnerException;
            }
            Message = sb.ToString();
        }

        public override string ToString()
        {
            return $"State: {ResultState} Value: {ResultValue} Message: {Message}";
        }
    }
}
