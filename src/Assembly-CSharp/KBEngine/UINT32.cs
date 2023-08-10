namespace KBEngine;

public struct UINT32
{
	private uint value;

	public static uint MaxValue => uint.MaxValue;

	public static uint MinValue => 0u;

	private UINT32(uint value)
	{
		this.value = value;
	}

	public static implicit operator uint(UINT32 value)
	{
		return value.value;
	}

	public static implicit operator UINT32(uint value)
	{
		return new UINT32(value);
	}
}
