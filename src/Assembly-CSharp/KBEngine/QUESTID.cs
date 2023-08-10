namespace KBEngine;

public struct QUESTID
{
	private int value;

	public static int MaxValue => int.MaxValue;

	public static int MinValue => int.MinValue;

	private QUESTID(int value)
	{
		this.value = value;
	}

	public static implicit operator int(QUESTID value)
	{
		return value.value;
	}

	public static implicit operator QUESTID(int value)
	{
		return new QUESTID(value);
	}
}
