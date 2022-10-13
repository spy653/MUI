using System.Threading;

namespace MUI;

public static class Input
{
	public static uint Selection{ get; set; }
	public static bool Selected { get; set; }

	public static void 	Handle()
	{
		if(Console.KeyAvailable)
		{
			ConsoleKey key = Console.ReadKey(true).Key;
		
			switch(key)
			{
				default: break;
				case ConsoleKey.UpArrow: 	
				case ConsoleKey.W:
					Selection--;
					Selection %= (uint)Core.State.Options.Count;
				break;
				
				case ConsoleKey.DownArrow: 	
				case ConsoleKey.S:
					Selection++;
					Selection %= (uint)Core.State.Options.Count;
				break;
				
				case ConsoleKey.LeftArrow: 	
				case ConsoleKey.A: 			
				case ConsoleKey.Backspace:	
				case ConsoleKey.D0:			
					Selection= 0;
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

				case ConsoleKey.D1:			Selection = 0; break;
				case ConsoleKey.D2:			Selection = 1; break;
				case ConsoleKey.D3:			Selection = 2; break;
				case ConsoleKey.D4:			Selection = 3; break;
				case ConsoleKey.D5:			Selection = 4; break;
				case ConsoleKey.D6:			Selection = 5; break;
				case ConsoleKey.D7:			Selection = 6; break;
				case ConsoleKey.D8:			Selection = 7; break;
				case ConsoleKey.D9:			Selection = 8; break;
			}
		}
		else
		{
			Thread.Sleep(250);
		}

		if(Selected && (Core.State.Options.Count>0))
		{
			try
			{
				Core.State.Options[(int)Selection%Core.State.Options.Count].Function();
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
