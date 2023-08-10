namespace KBEngine;

public struct INT64
{
	private long value;

	public static long MaxValue => long.MaxValue;

	public static long MinValue => long.MinValue;

	private INT64(long value)
	{
		this.value = value;
	}

	public static implicit operator long(INT64 value)
	{
		return value.value;
	}

	public static implicit operator INT64(long value)
	{
		return new INT64(value);
	}
}
