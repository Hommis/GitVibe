using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia.Platform.Storage;
using ReactiveUI;
using GitVibe.Model;
using GitVibe.Gui.ViewModels;

namespace GitVibe.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<GitLogEntry> GitLog { get; } = new()
    {
        new GitLogEntry("Initial commit", "John Doe"),
        new GitLogEntry("Add feature", "Jane Doe"),
    };

    public string Greeting { get; } = "Welcome to Avalonia!";

    public ObservableCollection<FolderItem> FolderTree { get; } = new()
    {

    };

    public IStorageFolder? SelectedFolder { get; protected set; } 

    public ICommand? OpenFolderCommand { get; set; }

    public MainWindowViewModel()
    {
    }

    public void OpenFolder(IStorageFolder folder)
    {
        SelectedFolder = folder;
        var folderItem = new FolderItemFromStorage(folder);
        FolderTree.Clear();
        FolderTree.Add(folderItem);
    }
}
