namespace ErsatzTV.Application.Scheduling;

public record CreateScheduleDayTemplateItem(int Index, TimeSpan? StartTime, int ScheduleBlockId);
