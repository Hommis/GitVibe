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
      return commitsInSubtree.Select( commit => new GitLogEntry(commit) ).ToArray();  

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
