namespace ErsatzTV.Application.Scheduling;

public record UpdateScheduleDayTemplateItem(int Index, TimeSpan? StartTime, int ScheduleBlockId);
