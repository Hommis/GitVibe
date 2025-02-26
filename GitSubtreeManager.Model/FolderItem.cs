using System.Collections.ObjectModel;

namespace GitSubtreeManager.Model;

public class FolderItem
{

    public virtual string Name { get; protected set; }
    private ObservableCollection<FolderItem>? _children;
    public virtual ObservableCollection<FolderItem> Children { 
      get {
        if (_children == null) {
          _children = new ObservableCollection<FolderItem>();
        }
        return _children;
      }   
    }

    public FolderItem(string name, string fullPath) : this()
    {
        Name = name;
        FullPath = fullPath;      
    }
    protected FolderItem() {

      Name = "Undefined";
      FullPath = "/";
    }
    public virtual string FullPath { get ;private set;}
}
