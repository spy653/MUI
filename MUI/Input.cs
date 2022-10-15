using System.Threading;

namespace MUI;

public static class Input
{
	private static bool Selected { get; set; }

	public static void 	Handle()
	{
		if(Console.KeyAvailable)
		{
			ConsoleKey key = Console.ReadKey(true).Key;
		
			switch(key)
			{
				case ConsoleKey.UpArrow: 	
				case ConsoleKey.W:
					Core.State.Args[0] = (int)Core.State.Args[0]-1; 
				break;
				
				case ConsoleKey.DownArrow: 	
				case ConsoleKey.S:
					Core.State.Args[0] = (int)Core.State.Args[0]+1;
				break;
				
				case ConsoleKey.LeftArrow: 	
				case ConsoleKey.A:	
				case ConsoleKey.Backspace:
				case ConsoleKey.D0:
					Core.State = Core.Root;
				break;

				case ConsoleKey.RightArrow: 
				case ConsoleKey.D: 
				case ConsoleKey.Spacebar:
				case ConsoleKey.Enter: 		
					Selected = true; 
				break;
	
				case ConsoleKey.Escape:		
					Core.Running = false;
				break;

				case ConsoleKey.D1:	Core.State.Args[0] = 0; break;
				case ConsoleKey.D2:	Core.State.Args[0] = 1; break;
				case ConsoleKey.D3:	Core.State.Args[0] = 2; break;
				case ConsoleKey.D4:	Core.State.Args[0] = 3; break;
				case ConsoleKey.D5:	Core.State.Args[0] = 4; break;
				case ConsoleKey.D6:	Core.State.Args[0] = 5; break;
				case ConsoleKey.D7:	Core.State.Args[0] = 6; break;
				case ConsoleKey.D8:	Core.State.Args[0] = 7; break;
				case ConsoleKey.D9:	Core.State.Args[0] = 8; break;
			}
		}
		else
		{
			Thread.Sleep(100);
		}

		if(Selected && (Core.State.Options.Count>0))
		{
			try
			{
				Core.State.Options[(int)Core.State.Args[0]%Core.State.Options.Count].Function();
			}
			catch (Exception e)
			{
				TUI.Message( "Enter Selection Failed.",3);
				Console.WriteLine(e);
				TUI.AskString("[Enter to Continue]");
			}
			Selected = false;
		}
	}
}
