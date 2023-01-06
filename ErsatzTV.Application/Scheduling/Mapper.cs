using ErsatzTV.Core.Api.Scheduling;
using ErsatzTV.Core.Domain.Scheduling;

namespace ErsatzTV.Application.Scheduling;

internal static class Mapper
{
    internal static ScheduleBlockResponseModel ProjectToResponseModel(ScheduleBlock scheduleBlock) =>
        new(
            scheduleBlock.Id,
            scheduleBlock.Name,
            scheduleBlock.Items.Map(ProjectToResponseModel).ToList());

    internal static ScheduleBlockItemResponseModel ProjectToResponseModel(ScheduleBlockItem scheduleBlockItem) =>
        new(
            scheduleBlockItem.Id,
            scheduleBlockItem.Index,
            scheduleBlockItem.Name,
            scheduleBlockItem.Query,
            scheduleBlockItem.PlayoutMode,
            scheduleBlockItem.PlaybackOrder);
}
