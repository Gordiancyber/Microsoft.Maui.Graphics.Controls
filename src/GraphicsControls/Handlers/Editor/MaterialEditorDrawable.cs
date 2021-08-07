﻿using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Graphics.Controls
{
    public class MaterialEditorDrawable : ViewDrawable<IEditor>, IEditorDrawable
    {
        const float FocusedPlaceholderFontSize = 12f;
        const float UnfocusedPlaceholderFontSize = 16f;

        const float FocusedPlaceholderPosition = 6f;
        const float UnfocusedPlaceholderPosition = 22f;

        public bool HasFocus { get; set; }
        public double AnimationPercent { get; set; }

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IEditor editor)
        {
            canvas.SaveState();

            if (editor.Background != null)
                canvas.SetFillPaint(editor.Background, dirtyRect);
            else
            {
                if (Application.Current?.RequestedTheme == OSAppTheme.Light)
                    canvas.FillColor = editor.IsEnabled ? Material.Color.Light.Gray5.ToColor() : Material.Color.Light.Gray3.ToColor();
                else
                    canvas.FillColor = editor.IsEnabled ? Material.Color.Dark.Gray5.ToColor() : Material.Color.Dark.Gray3.ToColor();
            }

            var width = dirtyRect.Width;

            var vBuilder = new PathBuilder();
            var path =
                vBuilder.BuildPath(
                    $"M0 4C0 1.79086 1.79086 0 4 0H{width - 4}C{width - 2}.209 0 {width} 1.79086 {width} 4V114.95H0V4Z");

            canvas.FillPath(path);

            canvas.RestoreState();
        }

        public void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IEditor editor)
        {
            canvas.SaveState();

            var strokeWidth = 1.0f;
            canvas.FillColor = (Application.Current?.RequestedTheme == OSAppTheme.Light) ? Material.Color.Black.ToColor() : Material.Color.Light.Gray6.ToColor().WithAlpha(0.5f);

            if (editor.IsEnabled && HasFocus)
            {
                strokeWidth = 2.0f;
                canvas.FillColor = Material.Color.Blue.ToColor();
            }

            var x = dirtyRect.X;
            var y = 112.91f;

            var width = dirtyRect.Width;
            var height = strokeWidth;

            canvas.FillRectangle(x, y, width, height);

            canvas.RestoreState();
        }

        public void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, IEditor editor)
        {
            canvas.SaveState();

            canvas.FontColor = editor.PlaceholderColor.WithDefault(Material.Color.DarkBackground, Material.Color.LightBackground);

            var materialPlaceholderFontSize = UnfocusedPlaceholderFontSize.Lerp(FocusedPlaceholderFontSize, AnimationPercent);

            canvas.FontSize = materialPlaceholderFontSize;

            float margin = 12f;

            var horizontalAlignment = HorizontalAlignment.Left;

            var x = dirtyRect.X + margin;

            if (editor.FlowDirection == FlowDirection.RightToLeft)
            {
                x = dirtyRect.X;
                horizontalAlignment = HorizontalAlignment.Right;
            }

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            var materialPlaceholderPosition = UnfocusedPlaceholderPosition.Lerp(FocusedPlaceholderPosition, AnimationPercent);

            canvas.DrawString(editor.Placeholder, x, materialPlaceholderPosition, width - margin, height, horizontalAlignment, VerticalAlignment.Top);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 114.95d);
    }
}
