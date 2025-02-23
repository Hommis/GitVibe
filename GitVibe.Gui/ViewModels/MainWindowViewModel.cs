using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia.Platform.Storage;
using ReactiveUI;
using GitVibe.Model;
using GitVibe.Gui.Services;

namespace GitVibe.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<GitLogEntry> GitLog { get; } = new();

    public string Greeting { get; } = "Welcome to Avalonia!";

    public ObservableCollection<FolderViewModel> FolderTree { get; } = new();

    public IStorageFolder? SelectedFolder { get; protected set; } 

    public ICommand? OpenFolderCommand { get; set; }

    public MainWindowViewModel()
    {
    }
    public Repository? SelectedRepository { get; set;
     }
    public void OpenFolder(IStorageFolder folder)
    {
        SelectedFolder = folder;
        var folderItem = new FolderItemFromStorage(folder);
        FolderTree.Clear();
        FolderTree.Add(new FolderViewModel( folderItem ));
          
        SelectedRepository = GitRepository.FromPath(folder.Path.LocalPath);
        GitLog.Clear();
        foreach (var entry in SelectedRepository.GetHistory())
        {
            GitLog.Add(entry);
        }

    }
}
