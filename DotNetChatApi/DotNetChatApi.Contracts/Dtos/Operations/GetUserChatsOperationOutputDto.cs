namespace DotNetChatApi.Contracts
{
    public class GetUserChatsOperationOutputDto
    {
        public required List<ChatDto> Chats { get; set; }
    }
}
