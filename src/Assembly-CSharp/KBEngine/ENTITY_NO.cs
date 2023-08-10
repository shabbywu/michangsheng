namespace KBEngine;

public struct ENTITY_NO
{
	private uint value;

	public static uint MaxValue => uint.MaxValue;

	public static uint MinValue => 0u;

	private ENTITY_NO(uint value)
	{
		this.value = value;
	}

	public static implicit operator uint(ENTITY_NO value)
	{
		return value.value;
	}

	public static implicit operator ENTITY_NO(uint value)
	{
		return new ENTITY_NO(value);
	}
}
