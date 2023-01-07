namespace ErsatzTV.Core.Domain.Scheduling;

public class ScheduleDayTemplate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ScheduleBlock> ScheduleBlocks { get; set; }
    public List<ScheduleDayTemplateItem> Items { get; set; }
    public List<Channel> Channels { get; set; }
    public List<ChannelScheduleDayTemplate> ChannelScheduleDayTemplates { get; set; }
}
