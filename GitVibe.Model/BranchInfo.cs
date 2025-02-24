using System;

namespace GitVibe.Model;

public record BranchInfo
{
  public bool IsNew { get; init; }
  public bool IsParentOfThis { get; init;} = false;
  public bool ParentIsMissing { get; init;} = false;
  public int Id { get; init; }
  public bool IsMerged { get; init; } = false;
  public bool IsCurrent { get; init; } = false;
  public BranchInfo( int id, bool isNew, bool isCurrent = false, bool parentIsNotFound = false, bool isMerged = false) {
    Id = id ;
    IsNew = isNew;  
    IsParentOfThis = isCurrent;
    ParentIsMissing = parentIsNotFound;
    IsMerged = isMerged;
  }
  public BranchInfo( Branch source, bool isParentOfThis, bool isCurrent ) {
    Id = source.BranchId;
    IsNew = source.IsNew;
    IsMerged = source.IsMerged;
    IsParentOfThis = isParentOfThis;
    IsCurrent = isCurrent;
  }
}