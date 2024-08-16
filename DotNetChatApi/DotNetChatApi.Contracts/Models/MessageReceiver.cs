using System.ComponentModel.DataAnnotations;

namespace DotNetChatApi.Contracts
{
    public class MessageReceiver
    {
        [Key]
        public required string Id { get; set; }

        [Required]
        public required string MessageId { get; set; }

        [Required]
        public required string Username { get; set; }

        public DateTime? ReceivedDate { get; set; }

        public DateTime? ReadDate { get; set; }


    }
}
