namespace KBEngine;

public struct CONTROLLER_ID
{
	private int value;

	public static int MaxValue => int.MaxValue;

	public static int MinValue => int.MinValue;

	private CONTROLLER_ID(int value)
	{
		this.value = value;
	}

	public static implicit operator int(CONTROLLER_ID value)
	{
		return value.value;
	}

	public static implicit operator CONTROLLER_ID(int value)
	{
		return new CONTROLLER_ID(value);
	}
}
