namespace MUI;

public static class Core{
	public  static string Title   { get; set; }
	public  static string Info    { get; set; }
	public  static State  Root    { get; set; }
	public static  State  State   { get; set; }

	public static void Enter(State state = null)
	{
		try{
			State = state ?? Root;

			TUI.Reset();

			while(State.Running)
			{
				State.Draw();
				State.Update();
				Input.Handle();

//				if(State.Running)
//					State.Update();
			}
		}
		catch(Exception e)
		{
			TUI.Message($"Entry failed.", 3, true);
			Console.WriteLine(e);
			TUI.AskString("[Enter to Continue]");
		}
	}
}
