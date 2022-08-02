namespace MUI;

public static class Core{
	private static bool running = true;
	public static bool Running 	{ get => running; set => running = value; }

	private static string title = "";
	public static string Title 	{ get => title; set => title = value; }

	private static string info 	= "";
	public static string Info 	{ get => info; set => info = value; }

	private static State root 	= null;
	public static State Root 	{ get => root; set => root = value; }

	private static State state 	= null;
	public static State State 	{ get => state; set{Input.Selection=0; state=value;} }

	public static void 	Entry(){
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
			TUI.Message($"Entry failed. {e}",3,true);
			TUI.AskString("[Enter to Continue]");
		}
	}
}