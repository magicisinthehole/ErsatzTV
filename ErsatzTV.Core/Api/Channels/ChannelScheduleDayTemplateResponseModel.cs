namespace ErsatzTV.Core.Api.Channels;

public record ChannelScheduleDayTemplateResponseModel(
    string ScheduleDayTemplateName,
    ICollection<DayOfWeek> DaysOfWeek,
    ICollection<int> DaysOfMonth,
    ICollection<int> MonthsOfYear);
