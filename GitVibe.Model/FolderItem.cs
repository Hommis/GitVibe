using System.Collections.ObjectModel;

namespace GitVibe.ViewModels;

public class FolderItem
{
  
    public virtual string Name { get; protected set; }
    private ObservableCollection<FolderItem> _children;
    public virtual ObservableCollection<FolderItem> Children { get {
      if (_children == null) {
        _children = new ObservableCollection<FolderItem>();
      }
      return _children;
    } 
    }

    public FolderItem(string name) : this()
    {
        Name = name;
      
    }
    protected FolderItem() {

      Name = "Undefined";
    }
}
