using System.Collections.ObjectModel;

namespace GitVibe.Model;

public class FolderItem
{
    public string Name { get; set; }
    public ObservableCollection<FolderItem> Children { get; set; }

    public FolderItem(string name)
    {
        Name = name;
        Children = new ObservableCollection<FolderItem>();
    }
}
