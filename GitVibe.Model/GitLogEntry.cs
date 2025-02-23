using System;
using System.ComponentModel.DataAnnotations;
using LibGit2Sharp;

namespace GitVibe.Model;

public class GitLogEntry
{
    public string Message { get; set; }
    public string Author { get; set; }
    public DateTime CreateDate { get; set; }
    public GitLogEntry(Commit entry )
    {
        Message = entry.Message;
        Author = $"{entry.Author.Name} <{entry.Author.Email}>" ;
        CreateDate = entry.Author.When.DateTime;
    }
}
