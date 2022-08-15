namespace FinancialSnapshot.Models.Web.General
{
    public class BaseDataResponse<E>: BaseDataRequest<E>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public static BaseDataResponse<E> Success(string message = default, E data = default)
        {
            return new BaseDataResponse<E> { IsSuccess = true, Message = message, Data = data };
        }

        public static BaseDataResponse<E> Error(string message = default)
        {
            return new BaseDataResponse<E> { IsSuccess = false, Message = message };
        }
    }
}
