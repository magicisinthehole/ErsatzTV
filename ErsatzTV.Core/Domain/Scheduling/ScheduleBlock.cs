namespace ErsatzTV.Core.Domain.Scheduling;

public class ScheduleBlock
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ScheduleBlockItem> Items { get; set; }
}
