using DotNetChatApi.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DotNetChatApi
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; } = default!;
        public DbSet<Message> Messages { get; set; } = default!;
        public DbSet<MessageReceiver> MessageReceivers { get; set; } = default!;

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

        public void SeedDatabase(IServiceProvider serviceProvider)
        {
            /*if (context.Users.Any() || context.Messages.Any())
            {
                return;   // DB has been seeded
            }*/

            if (this.Chats.Any())
            {
                return; // DB has been seeded
            }

            SeedMessages();

            SeedMessageReceivers();

            SeedChats();

            this.SaveChanges();
        }

        private void SeedMessages()
        {
            this.Messages.AddRange(
                new Message
                {
                    Id = "Message1",
                    Sender = "User1",
                    Content = "Hello group",
                    Receivers = new List<string>
                    {
                        "User2",
                        "User3",
                    },
                    SendDate = new DateTime(2024, 08, 14, 17, 53, 00),
                },
                new Message
                {
                    Id = "Message2",
                    Sender = "User3",
                    Content = "Wazzup?",
                    Receivers = new List<string>
                    {
                        "User1",
                        "User2",
                    },
                    SendDate = new DateTime(2024, 08, 14, 17, 56, 00),
                }
            );
        }

        private void SeedMessageReceivers()
        {
            this.MessageReceivers.AddRange(
                new MessageReceiver
                {
                    Id = "MessageReceiver1",
                    MessageId = "Message1",
                    Username = "User2",
                    ReceivedDate = new DateTime(2024, 08, 14, 17, 54, 00),
                },
                new MessageReceiver
                {
                    Id = "MessageReceiver2",
                    MessageId = "Message1",
                    Username = "User3",
                    ReceivedDate = new DateTime(2024, 08, 14, 17, 54, 00),
                    ReadDate = new DateTime(2024, 08, 14, 17, 55, 00),
                },
                new MessageReceiver
                {
                    Id = "MessageReceiver3",
                    MessageId = "Message2",
                    Username = "User1",
                    ReceivedDate = new DateTime(2024, 08, 14, 17, 57, 00),
                },
                new MessageReceiver
                {
                    Id = "MessageReceiver4",
                    MessageId = "Message2",
                    Username = "User2",
                }
            );
        }

        private void SeedChats()
        {
            this.Chats.AddRange(
                new Chat
                {
                    ChatId = "1",
                    ChatMembers = new List<string>
                    {
                                    "User1",
                                    "User2",
                                    "User3",
                    },
                    ChatName = "Group 1",
                    ChatMessages = new List<string>
                    {
                                    "Message1",
                                    "Message2"
                    },
                }
            );
        }
    }
}
