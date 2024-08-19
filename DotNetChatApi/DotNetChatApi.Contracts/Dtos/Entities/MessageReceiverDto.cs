namespace DotNetChatApi.Contracts
{
    public class MessageReceiverDto
    {
        public required string Username { get; set; }

        public DateTime? ReceivedDate { get; set; }

        public DateTime? ReadDate { get; set; }
    }
}
