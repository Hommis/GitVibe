using System.Collections.Immutable;
using LibGit2Sharp;
namespace GitVibe.Model;

public class GitRepository : Repository, IDisposable
{
  public override bool IsValid { get {return true;} }
  protected LibGit2Sharp.Repository Repository { get; private set; }
  protected GitRepository(string path) :base(path)
  {   if( !LibGit2Sharp.Repository.IsValid(path) )
      {
        throw new ArgumentException("Invalid repository path");
      }
      Repository = new LibGit2Sharp.Repository(base.Path);

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

      IEnumerable<Commit> commitsInSubtree;
      if( SubtreeFilter == null ) {
        commitsInSubtree = Repository.Commits;

      } else
      {
        commitsInSubtree = GetAllCommitsInSubtree();
      }
      return ParseCommitsForGraph( commitsInSubtree.Select( commit => new GitLogEntry(commit) ) );

  }

  private IEnumerable<GitLogEntry> ParseCommitsForGraph(IEnumerable<GitLogEntry> commits)
  {
    var returnArray = new List<GitLogEntry>();;
    var oldToNew = commits.OrderBy( commit => commit.CommitDate );
    // Create a lookup table for the commits
    var lookUpTable = new Dictionary<string, GitLogEntry>();
    foreach( var commit in oldToNew ) {
      lookUpTable.Add(commit.Hash, commit);
    }
    var branches = new Dictionary<string, string>();
    foreach( var commit in oldToNew ) {
      if( commit.ParentHash != null  && branches.ContainsKey(commit.ParentHash) ) {
        // If we can find the parent branch from the collection of branches, we don't need a new branch
        branches[commit.ParentHash] = commit.Hash;  
      }
      else {
        // If we can't find the parent branch, we need to create a new branch
        branches.Add(commit.Hash, commit.Hash);
      }
      commit.Branches = branches.Keys.ToImmutableArray();
      returnArray.Add(commit);
    }
    return returnArray;
  }

  private IEnumerable<Commit> GetAllCommitsInSubtree( )
  {
    return Repository.Commits.Where( commit => commit.Tree[SubtreeFilter] != null );
  }

  public void Dispose()
  {
    Repository.Dispose();
  }
}
