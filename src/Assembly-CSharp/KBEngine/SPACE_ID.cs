namespace KBEngine;

public struct SPACE_ID
{
	private uint value;

	public static uint MaxValue => uint.MaxValue;

	public static uint MinValue => 0u;

	private SPACE_ID(uint value)
	{
		this.value = value;
	}

	public static implicit operator uint(SPACE_ID value)
	{
		return value.value;
	}

	public static implicit operator SPACE_ID(uint value)
	{
		return new SPACE_ID(value);
	}
}
