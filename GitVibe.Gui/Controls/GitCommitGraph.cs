using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using GitVibe.Model;

namespace GitVibe.Gui.Controls;

    public class GitCommitGraph : Control
    {
        public static readonly StyledProperty<GitLogEntry?> DataProperty =
            AvaloniaProperty.Register<GitCommitGraph, GitLogEntry?>(nameof(Data));

        public GitLogEntry? Data
        {
            get => GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        public sealed override void Render(DrawingContext context)
        {
          var renderSize = Bounds.Size;
          context.FillRectangle(Brushes.Red , new Rect(renderSize));
          base.Render(context);
        }
    }