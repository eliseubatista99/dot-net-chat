using System.ComponentModel.DataAnnotations;

namespace DotNetChatApi.Contracts
{
    public class Message
    {
        [Key]
        public required string Id { get; set; }

        [Required]
        public required string Sender { get; set; }

        [Required]
        public required string Content { get; set; }

        [Required]
        public required List<string> Receivers { get; set; }

        [Required, DataType(DataType.DateTime)]
        public required DateTime SendDate { get; set; }
    }
}
