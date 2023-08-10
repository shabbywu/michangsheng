namespace KBEngine;

public struct ITEM_ID
{
	private int value;

	public static int MaxValue => int.MaxValue;

	public static int MinValue => int.MinValue;

	private ITEM_ID(int value)
	{
		this.value = value;
	}

	public static implicit operator int(ITEM_ID value)
	{
		return value.value;
	}

	public static implicit operator ITEM_ID(int value)
	{
		return new ITEM_ID(value);
	}
}
