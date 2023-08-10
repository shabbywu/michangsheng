namespace KBEngine;

public struct ENTITY_STATE
{
	private sbyte value;

	public static sbyte MaxValue => sbyte.MaxValue;

	public static sbyte MinValue => sbyte.MinValue;

	private ENTITY_STATE(sbyte value)
	{
		this.value = value;
	}

	public static implicit operator sbyte(ENTITY_STATE value)
	{
		return value.value;
	}

	public static implicit operator ENTITY_STATE(sbyte value)
	{
		return new ENTITY_STATE(value);
	}
}
