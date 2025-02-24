using GitVibe.Model;
using LibGit2Sharp;

public class Branch {
  public int BranchId { get; set; }
  public bool IsMerged { get; set; } = false;
  public bool IsNew { get; set; } = false;
  public Dictionary<string, GitLogEntry> Commits { get; set; }  = new Dictionary<string, GitLogEntry>();
  public Branch( int branchId ) {
    BranchId = branchId;
  }
  public void RegisterCommit( GitLogEntry commit ) {
    if( !Commits.ContainsKey( commit.Hash)) {
      Commits.Add(commit.Hash, commit);
    }
  }
}