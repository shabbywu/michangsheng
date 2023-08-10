namespace KBEngine;

public struct DAMAGE_TYPE
{
	private int value;

	public static int MaxValue => int.MaxValue;

	public static int MinValue => int.MinValue;

	private DAMAGE_TYPE(int value)
	{
		this.value = value;
	}

	public static implicit operator int(DAMAGE_TYPE value)
	{
		return value.value;
	}

	public static implicit operator DAMAGE_TYPE(int value)
	{
		return new DAMAGE_TYPE(value);
	}
}
