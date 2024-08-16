namespace DotNetChatApi.Contracts
{
    public class GetUserChatsOperationOutputDto
    {
        public required List<Chat> Chats { get; set; }
    }
}
