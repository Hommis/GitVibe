using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia;
using Avalonia.Platform;
using DynamicData;
using GitVibe.ViewModels;
using Splat;

namespace GitVibe.Gui.Services;

public class FolderViewModel
{
  public FolderItem _folderItem;
  public string Name => _folderItem.Name; 
  public string FullPath { get; protected set;}
  private ObservableCollection<FolderViewModel> _children = new ObservableCollection<FolderViewModel>();

  public ObservableCollection<FolderViewModel> Children { 
    get {
     var children = _folderItem.Children.Select(x => new FolderViewModel(x) );
     _children.Clear();
     _children.AddRange(children);
     return _children;
    }
  }
  public FolderViewModel(FolderItem folderItem)
  {
    _folderItem = folderItem;
    FullPath = folderItem.FullPath ;
  }
}
