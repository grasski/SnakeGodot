using Godot;
using System;

public partial class Background : CanvasGroup
{
	[Export]
	PackedScene Segment { get; set; }

	private int tileSize = 50; // Velikost jednoho políčka

	public override void _Ready()
	{
		int windowWidth = GetWindow().Size.X;
		int windowHeight = GetWindow().Size.Y;

		int horizonalCount = windowWidth / tileSize;
		int verticalCount = windowHeight / tileSize;

		for(int h=0; h < horizonalCount; h++){
			for(int v=1; v < verticalCount; v++){
				var segment = Segment.Instantiate();
				var position = new Vector2(h*tileSize, v*tileSize);
				var color = (v+h)%2==0 ? new Color("#33a8cd") : new Color("#bdebfc");

				segment.Set("position", position);
				segment.Set("modulate", color);
				AddChild(segment);
			}
		}
	}
}
