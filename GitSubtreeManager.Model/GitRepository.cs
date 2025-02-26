using System.Collections.Immutable;
using System.Net.Http.Headers;
using LibGit2Sharp;
namespace GitSubtreeManager.Model;

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
  private Dictionary<string,Branch> branchesSearchTable = new Dictionary<string, Branch>();
  private List<Branch> uniqueBranchesList = new List<Branch>();  
  private int branchIdGenerator = 0 ;
  private IEnumerable<GitLogEntry> ParseCommitsForGraph(IEnumerable<GitLogEntry> commits)
  {

    var returnArray = new List<GitLogEntry>();;
    var oldToNew = commits.OrderBy( commit => commit.CommitDate );
    // Create a lookup table for the commits
    var lookUpTable = new Dictionary<string, GitLogEntry>();
    foreach( var commit in oldToNew ) {
      lookUpTable.Add(commit.Hash, commit);
    }


    foreach( var commit in oldToNew ) {
      // Mark all branches as not new
      foreach( var branch in uniqueBranchesList ) {
        branch.IsNew = false;
      }      
      // It is a completely new branch
      if( commit.Parents.Length == 0 ) {
        var branch = FindOrCreateBranch(commit);
      }
      foreach( var parentHash in commit.Parents )
      {
        var parent = lookUpTable[parentHash];
        // Look for existing branch
        FindOrCreateBranch(parent);
      }

      // This is a merge commit
      if ( commit.Parents.Length > 1 )  {
        foreach( var parentHash in commit.Parents ) {
          var parent = lookUpTable[parentHash];
          var branch = FindOrCreateBranch(parent);
          branch.IsMerged = true;
        }
      }
      foreach( var branch in uniqueBranchesList ) {
        var isParentOfThis = false;
        foreach( var parentHash in commit.Parents ) {
          if( branch.Commits.ContainsKey(parentHash) ) {
            isParentOfThis = true;
            break;
          }
        }
        var isCurrent = branch.Commits.ContainsKey(commit.Hash);
        commit.Branches.Add( new BranchInfo( branch, isParentOfThis, isCurrent ) );

      }
      returnArray.Add(commit);
    }

    return returnArray;
  }

  private Branch FindOrCreateBranch(GitLogEntry commit)
  {
    if (branchesSearchTable.ContainsKey(commit.Hash))
    {
      var branch = branchesSearchTable[commit.Hash];
      branch.RegisterCommit( commit );
      return branch;
    }
    else
    {
      var newBranch = new Branch(branchIdGenerator++);
      newBranch.RegisterCommit( commit );
      branchesSearchTable.Add(commit.Hash, newBranch);
      uniqueBranchesList.Add(branchesSearchTable[commit.Hash]);
      newBranch.IsNew = true; 
      return newBranch;
    }
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
