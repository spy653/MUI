namespace MUI.TUI;

using System.Collections.Generic;

public class State
{
	public int		Index = 0;

	public List<Object> Args;
	public List<Action<State>> Options;
	public string[]	Definition;

	public	bool	EasyExit = true;
	public	bool	Running = true;

	public Action 	Init;
	public Action 	Update;
	public Action 	Exit;

	public void Enter()
	{
		Init();
		while (Running)
		{
			Draw();
			Input.Handle(this);
			Update();
		}
		Exit();
	}

	public void Draw()
	{
		foreach(string line in Definition)
		{
			// -- Gotta parse the Design language here.

			Screen.WriteTo(line,Screen.DefaultForeground,Screen.DefaultBackground);
		}

		Screen.Clear();
		Screen.Draw();
	}
}
