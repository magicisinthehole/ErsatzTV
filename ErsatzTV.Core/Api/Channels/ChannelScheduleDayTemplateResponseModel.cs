namespace ErsatzTV.Core.Api.Channels;

public record ChannelScheduleDayTemplateResponseModel(
    int Index,
    string ScheduleDayTemplateName,
    ICollection<DayOfWeek> DaysOfWeek,
    ICollection<int> DaysOfMonth,
    ICollection<int> MonthsOfYear);
