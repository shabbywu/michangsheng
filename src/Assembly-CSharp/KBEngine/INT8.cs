namespace KBEngine;

public struct INT8
{
	private sbyte value;

	public static sbyte MaxValue => sbyte.MaxValue;

	public static sbyte MinValue => sbyte.MinValue;

	private INT8(sbyte value)
	{
		this.value = value;
	}

	public static implicit operator sbyte(INT8 value)
	{
		return value.value;
	}

	public static implicit operator INT8(sbyte value)
	{
		return new INT8(value);
	}
}
