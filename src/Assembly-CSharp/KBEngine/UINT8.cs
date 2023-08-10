namespace KBEngine;

public struct UINT8
{
	private byte value;

	public static byte MaxValue => byte.MaxValue;

	public static byte MinValue => 0;

	private UINT8(byte value)
	{
		this.value = value;
	}

	public static implicit operator byte(UINT8 value)
	{
		return value.value;
	}

	public static implicit operator UINT8(byte value)
	{
		return new UINT8(value);
	}
}
