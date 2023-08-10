namespace KBEngine;

public struct ENMITY
{
	private int value;

	public static int MaxValue => int.MaxValue;

	public static int MinValue => int.MinValue;

	private ENMITY(int value)
	{
		this.value = value;
	}

	public static implicit operator int(ENMITY value)
	{
		return value.value;
	}

	public static implicit operator ENMITY(int value)
	{
		return new ENMITY(value);
	}
}
