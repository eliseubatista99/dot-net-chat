namespace DotNetChatApi.Contracts
{
    public class BaseOperationOutputMetadataDto
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";
    }

    public class BaseOperationOutputDto<T>
    {
        public T? Data { get; set; }
        public BaseOperationOutputMetadataDto? Metadata { get; set; }

        public BaseOperationOutputDto()
        {
            Data = default;
            Metadata = new BaseOperationOutputMetadataDto
            {
                Success = true,
                Message = "",
            };
        }

        public void SetData(T data)
        {
            Data = data;
        }

        public void SetError(string error)
        {
            Metadata.Success = false;
            Metadata.Message = error;
        }
    }
}
