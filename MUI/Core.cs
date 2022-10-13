namespace MUI;

public static class Core{
	public  static bool   Running { get; set; }
	public  static string Title   { get; set; }
	public  static string Info 	  { get; set; }
	public  static State  Root 	  { get; set; }
	public  static State  State   { get => _state; set{Input.Selection=0; _state=value;} }
	private static State  _state;

	public static void 	Entry()
	{
		try{
			State = Root;
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
			TUI.Message($"Entry failed.",3,true);
			Console.WriteLine(e);
			TUI.AskString("[Enter to Continue]");
		}
	}
}