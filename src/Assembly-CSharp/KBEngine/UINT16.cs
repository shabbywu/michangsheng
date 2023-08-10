namespace KBEngine;

public struct UINT16
{
	private ushort value;

	public static ushort MaxValue => ushort.MaxValue;

	public static ushort MinValue => 0;

	private UINT16(ushort value)
	{
		this.value = value;
	}

	public static implicit operator ushort(UINT16 value)
	{
		return value.value;
	}

	public static implicit operator UINT16(ushort value)
	{
		return new UINT16(value);
	}
}
