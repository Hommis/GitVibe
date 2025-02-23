using System;

namespace GitVibe.Model;

public class Repository 
{
  public virtual string Path { get; protected set; }

  public virtual bool IsValid => false;

  public virtual string? SubtreeFilter { get; protected set; }

  internal Repository(string path) 
  {
    Path = path;
  }

  public virtual IEnumerable<GitLogEntry> GetHistory()
  {
    return Array.Empty<GitLogEntry>();
  }

  public void SetSubtreeFilterFromPath(string fullPath)
  {
    if (string.IsNullOrEmpty(fullPath))
    {
      throw new ArgumentException("Full path cannot be null or empty", nameof(fullPath));
    }

    if (!fullPath.StartsWith(Path, StringComparison.OrdinalIgnoreCase))
    {
      throw new ArgumentException("Full path must be within the repository path", nameof(fullPath));
    }

    // Set SubtreeFilter to the relative path from the repository path
    SubtreeFilter = fullPath.Substring(Path.Length).Trim(System.IO.Path.DirectorySeparatorChar).Replace(System.IO.Path.DirectorySeparatorChar, '/');
    
    if( SubtreeFilter.Trim() =="" ) {
      SubtreeFilter = null;
    }
  }

  public void ClearSubtreeFilter()
  {
    SubtreeFilter = null;
  }
}
