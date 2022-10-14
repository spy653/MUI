using System.Collections.Generic;

namespace MUI;

public class State
{
	public string          Header  = "";
	public string          Info    = "";
	public List<NavOption> Options = new ();
	public Action          Update  = ()=>{};
	public List<object>    Args    = new (){0};
	
	public bool			   Running = true;

	public void		Enter()
	{
		try{
			Running = true;

			TUI.Reset();

			while(Running)
			{
				Draw();
				Update();
				Input.Handle();
				if(Running) Update();
			}
		}
		catch(Exception e)
		{
			TUI.Message($"Entry failed.", 3, true);
			Console.WriteLine(e);
			TUI.AskString("[Enter to Continue]");
		}
	}

	public class NavOption
	{
		public string 		Text = "";
		public Action 		Function = ()=>{};
		
		
		public NavOption(string text = "")
		{
			Text = text;
			Function = ()=>{};
		}

		public NavOption(string text,Action function)
		{
			Text = text;
			Function = function;
		}
	}

	public void Draw()
	{
		Console.Clear();	
		TUI.Message($" [{Core.Title}] [{Header}] {Core.Info}",4,true);
		TUI.Message(Info, 4);

		foreach(NavOption n in Options)
		{
			int i = Options.IndexOf(n);
			TUI.Message($" {i+1} | "+n.Text, 4, i==(int)Args[0]%Options.Count);	
		}
	}
}