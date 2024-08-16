namespace DotNetChatApi.Contracts
{
    public interface IDatabaseProvider
    {


        public void RetrieveMessagesForUser(string username);
    }
}
