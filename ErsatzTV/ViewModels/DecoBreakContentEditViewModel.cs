using System.ComponentModel;
using System.Runtime.CompilerServices;
using ErsatzTV.Annotations;
using ErsatzTV.Application.MediaCollections;
using ErsatzTV.Application.MediaItems;
using ErsatzTV.Core.Domain;
using ErsatzTV.Core.Domain.Scheduling;

namespace ErsatzTV.ViewModels;

public class DecoBreakContentEditViewModel : INotifyPropertyChanged
{
    private ProgramScheduleItemCollectionType _collectionType;

    public int Id { get; set; }
    public int Index { get; set; }

    public ProgramScheduleItemCollectionType CollectionType
    {
        get => _collectionType;
        set
        {
            if (_collectionType != value)
            {
                _collectionType = value;

                Collection = null;
                MultiCollection = null;
                MediaItem = null;
                SmartCollection = null;

                OnPropertyChanged(nameof(Collection));
                OnPropertyChanged(nameof(MultiCollection));
                OnPropertyChanged(nameof(MediaItem));
                OnPropertyChanged(nameof(SmartCollection));
                OnPropertyChanged(nameof(CollectionName));
            }
        }
    }

    [CanBeNull]
    public MediaCollectionViewModel Collection { get; set; }

    [CanBeNull]
    public MultiCollectionViewModel MultiCollection { get; set; }

    [CanBeNull]
    public SmartCollectionViewModel SmartCollection { get; set; }

    [CanBeNull]
    public NamedMediaItemViewModel MediaItem { get; set; }

    public DecoBreakPlacement Placement { get; set; }

    public string CollectionName => CollectionType switch
    {
        ProgramScheduleItemCollectionType.Collection => Collection?.Name,
        ProgramScheduleItemCollectionType.TelevisionShow => MediaItem?.Name,
        ProgramScheduleItemCollectionType.TelevisionSeason => MediaItem?.Name,
        ProgramScheduleItemCollectionType.Artist => MediaItem?.Name,
        ProgramScheduleItemCollectionType.MultiCollection => MultiCollection?.Name,
        ProgramScheduleItemCollectionType.SmartCollection => SmartCollection?.Name,
        _ => string.Empty
    };

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
