using ErsatzTV.Core.Domain;

namespace ErsatzTV.Application.Scheduling;

public record CreateScheduleBlockItem(
    string Name,
    int Index,
    string Query,
    PlayoutMode PlayoutMode,
    PlaybackOrder PlaybackOrder);
