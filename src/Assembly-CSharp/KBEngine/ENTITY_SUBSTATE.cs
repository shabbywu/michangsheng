namespace KBEngine;

public struct ENTITY_SUBSTATE
{
	private byte value;

	public static byte MaxValue => byte.MaxValue;

	public static byte MinValue => 0;

	private ENTITY_SUBSTATE(byte value)
	{
		this.value = value;
	}

	public static implicit operator byte(ENTITY_SUBSTATE value)
	{
		return value.value;
	}

	public static implicit operator ENTITY_SUBSTATE(byte value)
	{
		return new ENTITY_SUBSTATE(value);
	}
}
