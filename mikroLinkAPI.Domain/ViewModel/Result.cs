
using System.Text.Json.Serialization;

namespace mikroLinkAPI.Domain.ViewModel
{
    public sealed class Result<T>
    {
        public T Data { get; set; }
        public int IslemId { get; set; }

        public List<string> ErrorMessages { get; set; }

        public bool IsSuccessful { get; set; } = true;
        public byte[] ExcelFile { get; set; }


        [JsonIgnore]
        public int StatusCode { get; set; } = 200;


        public Result(T data)
        {
            Data = data;
        }
        public Result(T data, byte[] excelFile)
        {
            Data = data;
            ExcelFile = excelFile;
        }
        public Result(int statusCode, List<string> errorMessages)
        {
            IsSuccessful = false;
            StatusCode = statusCode;
            ErrorMessages = errorMessages;
        }
        public Result(int statusCode, List<string> errorMessages, byte[] excelFile)
        {
            IsSuccessful = false;
            StatusCode = statusCode;
            ErrorMessages = errorMessages;
            ExcelFile = excelFile;
        }
        public Result(int statusCode, string errorMessage, byte[] excelFile)
        {
            IsSuccessful = false;
            StatusCode = statusCode;
            ErrorMessages = new List<string> { errorMessage };
            ExcelFile = excelFile;
        }
        public Result(int statusCode, string errorMessage)
        {
            IsSuccessful = false;
            StatusCode = statusCode;
            ErrorMessages = new List<string> { errorMessage };
        }

        public static implicit operator Result<T>(T data)
        {
            return new Result<T>(data);
        }

        public static implicit operator Result<T>((int statusCode, List<string> errorMessages) parameters)
        {
            return new Result<T>(parameters.statusCode, parameters.errorMessages);
        }

        public static implicit operator Result<T>((int statusCode, string errorMessage) parameters)
        {
            return new Result<T>(parameters.statusCode, parameters.errorMessage);
        }

        public static Result<T> Succeed(T data)
        {
            return new Result<T>(data);
        }
        public static Result<T> Succeed(T data, byte[] excelFile)
        {
            return new Result<T>(data,excelFile);
        }
        public static Result<T> Failure(int statusCode, List<string> errorMessages)
        {
            return new Result<T>(statusCode, errorMessages);
        }
        public static Result<T> Failure(int statusCode, List<string> errorMessages, byte[] excelFile)
        {
            return new Result<T>(statusCode, errorMessages, excelFile);
        }
        public static Result<T> Failure(int statusCode, string errorMessage)
        {
            return new Result<T>(statusCode, errorMessage);
        }
        public static Result<T> Failure(int statusCode, string errorMessage, byte[] excelFile)
        {
            return new Result<T>(statusCode, errorMessage,excelFile);
        }

        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T>(500, errorMessage);
        }
        public static Result<T> Failure(string errorMessage, byte[] excelFile)
        {
            return new Result<T>(500, errorMessage,excelFile);
        }
        public static Result<T> Failure(List<string> errorMessages, byte[] excelFile)
        {
            return new Result<T>(500, errorMessages,excelFile);
        }
        public static Result<T> Failure(List<string> errorMessages)
        {
            return new Result<T>(500, errorMessages);
        }
    }
}
