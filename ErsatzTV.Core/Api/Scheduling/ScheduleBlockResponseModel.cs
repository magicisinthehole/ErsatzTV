namespace ErsatzTV.Core.Api.Scheduling;

public record ScheduleBlockResponseModel(
    int Id,
    string Name,
    List<ScheduleBlockItemResponseModel> Items);