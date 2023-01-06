namespace ErsatzTV.Core.Domain.Scheduling;

public class ScheduleBlockItem
{
    public int Id { get; set; }
    public int Index { get; set; }
    public string Name { get; set; }
    public string Query { get; set; }
    // public ScheduleBlockItemGuideMode GuideMode { get; set; }
    // public string CustomTitle { get; set; }
    public PlayoutMode PlayoutMode { get; set; }
    public PlaybackOrder PlaybackOrder { get; set; }
    public int ScheduleBlockId { get; set; }
    public ScheduleBlock ScheduleBlock { get; set; }
}
