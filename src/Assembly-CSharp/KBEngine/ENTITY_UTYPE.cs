namespace KBEngine;

public struct ENTITY_UTYPE
{
	private uint value;

	public static uint MaxValue => uint.MaxValue;

	public static uint MinValue => 0u;

	private ENTITY_UTYPE(uint value)
	{
		this.value = value;
	}

	public static implicit operator uint(ENTITY_UTYPE value)
	{
		return value.value;
	}

	public static implicit operator ENTITY_UTYPE(uint value)
	{
		return new ENTITY_UTYPE(value);
	}
}
