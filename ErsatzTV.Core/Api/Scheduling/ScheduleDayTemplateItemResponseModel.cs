using ErsatzTV.Core.Domain;

namespace ErsatzTV.Core.Api.Scheduling;

public record ScheduleDayTemplateItemResponseModel(
    int Index,
    TimeSpan? StartTime,
    int ScheduleBlockId,
    string ScheduleBlockName)
{
    public StartType StartType => StartTime.HasValue ? StartType.Fixed : StartType.Dynamic;
}
