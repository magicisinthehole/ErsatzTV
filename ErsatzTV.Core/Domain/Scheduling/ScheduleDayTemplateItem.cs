namespace ErsatzTV.Core.Domain.Scheduling;

public class ScheduleDayTemplateItem
{
    public int ScheduleDayTemplateId { get; set; }
    public ScheduleDayTemplate ScheduleDayTemplate { get; set; }
    public int ScheduleBlockId { get; set; }
    public ScheduleBlock ScheduleBlock { get; set; }
    public int Index { get; set; }
    public StartType StartType => StartTime.HasValue ? StartType.Fixed : StartType.Dynamic;
    public TimeSpan? StartTime { get; set; }
}
