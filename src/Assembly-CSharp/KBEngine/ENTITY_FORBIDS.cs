namespace KBEngine;

public struct ENTITY_FORBIDS
{
	private int value;

	public static int MaxValue => int.MaxValue;

	public static int MinValue => int.MinValue;

	private ENTITY_FORBIDS(int value)
	{
		this.value = value;
	}

	public static implicit operator int(ENTITY_FORBIDS value)
	{
		return value.value;
	}

	public static implicit operator ENTITY_FORBIDS(int value)
	{
		return new ENTITY_FORBIDS(value);
	}
}
