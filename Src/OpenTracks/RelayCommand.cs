// Decompiled with JetBrains decompiler
// Type: OpenTracksBETA.RelayCommand`1
// Assembly: OpenTracksBETA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 37D9428F-DECE-49E6-8BF2-BFFD8E04DA19
// Assembly location: C:\Users\Admin\Desktop\RE\OpenTracksCE\OpenTracksBETA.dll

using System;
using System.Windows.Input;

#nullable disable
namespace OpenTracksBETA
{
  public class RelayCommand<T> : ICommand
  {
    private readonly Action<T> _execute;
    private readonly Predicate<T> _canExecute;

    public RelayCommand(Action<T> execute)
      : this(execute, (Predicate<T>) null)
    {
    }

    public RelayCommand(Action<T> execute, Predicate<T> canExecute)
    {
      this._execute = execute != null ? execute : throw new ArgumentNullException(nameof (execute));
      this._canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
      return this._canExecute == null || this._canExecute((T) parameter);
    }

    public void Execute(object parameter) => this._execute((T) parameter);

    public event EventHandler CanExecuteChanged
    {
      add
      {
      }
      remove
      {
      }
    }
  }
}
