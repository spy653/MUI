namespace MUI;

public static class TUI
{
	public static bool CursorVisible { get; set; } = false;
	public static ConsoleColor BackgroundColour { get; set; } = ConsoleColor.Black;
	public static ConsoleColor ForegroundColour { get; set; } = ConsoleColor.White;

	// TODO: investigate improved line wrapping
	public static void Message(string text = "", uint level = 0, bool invert = false)
	{
		ConsoleColor targetColour = level switch
		{
			1 => ConsoleColor.Green,
			2 => ConsoleColor.Yellow,
			3 => ConsoleColor.Red,
			4 => ForegroundColour,
			_ => ConsoleColor.White
		};

		Console.ForegroundColor = invert ? BackgroundColour : targetColour;
		Console.BackgroundColor = invert ? targetColour : BackgroundColour;
		
		Write(text);
		Reset();
	}

	// TODO: tests for this.
	public static string Bar(double count, double total, uint width = 0)
	{
		if(width==0)
		{width = (uint)Console.WindowWidth;}
		width = width-=2;
		string bar = "|".PadLeft((int)((count/total)*width),'=');

		return $"[{bar.PadRight((int)width)}]";
	}

	public static void Write(string text)
	{
		try{
			if(text.Length>=Console.WindowWidth)
			{
				Console.WriteLine(text.Remove(Console.WindowWidth,text.Length-Console.WindowWidth));
			}else{
				Console.WriteLine(text.PadRight(Console.WindowWidth));
			}
		}catch{
			AskString("Console Write Error!");
		}
	}

	private static string Ask(string text,string input, uint level, bool inline)
	{
		string _text = text+((input==null)?"":" ["+input+"]");
		Message(_text,level);
		
		if(inline)	Console.SetCursorPosition(_text.Length+1,Console.CursorTop-1);

		string output = Console.ReadLine();

		if(output=="")
			return input;

		return output;
	}

	public static string AskString(string text ="", object input =null, uint level =4, bool inline =true)
	{
		return Ask(text,input?.ToString(),level,inline);			
	}

	public static int AskInt(string text ="", object input =null, uint level =4, bool inline =true)
	{
		return int.Parse(Ask(text,input?.ToString(),level,inline));
	}

	public static uint AskUint(string text ="", object input =null, uint level =4, bool inline =true)
	{
		return uint.Parse(Ask(text,input?.ToString(),level,inline));
	}

	public static void 		Reset()
	{
		Console.ForegroundColor = ForegroundColour;
		Console.BackgroundColor = BackgroundColour;
		Console.CursorVisible = CursorVisible;
	}
}