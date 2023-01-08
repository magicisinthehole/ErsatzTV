namespace ErsatzTV.Core.Domain.Scheduling;

public class ChannelPlayoutItem
{
    public int Id { get; set; }
    public int ChannelPlayoutId { get; set; }
    public ChannelPlayout ChannelPlayout { get; set; }
    public int MediaItemId { get; set; }
    public MediaItem MediaItem { get; set; }
    public DateTime Start { get; set; }
    public DateTime Finish { get; set; }
    public TimeSpan InPoint { get; set; }
    public TimeSpan OutPoint { get; set; }
    public DateTimeOffset StartOffset => new DateTimeOffset(Start, TimeSpan.Zero).ToLocalTime();
    public DateTimeOffset FinishOffset => new DateTimeOffset(Finish, TimeSpan.Zero).ToLocalTime();
}
