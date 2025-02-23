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
    public ImmutableArray<string> Branches { get; set; } = ImmutableArray<string>.Empty;
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
    public GitLogEntry(Commit entry )
    {
        Message = entry.MessageShort;
        Author = $"{entry.Author.Name} <{entry.Author.Email}>" ;
        CommitDate = entry.Author.When.DateTime;
        Hash = entry.Sha;
        ParentHash = entry.Parents.FirstOrDefault()?.Sha ?? null;
    }
}
