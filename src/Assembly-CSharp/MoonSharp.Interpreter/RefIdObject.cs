namespace MoonSharp.Interpreter;

public class RefIdObject
{
	private static int s_RefIDCounter;

	private int m_RefID = ++s_RefIDCounter;

	public int ReferenceID => m_RefID;

	public string FormatTypeString(string typeString)
	{
		return $"{typeString}: {m_RefID:X8}";
	}
}
