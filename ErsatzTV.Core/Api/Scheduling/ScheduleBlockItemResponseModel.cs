using ErsatzTV.Core.Domain;

namespace ErsatzTV.Core.Api.Scheduling;

public record ScheduleBlockItemResponseModel(
    int Id,
    int Index,
    string Name,
    string Query,
    PlayoutMode PlayoutMode,
    PlaybackOrder PlaybackOrder);
