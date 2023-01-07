namespace ErsatzTV.Application.Channels;

public record UpdateChannelScheduleDayTemplateItem(
    int ScheduleDayTemplateId,
    int Index,
    ICollection<DayOfWeek> DaysOfWeek,
    ICollection<int> DaysOfMonth,
    ICollection<int> MonthsOfYear);
