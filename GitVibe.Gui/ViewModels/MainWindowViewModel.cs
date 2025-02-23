using System.Collections.Generic;
using System.Collections.ObjectModel;
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
}
