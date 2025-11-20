// Decompiled with JetBrains decompiler
// Type: OpenTracksBETA.ItemViewModel
// Assembly: OpenTracksBETA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 37D9428F-DECE-49E6-8BF2-BFFD8E04DA19
// Assembly location: C:\Users\Admin\Desktop\RE\OpenTracksCE\OpenTracksBETA.dll

using System.ComponentModel;

#nullable disable
namespace OpenTracksBETA
{
  public class ItemViewModel : INotifyPropertyChanged
  {
    private string _lineOne;
    private string _lineTwo;
    private string _lineThree;

    public string LineOne
    {
      get => this._lineOne;
      set
      {
        if (!(value != this._lineOne))
          return;
        this._lineOne = value;
        this.NotifyPropertyChanged(nameof (LineOne));
      }
    }

    public string LineTwo
    {
      get => this._lineTwo;
      set
      {
        if (!(value != this._lineTwo))
          return;
        this._lineTwo = value;
        this.NotifyPropertyChanged(nameof (LineTwo));
      }
    }

    public string LineThree
    {
      get => this._lineThree;
      set
      {
        if (!(value != this._lineThree))
          return;
        this._lineThree = value;
        this.NotifyPropertyChanged(nameof (LineThree));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (null == propertyChanged)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
