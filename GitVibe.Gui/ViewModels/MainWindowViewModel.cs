using System.Collections.ObjectModel;
using System.Windows.Input;

using GitVibe.Model;

using ReactiveUI ;
using GitVibe.Views;
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
      new FolderItem("Root")
      {
          Children = new ObservableCollection<FolderItem>
          {
              new FolderItem("Folder 1"),
              new FolderItem("Folder 2")
          }
      }
  };

  public ICommand OpenFolderCommand { get; set; }

  public MainWindowViewModel()
  {

  }

}
