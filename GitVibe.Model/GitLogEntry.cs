using System;

namespace GitVibe.Model;

public class GitLogEntry
{
    public string Message { get; set; }
    public string Author { get; set; }
    public GitLogEntry(string message, string author)
    {
        Message = message;
        Author = author;
    }
}
