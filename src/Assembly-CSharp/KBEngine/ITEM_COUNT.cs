namespace KBEngine;

public struct ITEM_COUNT
{
	private uint value;

	public static uint MaxValue => uint.MaxValue;

	public static uint MinValue => 0u;

	private ITEM_COUNT(uint value)
	{
		this.value = value;
	}

	public static implicit operator uint(ITEM_COUNT value)
	{
		return value.value;
	}

	public static implicit operator ITEM_COUNT(uint value)
	{
		return new ITEM_COUNT(value);
	}
}
