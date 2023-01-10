namespace MUI.TUI;

public struct Point
{
	public int X;
	public int Y;

	public Point(int x,int y)
	{
		X = x;
		Y = y;
	}
}

public class Buffer<T>
{
	public T[,] Array;

	public void Fill(T input)
	{
		Itterate(Screen.Size,(x,y,array) => { array[x,y] = input; });
	}

	public void Itterate(Action<int,int,T[,]> action)
	{
		Itterate(new(0,0),Screen.Size,action);
	}

	public void Itterate(Point size, Action<int,int,T[,]> action)
	{
		Itterate(new(0,0),size,action);
	}

	public void Itterate(Point origin,Point size, Action<int,int,T[,]> action)
	{
		for		(int x = origin.X; x < size.X; x++)
		{	for (int y = origin.Y; y < size.Y; y++)
			{
				action.Invoke(x,y,Array);
			}
		}
	}
}

public static class Screen
{
	public static Point					Size;
	public static Point					Cursor;

	public static Buffer<char>			CharacterBuffer;
	public static Buffer<ConsoleColor>	ForegroundBuffer;
	public static Buffer<ConsoleColor>	BackgroundBuffer;

	public static ConsoleColor 			DefaultForeground;
	public static ConsoleColor 			DefaultBackground;

	public static void Clear()
	{
		Fill(' ',DefaultForeground,DefaultBackground);
	}

	public static void Fill(char character, ConsoleColor foreground, ConsoleColor background)
	{
		CharacterBuffer.Fill(character);
		ForegroundBuffer.Fill(foreground);
		BackgroundBuffer.Fill(background);
	}

	public static void Draw()
	{
		CharacterBuffer.Itterate(Size,(x,y,array) => {
			Console.SetCursorPosition(Cursor.X,Cursor.Y);
			Console.ForegroundColor = ForegroundBuffer.Array[x,y];
			Console.BackgroundColor = BackgroundBuffer.Array[x,y];
			Console.Write(array[x,y]);
		});
	}

	public static void WriteTo(string text,ConsoleColor foreground, ConsoleColor background)
	{
		foreach (char rawCharacter in text)
		{
			char proccessedCharacter;

			// Special character processing
			if (CharacterBuffer.Array[Cursor.X-1,Cursor.Y] == '\\')
			{
				Cursor.X--;
				CharacterBuffer.Array[Cursor.X,Cursor.Y] = ' ';

				switch (rawCharacter)
				{
					case 'n': Cursor.X = 0;
						// New Line
						// Guard clause
						if (Cursor.Y == Size.Y)
							return;
						Cursor.Y++;
						proccessedCharacter = ' ';
					break;
					case 'r':
						// Carriage Return
						Cursor.X = 0;
						proccessedCharacter = ' ';
					break;
					case 't':
						// Tab
						WriteTo("    ",foreground,background);
					return;
					case 'e':
						// End
						WriteTo(" ".PadLeft(Size.X-Cursor.X),foreground,background);
						return;
					default:
						proccessedCharacter = ' ';
					break;
				}
			}
			else
			{
				proccessedCharacter = rawCharacter;
			}

			CharacterBuffer.Array [Cursor.X,Cursor.Y] = proccessedCharacter;
			ForegroundBuffer.Array[Cursor.X,Cursor.Y] = foreground;
			BackgroundBuffer.Array[Cursor.X,Cursor.Y] = background;

			// Guard clause
			if (Cursor.X == Size.X)
				return;
			Cursor.X++;
		}
	}
}
