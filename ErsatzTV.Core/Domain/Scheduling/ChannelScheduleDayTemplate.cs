namespace ErsatzTV.Core.Domain.Scheduling;

public class ChannelScheduleDayTemplate
{
    public int ChannelId { get; set; }
    public Channel Channel { get; set; }
    public int ScheduleDayTemplateId { get; set; }
    public ScheduleDayTemplate ScheduleDayTemplate { get; set; }
    public int Index { get; set; }
    public ICollection<DayOfWeek> DaysOfWeek { get; set; }
    public ICollection<int> DaysOfMonth { get; set; }
    public ICollection<int> MonthsOfYear { get; set; }
}
