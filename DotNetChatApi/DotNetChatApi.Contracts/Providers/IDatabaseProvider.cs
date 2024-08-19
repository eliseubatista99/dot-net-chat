namespace DotNetChatApi.Contracts
{
    public interface IDatabaseProvider
    {
        public List<ChatDto> RetrieveChatsForUser(string username);

        public void AddNewChat(ChatDto data);

        public List<MessageDto> RetrieveMessagesById(List<string> ids);

        public List<MessageReceiverDto> RetrieveMessagesReceiversById(List<string> ids);
    }
}
