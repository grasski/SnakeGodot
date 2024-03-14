using Godot;
using System;
using System.Collections.Generic;

class Pixel
{
	public int XPos { get; set; }
	public int YPos { get; set; }
	public Color Color {get; set; }
}
class Snake: Pixel
{
	public List<Pixel> Tail { get; set; }
}

public partial class game : Node
{
	readonly Random randomNumber = new();


	int CellsX {get; set;}
	int CellsY {get; set;}
	int tileSize = 50;

	int Score { get; set; }

	[Export]
	PackedScene Segment { get; set; }

	[Export]
	CanvasLayer Ui { get; set; }
	Node Menu {get; set; }
	Node ScoreText {get; set; }


	Snake Snake { get; set; }
	Pixel Berry { get; set; }

	Direction direction = Direction.Right;
	bool CanMove = true;

	Timer timer { get; set; }
	Boolean GameRunning = true;

	enum Direction
	{
		Up,
		Down,
		Right,
		Left
	}
	Direction ReadDirection(Direction direction)
	{
		if (CanMove){
			if (Input.IsActionJustPressed("move_up") && direction != Direction.Down){
				direction = Direction.Up;
				CanMove = false;
			}
			else if (Input.IsActionJustPressed("move_down") && direction != Direction.Up)
			{
				direction = Direction.Down;
				CanMove = false;
			}
			else if (Input.IsActionJustPressed("move_left") && direction != Direction.Right)
			{
				direction = Direction.Left;
				CanMove = false;
			}
			else if (Input.IsActionJustPressed("move_right") && direction != Direction.Left)
			{
				direction = Direction.Right;
				CanMove = false;
			}
		}
		return direction;
	}

	void DrawPixel(Pixel pixel, bool isBerry)
	{
		var pos = new Vector2(pixel.XPos, pixel.YPos + 1) * tileSize;
		if (isBerry){
			var texture = GD.Load<Texture2D>("res://assets/apple.png");
			Sprite2D sprite = new Sprite2D
			{
				Texture = texture,
				Position = new Vector2(pos.X + (texture.GetSize().X/2), pos.Y + (texture.GetSize().Y/2))
			};
			AddChild(sprite);
		} else{
			var segment = Segment.Instantiate();

			segment.Set("position", pos);
			segment.Set("modulate", pixel.Color);
			AddChild(segment);
		}
	}

	void HandleBerryCollision()
		{
			if (Berry.XPos == Snake.XPos && Berry.YPos == Snake.YPos)
			{
				Score++;
				Berry.XPos = randomNumber.Next(0, CellsX - 1);
				Berry.YPos = randomNumber.Next(0, CellsY - 2);

				ScoreText.Set("text", "Skóre: " + Score);
			}
		}


	void SetupSizes(){
		
		int windowWidth = GetWindow().Size.X;
		int windowHeight = GetWindow().Size.Y;

		CellsX = windowWidth / tileSize;
		CellsY = windowHeight / tileSize;
	}

	public override void _Ready()
	{
		SetupSizes();


		Menu = Ui.FindChild("Menu");
		ScoreText = Ui.FindChild("ScoreText");

		Snake = new(){
			XPos = CellsX/2,
			YPos = CellsY/2,
			Color = new Color("#0a3e21"),

			Tail = new List<Pixel>(),
		};
		Berry = new()
		{
			XPos = randomNumber.Next(0, CellsY - 1),
			YPos = randomNumber.Next(0, CellsY - 2),
			Color = new Color("#f24aab")
		};

		timer = GetNode<Timer>("MoveTimer");
		timer.Start();

		Score = 3;
		ScoreText.Set("text", "Skóre: " + Score);

		DrawPixel(Berry, false);
		DrawPixel(Snake, true);
	}

	void ClearMap(){
		foreach(Node n in GetChildren()){
			if (n.Name != "Background" && n.Name != "UI" && n.Name != "MoveTimer"){
				RemoveChild(n);
			}
		}
	}

	bool CheckGameOver(Snake snake)
		{
			if (snake.XPos == CellsX || snake.XPos+1 == 0 || snake.YPos+1 == CellsY || snake.YPos+1 == 0)
			{
				return true;
			}
			for (int i=0; i < snake.Tail.Count; i++)
			{
				if (snake.Tail[i].XPos == snake.XPos && snake.Tail[i].YPos == snake.YPos)
				{
					return true;
				}
			}

			if (Score == (CellsX * CellsY)-1){
				return true;
			}

			return false;
		}

	public override void _Process(double delta)
	{
		direction = ReadDirection(direction);

		if (GameRunning){
			Menu.Set("visible", false);
		} else{
			Menu.Set("visible", true);
		}
	}

	public void OnStartTimerTimeout(){
		CanMove = true;
		ClearMap();

		if (CheckGameOver(Snake))
		{
			timer.Stop();
			GameRunning = false;
			GetTree().ChangeSceneToFile("res://ui.tscn");
		}

		HandleBerryCollision();

		DrawPixel(Snake, false);
		for (int i = 0; i < Snake.Tail.Count; i++)
		{
			DrawPixel(Snake.Tail[i], false);   // Draw snake tail
		}
		DrawPixel(Berry, true);

		Snake.Tail.Add(
			new Pixel{
				XPos = Snake.XPos,
				YPos = Snake.YPos,
				Color = new Color("#2ab167")
			}
		);
		// Ořezání ocasu hada, pokud je příliš dlouhý
		if (Snake.Tail.Count > Score)
		{
			Snake.Tail.RemoveAt(0);
		}

		switch (direction)
		{
			case Direction.Up:
				Snake.YPos--;
				break;
			case Direction.Down:
				Snake.YPos++;
				break;
			case Direction.Left:
				Snake.XPos--;
				break;
			case Direction.Right:
				Snake.XPos++;
				break;
		}
	}
}
