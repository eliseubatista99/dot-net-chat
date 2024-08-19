namespace DotNetChatApi.Contracts
{
    public class MessageDto
    {
        public required string Id { get; set; }
        public required string Sender { get; set; }
        public required string Content { get; set; }
        public required List<MessageReceiverDto> Receivers { get; set; }
        public required DateTime SendDate { get; set; }
    }
}
