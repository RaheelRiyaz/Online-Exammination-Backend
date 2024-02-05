namespace OnlineExammination.Application.RRModels
{
    public class NotificationRequest
    {
        public string Description { get; set; } = null!;
    }

    public class NotificationResponse:NotificationRequest
    {
        public Guid Id { get; set; }
    }
}
