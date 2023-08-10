namespace KBEngine;

public struct OBJECT_ID
{
	private int value;

	public static int MaxValue => int.MaxValue;

	public static int MinValue => int.MinValue;

	private OBJECT_ID(int value)
	{
		this.value = value;
	}

	public static implicit operator int(OBJECT_ID value)
	{
		return value.value;
	}

	public static implicit operator OBJECT_ID(int value)
	{
		return new OBJECT_ID(value);
	}
}
