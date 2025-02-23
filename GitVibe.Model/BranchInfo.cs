using System;

namespace GitVibe.Model;

public record BranchInfo
{
  public bool IsNew { get; init; }
  public bool HasParent { get; init;} = false;
  public bool IsCurrent { get; init;} = false;
  public bool ParentIsNotFound { get; init;} = false; 
  public BranchInfo( bool isNew, bool hasParent = false, bool isCurrent = false, bool parentIsNotFound = false) {
    IsNew = isNew;  
    HasParent = hasParent;
    IsCurrent = isCurrent;
    ParentIsNotFound = parentIsNotFound;
  }
  
}
