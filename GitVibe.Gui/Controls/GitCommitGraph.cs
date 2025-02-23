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
        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);
            InvalidateVisual();
        }

        public sealed override void Render(DrawingContext context)
        {
          if( Data == null ) return;
          var x = 10 ;
          foreach( var branch in Data.Branches ) {
            var renderSize = Bounds.Size;
            var pen = new Pen(Brushes.RoyalBlue, 3);


            if( branch.ParentIsNotFound) {
              var redPen = new Pen(Brushes.Red, 3); 
              context.DrawLine(redPen , new Point(x,5), new Point(x, renderSize.Height / 2));
              context.DrawEllipse(Brushes.White, redPen, new Point( x, 6 ), 5,5 );
            } else {
              if( !branch.IsNew) {
                context.DrawLine(pen , new Point(x,0), new Point(x, renderSize.Height / 2));
              }
            }

            context.DrawLine(pen , new Point(x,renderSize.Height / 2), new Point(x , renderSize.Height));
            if( branch.IsCurrent ) {
               context.DrawEllipse(Brushes.White, pen, new Point( x, renderSize.Height / 2 ), 5,5 );
            }
            x += 20;
          }
          base.Render(context);
        }
    }