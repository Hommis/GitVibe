using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using GitVibe.ViewModels;

namespace GitVibe.Gui.ViewModels;

public class FolderItemFromStorage : FolderItem
{
  private readonly IStorageFolder _storageFolder;
  public override string Name => _storageFolder.Name;
  private ObservableCollection<FolderItem>? _children;
  public override ObservableCollection<FolderItem> Children { get {
    if(_children == null ) {
      _children = new ObservableCollection<FolderItem>();
      LoadChildrenAsync().Wait();
    }
    return _children;
  }}
  public FolderItemFromStorage(IStorageFolder storageFolder) 
  {
      _storageFolder = storageFolder;
  }

  public async Task LoadChildrenAsync()
  {
    await foreach (var item in _storageFolder.GetItemsAsync())
    {
      if (item is IStorageFolder folder)
      {
          Children.Add(new FolderItemFromStorage(folder));
      }
      /*else if (item is IStorageFile file)
      {
          Children.Add(new FolderItem(file.Name));
      }*/
    }
  }
}
