namespace MUI;

public static class Core{
	public  static string Title   { get; set; }
	public  static string Info    { get; set; }
	public  static State  Root    { get; set; }
	public static  State  State   { get; set; }

	public static bool Running = true;
	
	public static void Enter(State state = null)
	{
		try{
			State = state ?? Root;
			Running = true;
			
			TUI.Reset();

			while(Running)
			{
				State.Draw();
				State.Update();
				Input.Handle();
				if(Running) State.Update();
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