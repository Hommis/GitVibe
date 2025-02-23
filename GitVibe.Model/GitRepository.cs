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
    var branchIdGenerator = 0 ;
    var returnArray = new List<GitLogEntry>();;
    var oldToNew = commits.OrderBy( commit => commit.CommitDate );
    // Create a lookup table for the commits
    var lookUpTable = new Dictionary<string, GitLogEntry>();
    foreach( var commit in oldToNew ) {
      lookUpTable.Add(commit.Hash, commit);
    }
    var branches = new Dictionary<string, BranchInfo>();
    foreach( var commit in oldToNew ) {

      // Resetting all branches to not new.
      foreach(var branchKey in branches.Keys.ToArray() ) {
        var parentMatches =  commit.Parents.Any( parentHash => parentHash == branchKey) ; 
        branches[ branchKey] = new BranchInfo( branches[ branchKey].Id, false, false, parentMatches, false, branches[ branchKey].IsMered);
      }
      var isMerged = false;
      var missingParent = true;
      foreach( var parentHash in commit.Parents ) {
        if( branches.ContainsKey(parentHash) ) {
          // If we can find the parent branch, we need to update it
          var parentBranch = branches[parentHash];
          missingParent = !lookUpTable.ContainsKey( parentHash);
          branches.Remove( parentHash);
          if( !isMerged) branches.Add(commit.Hash, new BranchInfo(parentBranch.Id, true, false, true, missingParent, isMerged ));
          isMerged = true;
        } else {

        branches.Add(commit.Hash, new BranchInfo(branchIdGenerator++, true, false, true, missingParent, false));
        }
      }
      if( commit.Parents.Length > 0  && branches.ContainsKey(commit.ParentHash) ) {
        // If we can find the parent branch, we need to update it
        var parentBranch = branches[commit.ParentHash];
        branches.Remove( commit.ParentHash);
        branches.Add( commit.Hash, new BranchInfo(parentBranch.Id, false, false, false, false) );
      }


      commit.Branches = branches.Values.ToImmutableArray();
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
