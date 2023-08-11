namespace KBEngine;

public struct MP
{
	private int value;

	public static int MaxValue => int.MaxValue;

	public static int MinValue => int.MinValue;

	private MP(int value)
	{
		this.value = value;
	}

	public static implicit operator int(MP value)
	{
		return value.value;
	}

	public static implicit operator MP(int value)
	{
		return new MP(value);
	}
}
