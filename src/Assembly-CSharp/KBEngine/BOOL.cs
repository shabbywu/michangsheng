namespace KBEngine;

public struct BOOL
{
	private byte value;

	public static byte MaxValue => byte.MaxValue;

	public static byte MinValue => 0;

	private BOOL(byte value)
	{
		this.value = value;
	}

	public static implicit operator byte(BOOL value)
	{
		return value.value;
	}

	public static implicit operator BOOL(byte value)
	{
		return new BOOL(value);
	}
}
