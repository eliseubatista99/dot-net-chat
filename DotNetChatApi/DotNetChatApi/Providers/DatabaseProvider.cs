using DotNetChatApi.Contracts;

namespace DotNetChatApi
{
    public class DatabaseProvider : IDatabaseProvider
    {
        private IServiceScopeFactory scopeFactory;

        public DatabaseProvider(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        private DatabaseContext GetDatabaseContext()
        {
            // Create a new scope (since DbContext is scoped by default)
            var scope = scopeFactory.CreateScope();

            // Get a Dbcontext from the scope
            return scope.ServiceProvider
                .GetRequiredService<DatabaseContext>();
        }

        public void AddNewChat(ChatDto data)
        {
            var chat = new Chat
            {
                ChatId = data.ChatId,
                ChatName = data.ChatName,
                ChatMembers = data.ChatMembers,
                ChatMessages = data.ChatMessages.Select(message => message.Id).ToList(),
            };

            GetDatabaseContext().Chats.Add(chat);
        }

        public List<ChatDto> RetrieveChatsForUser(string username)
        {
            var chatsInDb = GetDatabaseContext().Chats.ToList();

            if (chatsInDb == null || chatsInDb.Count == 0)
            {
                return new List<ChatDto>();
            }

            chatsInDb = chatsInDb.Where(chat => chat.ChatMembers.Contains(username)).ToList();

            var chats = chatsInDb.Select(chat =>
            {
                var messages = RetrieveMessagesById(chat.ChatMessages);

                return new ChatDto
                {
                    ChatId = chat.ChatId,
                    ChatName = chat.ChatName,
                    ChatMembers = chat.ChatMembers,
                    ChatMessages = messages,
                    LastMessageDate = messages[0]?.SendDate,
                };
            }).ToList();

            return chats;
        }

        public List<MessageDto> RetrieveMessagesById(List<string> ids)
        {
            var result = new List<MessageDto>();

            for (int i = 0; i < ids.Count; i++)
            {
                var messageInDb = GetDatabaseContext().Messages.FirstOrDefault(message => message.Id == ids[i]);

                if (messageInDb != null)
                {
                    var messageReceivers = RetrieveMessagesReceiversById(messageInDb.Receivers);

                    result.Add(new MessageDto
                    {
                        Id = messageInDb.Id,
                        Content = messageInDb.Content,
                        Receivers = messageReceivers,
                        Sender = messageInDb.Sender,
                        SendDate = messageInDb.SendDate,
                    });
                }
            }

            return result;
        }

        public List<MessageReceiverDto> RetrieveMessagesReceiversById(List<string> ids)
        {
            var result = new List<MessageReceiverDto>();

            for (int i = 0; i < ids.Count; i++)
            {
                var receiverInDb = GetDatabaseContext().MessageReceivers.FirstOrDefault(receiver => receiver.Id == ids[i]);

                if (receiverInDb != null)
                {
                    result.Add(new MessageReceiverDto
                    {
                        ReadDate = receiverInDb.ReadDate,
                        ReceivedDate = receiverInDb.ReceivedDate,
                        Username = receiverInDb.Username,
                    });
                }
            }

            return result;
        }
    }
}
