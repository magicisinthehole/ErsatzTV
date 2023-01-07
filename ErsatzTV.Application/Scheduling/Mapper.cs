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

    internal static ScheduleDayTemplateResponseModel ProjectToResponseModel(ScheduleDayTemplate scheduleDayTemplate) =>
        new(
            scheduleDayTemplate.Id,
            scheduleDayTemplate.Name,
            scheduleDayTemplate.Items.Map(ProjectToResponseModel).ToList());

    internal static ScheduleDayTemplateItemResponseModel ProjectToResponseModel(
        ScheduleDayTemplateItem scheduleDayTemplateItem) =>
        new(
            scheduleDayTemplateItem.Index,
            scheduleDayTemplateItem.StartTime,
            scheduleDayTemplateItem.ScheduleBlockId,
            scheduleDayTemplateItem.ScheduleBlock.Name);
}
