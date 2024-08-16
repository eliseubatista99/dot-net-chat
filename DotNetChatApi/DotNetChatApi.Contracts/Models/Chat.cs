using System.ComponentModel.DataAnnotations;

namespace DotNetChatApi.Contracts
{
    public class Chat
    {
        [Key]
        public required string ChatId { get; set; }

        [Required]
        public required List<string> ChatMembers { get; set; }

        [Required]
        public required string ChatName { get; set; }

        [Required]
        public required List<string> ChatMessages { get; set; }

    }
}
