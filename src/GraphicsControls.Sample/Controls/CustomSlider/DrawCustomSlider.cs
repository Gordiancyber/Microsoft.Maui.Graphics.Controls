﻿using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Controls;

namespace GraphicsControls.Sample.Controls
{
    public class DrawCustomSlider : Slider
    {

    }

    public class DrawCustomSliderHandler : SliderHandler
	{
		protected override ISliderDrawable CreateDrawable() => new MaterialSliderDrawable();

		public override void Draw(ICanvas canvas, RectangleF dirtyRect)
        {			
			canvas.SaveState();

			canvas.FontColor = Fluent.Color.Foreground.Black.ToColor();
			canvas.FontSize = 8f;

			var height = dirtyRect.Height;
			var width = dirtyRect.Width;
						
			var x = dirtyRect.X;
			var y = dirtyRect.Y;

			canvas.SetToBoldSystemFont();

			string valueString = VirtualView.Value.ToString("####0.00");

			canvas.DrawString(valueString, x, y, width, height, HorizontalAlignment.Right, VerticalAlignment.Top);

			canvas.RestoreState(); 
			
			base.Draw(canvas, dirtyRect);
		}
    }
}