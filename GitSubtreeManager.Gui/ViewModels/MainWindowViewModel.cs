using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia.Platform.Storage;
using ReactiveUI;
using GitSubtreeManager.Model;
using GitSubtreeManager.Gui.Services;
using System;
using System.Reactive.Linq;

using ReactiveUI.SourceGenerators;
namespace GitSubtreeManager.Gui.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<GitLogEntry> GitLog { get; } = new();

    public ObservableCollection<FolderViewModel> FolderTree { get; } = new();
    [Reactive]
    public partial FolderViewModel? SelectedSubDirectory {get; set;}

    public IStorageFolder? SelectedFolder { get; protected set; } 

    public ICommand? OpenFolderCommand { get; set; }

    public MainWindowViewModel()
    {

      // Subscribe to changes in SelectedSubDirectory to update history
      this.WhenAnyValue(vm => vm.SelectedSubDirectory).Subscribe(_ => RefreshGitLog());

    }
    public Repository? SelectedRepository { get; set;
     }
    public void OpenFolder(IStorageFolder folder)
  {
    SelectedFolder = folder;
    var folderItem = new FolderItemFromStorage(folder);
    FolderTree.Clear();
    SelectedRepository = GitRepository.FromPath(folder.Path.LocalPath);
    RefreshGitLog();
    var folderViewModel = new FolderViewModel(folderItem);
    FolderTree.Add(folderViewModel);
  }

  private void RefreshGitLog()
  {
    if( SelectedRepository == null ) return;
    if( SelectedSubDirectory == null ) {
      SelectedRepository.ClearSubtreeFilter();
    } else {
      SelectedRepository.SetSubtreeFilterFromPath( SelectedSubDirectory.FullPath );
    }
    GitLog.Clear();
    foreach (var entry in SelectedRepository.GetHistory())
    {
      GitLog.Add(entry);
    }
  }
}
