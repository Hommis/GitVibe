using System;

namespace GitVibe.Model;

public class Repository 
{
  public virtual string Path { get; protected set; }


  public virtual bool IsValid => false;
  internal Repository(string path) 
  {
    Path = path;
  }
  public virtual IEnumerable<GitLogEntry> GetHistory()
  {
    return Array.Empty<GitLogEntry>();
  }
}
