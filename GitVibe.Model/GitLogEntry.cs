using System;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using LibGit2Sharp;

namespace GitVibe.Model;

public class GitLogEntry
{
    public string Message { get; set; }
    public string Author { get; set; }
    public DateTime CommitDate { get; set; }
    public string ShortHash { get { return Hash.Substring(0,7); } }
    public ImmutableArray<BranchInfo> Branches { get; set; } = ImmutableArray<BranchInfo>.Empty;

    public bool IsMerge { get; set;}
    public string? ParentShortHash { 
      get { 
        if( ParentHash == null ) {
          return null;
        }
        return ParentHash.Substring(0,7); 
      }
    } 
    public string Hash { get; set;}
    public string? ParentHash { get; set; }  
    public string[] Parents { get; set; } = new string[0];
    public GitLogEntry(Commit entry )
    {
        Message = entry.MessageShort;
        Author = $"{entry.Author.Name} <{entry.Author.Email}>" ;
        CommitDate = entry.Author.When.DateTime;
        Hash = entry.Sha;
        Parents = entry.Parents.Select( parent => parent.Sha ).ToArray();
        ParentHash = Parents.FirstOrDefault() ?? null;
        IsMerge = entry.Parents.Count() > 1;
    }
}
