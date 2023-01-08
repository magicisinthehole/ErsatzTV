namespace ErsatzTV.Core.Domain.Scheduling;

public class ChannelPlayout
{
    public int Id { get; set; }
    public int ChannelId { get; set; }
    public Channel Channel { get; set; }
}
