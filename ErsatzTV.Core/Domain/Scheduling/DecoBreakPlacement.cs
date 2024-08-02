namespace ErsatzTV.Core.Domain.Scheduling;

[Flags]
public enum DecoBreakPlacement
{
    BlockStart,
    BlockFinish,
    BetweenBlockItems,
    ChapterMarkers
}
