namespace KBEngine;

public struct ENTITY_ID
{
	private int value;

	public static int MaxValue => int.MaxValue;

	public static int MinValue => int.MinValue;

	private ENTITY_ID(int value)
	{
		this.value = value;
	}

	public static implicit operator int(ENTITY_ID value)
	{
		return value.value;
	}

	public static implicit operator ENTITY_ID(int value)
	{
		return new ENTITY_ID(value);
	}
}
