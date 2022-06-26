global using System;

namespace MUI;

// TODO: Move into TUI subclass in prep for GUI set.
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

	public static void Prime(){}

	public static void 	Entry(){
		try{
			Console.CursorVisible = false;
			
			State = Root;
			Running = true;

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
			TUI.AskString($"Entry failed.");
			TUI.Message($"{e}");
		}
	}
	
	// DEAD: Kill this.
	public static void 	Entry(State root){
		Root = root;
		Entry();
	}
}