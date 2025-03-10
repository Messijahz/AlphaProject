namespace AlphaProject.Core.Models;

public class Message
{
    public Guid MessageId { get; set; }
    public Guid SenderId { get; set; }
    public User Sender { get; set; }

    public Guid ReceiverId { get; set; }
    public User Receiver { get; set; }

    public string Content { get; set; } = string.Empty;
    public DateTime SentAt { get; set; } = DateTime.Now;
    public bool IsRead { get; set; } = false;
}
