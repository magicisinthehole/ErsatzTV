namespace ErsatzTV.Core.Api.Scheduling;

public record ScheduleDayTemplateResponseModel(
    int Id,
    string Name,
    List<ScheduleDayTemplateItemResponseModel> Items);
