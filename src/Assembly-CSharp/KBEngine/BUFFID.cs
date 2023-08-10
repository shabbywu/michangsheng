namespace KBEngine;

public struct BUFFID
{
	private ushort value;

	public static ushort MaxValue => ushort.MaxValue;

	public static ushort MinValue => 0;

	private BUFFID(ushort value)
	{
		this.value = value;
	}

	public static implicit operator ushort(BUFFID value)
	{
		return value.value;
	}

	public static implicit operator BUFFID(ushort value)
	{
		return new BUFFID(value);
	}
}
