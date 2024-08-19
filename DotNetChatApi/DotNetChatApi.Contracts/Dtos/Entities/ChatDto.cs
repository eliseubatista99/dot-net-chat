namespace DotNetChatApi.Contracts
{
    public class ChatDto
    {
        public required string ChatId { get; set; }

        public DateTime? LastMessageDate { get; set; }

        public required List<string> ChatMembers { get; set; }

        public required string ChatName { get; set; }

        public required List<MessageDto> ChatMessages { get; set; }
    }
}
