using ErsatzTV.Core.Domain;

namespace ErsatzTV.Application.Scheduling;

public record UpdateScheduleBlockItem(
    string Name,
    int Index,
    string Query,
    PlayoutMode PlayoutMode,
    PlaybackOrder PlaybackOrder);
