// Decompiled with JetBrains decompiler
// Type: OpenTracksBETA.ViewModels.TrackItem
// Assembly: OpenTracksBETA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 37D9428F-DECE-49E6-8BF2-BFFD8E04DA19
// Assembly location: C:\Users\Admin\Desktop\RE\OpenTracksCE\OpenTracksBETA.dll

using System.ComponentModel;

#nullable disable
namespace OpenTracksBETA.ViewModels
{
  public class TrackItem : INotifyPropertyChanged
  {
    private string _title;
    private string _artist;
    private string _audioPath;
    private string _albumCoverPath;

    public string Title
    {
      get => this._title;
      set
      {
        this._title = value;
        this.OnPropertyChanged(nameof (Title));
      }
    }

    public string Artist
    {
      get => this._artist;
      set
      {
        this._artist = value;
        this.OnPropertyChanged(nameof (Artist));
      }
    }

    public string AudioPath
    {
      get => this._audioPath;
      set
      {
        this._audioPath = value;
        this.OnPropertyChanged(nameof (AudioPath));
      }
    }

    public string AlbumCoverPath
    {
      get => this._albumCoverPath;
      set
      {
        this._albumCoverPath = value;
        this.OnPropertyChanged(nameof (AlbumCoverPath));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
