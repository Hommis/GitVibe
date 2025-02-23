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
          var x = 10 ;
          var pen = new Pen(Brushes.RoyalBlue, 3);
          context.DrawLine(pen , new Point(x,0), new Point(x, renderSize.Height));
          context.DrawEllipse(Brushes.White, pen, new Point( x, renderSize.Height / 2 ), 5,5 );
          base.Render(context);
        }
    }