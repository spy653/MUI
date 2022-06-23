using System;
using System.Collections.Generic;

namespace MUI
{
    public class State
	{
		public string 			Header 	= "";
		public string 			Info	= "";
		public List<NavOption> 	Options	= new ();
		public Action 			Update	= ()=>{};

		public class NavOption{
			public string 			Text = "";
			public Action 			Function = ()=>{};

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

		public void 			Draw(){
			Console.Clear();	
			TUI.Message($" [{Core.Title}] [{Header}] {Core.Info}",4,true);
			TUI.Message(Info, 4);
			TUI.Message();

			foreach(NavOption n in Options)
			{	
				TUI.Message(Options.IndexOf(n)+1+"| "+n.Text, 4, Options.IndexOf(n)==(Input.Selection%Options.Count));	
			}

			TUI.Message();
		}
	}
}