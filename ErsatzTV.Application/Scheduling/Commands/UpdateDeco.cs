using ErsatzTV.Core;
using ErsatzTV.Core.Domain;
using ErsatzTV.Core.Domain.Scheduling;

namespace ErsatzTV.Application.Scheduling;

public record UpdateDecoBreakContent(
    int Id,
    ProgramScheduleItemCollectionType CollectionType,
    int? CollectionId,
    int? MediaItemId,
    int? MultiCollectionId,
    int? SmartCollectionId,
    DecoBreakPlacement Placement);

public record UpdateDeco(
    int DecoId,
    int DecoGroupId,
    string Name,
    DecoMode WatermarkMode,
    int? WatermarkId,
    bool UseWatermarkDuringFiller,
    DecoMode DefaultFillerMode,
    ProgramScheduleItemCollectionType DefaultFillerCollectionType,
    int? DefaultFillerCollectionId,
    int? DefaultFillerMediaItemId,
    int? DefaultFillerMultiCollectionId,
    int? DefaultFillerSmartCollectionId,
    bool DefaultFillerTrimToFit,
    DecoMode DeadAirFallbackMode,
    ProgramScheduleItemCollectionType DeadAirFallbackCollectionType,
    int? DeadAirFallbackCollectionId,
    int? DeadAirFallbackMediaItemId,
    int? DeadAirFallbackMultiCollectionId,
    int? DeadAirFallbackSmartCollectionId,
    DecoMode BreakContentMode,
    List<UpdateDecoBreakContent> BreakContent)
    : IRequest<Either<BaseError, DecoViewModel>>;
