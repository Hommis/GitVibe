using System;

namespace GitVibe.Model;

public record BranchInfo
{
  public bool IsNew { get; init; }
  public bool HasParent { get; init;} = false;
  public bool IsCurrent { get; init;} = false;
  public bool ParentIsNotFound { get; init;} = false;
  public int Id { get; init; }
  public bool IsMered { get; init; } = false;
  public BranchInfo( int id, bool isNew, bool hasParent = false, bool isCurrent = false, bool parentIsNotFound = false, bool isMerged = false) {
    Id = id ;
    IsNew = isNew;  
    HasParent = hasParent;
    IsCurrent = isCurrent;
    ParentIsNotFound = parentIsNotFound;
    IsMered = isMerged;
  }
  
}