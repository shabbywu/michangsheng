namespace KBEngine;

public struct SKILLID
{
	private int value;

	public static int MaxValue => int.MaxValue;

	public static int MinValue => int.MinValue;

	private SKILLID(int value)
	{
		this.value = value;
	}

	public static implicit operator int(SKILLID value)
	{
		return value.value;
	}

	public static implicit operator SKILLID(int value)
	{
		return new SKILLID(value);
	}
}
