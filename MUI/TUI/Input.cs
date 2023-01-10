using System.Threading;

namespace MUI.TUI;

public static class Input
{
	public static void 	Handle(State state)
	{
		if(Console.KeyAvailable)
		{
			ConsoleKey key = Console.ReadKey(true).Key;

			switch(key)
			{
				case ConsoleKey.UpArrow or
					ConsoleKey.LeftArrow or
					ConsoleKey.W or
					ConsoleKey.A:
						state.Index++;
				break;

				case ConsoleKey.DownArrow or
					ConsoleKey.RightArrow or
					ConsoleKey.S or
					ConsoleKey.D:
						state.Index--;
				break;

				case ConsoleKey.Enter:
					try
					{
						state.Options[state.Index].Invoke(state);
					}
					catch (Exception e)
					{
						Text.Write("Failed to execute.",3);
						Console.WriteLine(e);
						Text.Ask("[Enter] to continue.","");
					}

					break;

				case ConsoleKey.Backspace or
					ConsoleKey.Escape:
						if(state.EasyExit) state.Exit();
				break;
			}
		}
		else
		{
			Thread.Sleep(100);
		}
	}
}
