namespace KBEngine;

public struct INT16
{
	private short value;

	public static short MaxValue => short.MaxValue;

	public static short MinValue => short.MinValue;

	private INT16(short value)
	{
		this.value = value;
	}

	public static implicit operator short(INT16 value)
	{
		return value.value;
	}

	public static implicit operator INT16(short value)
	{
		return new INT16(value);
	}
}
