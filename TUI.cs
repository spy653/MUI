namespace MUI;

public static class TUI{

	private static ConsoleColor uIColour = ConsoleColor.White;
	public static ConsoleColor UIColour { get => uIColour; set => uIColour = value; }

	// TODO: investigate improved line wrapping
	public static void Message(string text = "", uint level = 0, bool invert = false){
		ConsoleColor targetColour = level switch
		{
			1 => ConsoleColor.Green,
			2 => ConsoleColor.Yellow,
			3 => ConsoleColor.Red,
			4 => UIColour,
			_ => ConsoleColor.White
		};

		Console.ForegroundColor = invert ? ConsoleColor.Black : targetColour;
		Console.BackgroundColor = invert ? targetColour : ConsoleColor.Black;
		
		Write(text);
		Reset();
	}

	// TODO: tests for this.
	public static string Bar(double count, double total, uint width = 0){
		if(width==0)
		{width = (uint)Console.WindowWidth;}
		width = width-=2;
		string bar = "|".PadLeft((int)((count/total)*width),'=');

		return $"[{bar.PadRight((int)width)}]";
	}

	public static void Write(string text){
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

	public static string AskString(string text ="", object input =null, uint level =4, bool inline =false)
	{
		return Ask(text,input?.ToString(),level,inline);			
	}

	public static int AskInt(string text ="", object input =null, uint level =4, bool inline =false)
	{
		return int.Parse(Ask(text,input?.ToString(),level,inline));
	}

	public static uint AskUint(string text ="", object input =null, uint level =4, bool inline =false)
	{
		return uint.Parse(Ask(text,input?.ToString(),level,inline));
	}

	public static void 		Reset(){
		Console.ForegroundColor = UIColour;
		Console.BackgroundColor = ConsoleColor.Black;
	}
}