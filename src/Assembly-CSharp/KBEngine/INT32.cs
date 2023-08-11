namespace KBEngine;

public struct INT32
{
	private int value;

	public static int MaxValue => int.MaxValue;

	public static int MinValue => int.MinValue;

	private INT32(int value)
	{
		this.value = value;
	}

	public static implicit operator int(INT32 value)
	{
		return value.value;
	}

	public static implicit operator INT32(int value)
	{
		return new INT32(value);
	}
}
