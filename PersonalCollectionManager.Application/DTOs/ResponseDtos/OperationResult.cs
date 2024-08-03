namespace PersonalCollectionManager.Application.DTOs.ResponseDtos
{
    //public class OperationResult
    //{
    //    public bool Success { get; set; }
    //    public string Message { get; set; }

    //    public OperationResult(bool success, string message)
    //    {
    //        Success = success;
    //        Message = message;
    //    }
    //}

    public record OperationResult(bool Success, string Message = null, string Token = null, string RefreshToken = null, string PrefferedLanguage = null,bool PrefferedThemDark = false);
}
