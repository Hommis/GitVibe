using System;
using System.Collections;
using System.Collections.Generic;
using GitVibe.Model;
using LibGit2Sharp;
namespace GitVibe.Model;

public class GitRepository : Repository
{
  public override bool IsValid { get {return true;} }

  protected GitRepository(string path) :base(path)
  {

      if( !LibGit2Sharp.Repository.IsValid(path) )
      {
        throw new ArgumentException("Invalid repository path");
      }
  }
  public static Repository FromPath( string path ) {
    if(LibGit2Sharp.Repository.IsValid(path) ) {
      return new GitRepository(path);
    } else {
      return new Repository(path) ;
    }
  }

  public override IEnumerable<GitLogEntry> GetHistory()
  {

    using (var repo = new LibGit2Sharp.Repository(base.Path))
    { 
      var allCommits = repo.Commits;
      var commitsInSubtree = new List<Commit>();
      if( SubtreeFilter == null ) {
        commitsInSubtree = allCommits.ToList();
      } else {
        string[] filterParts = SubtreeFilter.Split('/');        
        
        foreach( var commit in allCommits ) {
          if( commit.Tree[ SubtreeFilter ] != null ) {
            commitsInSubtree.Add(commit);
          }
        }
      }
      return commitsInSubtree.Select( commit => new GitLogEntry(commit) ).ToArray();  
    }
  }
}
