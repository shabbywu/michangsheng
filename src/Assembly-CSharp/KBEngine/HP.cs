namespace KBEngine;

public struct HP
{
	private int value;

	public static int MaxValue => int.MaxValue;

	public static int MinValue => int.MinValue;

	private HP(int value)
	{
		this.value = value;
	}

	public static implicit operator int(HP value)
	{
		return value.value;
	}

	public static implicit operator HP(int value)
	{
		return new HP(value);
	}
}
