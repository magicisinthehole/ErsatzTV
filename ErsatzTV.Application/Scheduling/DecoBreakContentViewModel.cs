using ErsatzTV.Application.MediaCollections;
using ErsatzTV.Application.MediaItems;
using ErsatzTV.Core.Domain;
using ErsatzTV.Core.Domain.Scheduling;

namespace ErsatzTV.Application.Scheduling;

public record DecoBreakContentViewModel(
    int Id,
    ProgramScheduleItemCollectionType CollectionType,
    MediaCollectionViewModel Collection,
    NamedMediaItemViewModel MediaItem,
    MultiCollectionViewModel MultiCollection,
    SmartCollectionViewModel SmartCollection,
    DecoBreakPlacement Placement);
