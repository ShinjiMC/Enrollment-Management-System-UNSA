namespace NotificationsMicroservice.Domain.ValueObjects
{
    public class Message
    {
        public string Content { get; private set; }

        public Message(string content)
        {
            Content = content;
        }

        public static implicit operator Message(string content) => new Message(content);
        public static implicit operator string(Message message) => message.Content;
    }
}
