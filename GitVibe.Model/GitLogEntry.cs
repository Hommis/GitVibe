using System;
using System.ComponentModel.DataAnnotations;
using LibGit2Sharp;

namespace GitVibe.Model;

public class GitLogEntry
{
    public string Message { get; set; }
    public string Author { get; set; }
    public DateTime CreateDate { get; set; }
    public string ShortHash { get ; set;}
    public string ParentShortHash { get ; set;}
    public string Graph {
      get {
        return "M 0,0 L 0,20 L 2,20 L 2,0 Z";
      }
    }
    public GitLogEntry(Commit entry )
    {
        Message = entry.MessageShort;
        Author = $"{entry.Author.Name} <{entry.Author.Email}>" ;
        CreateDate = entry.Author.When.DateTime;
        ShortHash = entry.Sha.Substring(0,7);
        ParentShortHash = entry.Parents.FirstOrDefault()?.Sha.Substring(0,7) ?? "Not found";
    }
}
