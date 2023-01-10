namespace MUI.TUI;

// -- Left here

public class Text
{
	public static string Ask(string prompt,string value = "",uint level = 0,bool inline = true,bool invert = false)
	{
		Screen.WriteTo(value != null ? $"{prompt}: [{value}]" : prompt,level switch{},Screen.BackgroundColour);

		if(inline)	Screen.Cursor = (prompt.Length+1,Screen.Cursor.Item2-1);

		string output = Console.ReadLine();

		return output=="" ? value : output;

	}

	// -- tricky shit
	public static string Bar(double value,uint length)
	{

		return "";
	}

	// --------------------------------------------------------------------------------------------

	public enum GroupType
	{
		Default,
		Bordered,
		Walled,
		Padded,
		PaddedBordered,
		PaddedWalled,
	}

	public enum LineType
	{
		Solid,
		Dashed,
		DoubleSolid,
		DoubleDashed,
	}

	// --------------------------------------------------------------------------------------------

	public static string Division(int divisions,string[] contents) {

	}

	public static string Group   (GroupType groupType,int size) {

	}

	public static string Line    (LineType lineType,int size)
	{
		string result = "";
		switch (lineType)
		{
			case LineType.Solid:
				result = "-".PadRight(size,'-'); break;
			case LineType.DoubleSolid:
				result = "=".PadRight(size,'='); break;
			case LineType.Dashed:		result = "";
				for (int i = 0;i < size;i++)
				{
					result += (i%2) switch
					{
						0 => " ",
						1 => "-"
					};
				}
				break;
			case LineType.DoubleDashed:	result = "";
				for (int i = 0;i < size;i++)
				{
					result += (i%2) switch
					{
						0 => " ",
						1 => "="
					};
				}
				break;
		}
		return result;
	}
}
