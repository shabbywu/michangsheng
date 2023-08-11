namespace KBEngine;

public struct EXPERIENCE
{
	private int value;

	public static int MaxValue => int.MaxValue;

	public static int MinValue => int.MinValue;

	private EXPERIENCE(int value)
	{
		this.value = value;
	}

	public static implicit operator int(EXPERIENCE value)
	{
		return value.value;
	}

	public static implicit operator EXPERIENCE(int value)
	{
		return new EXPERIENCE(value);
	}
}
