namespace KBEngine;

public struct FLOAT
{
	private float value;

	public static float MaxValue => float.MaxValue;

	public static float MinValue => float.MinValue;

	private FLOAT(float value)
	{
		this.value = value;
	}

	public static implicit operator float(FLOAT value)
	{
		return value.value;
	}

	public static implicit operator FLOAT(float value)
	{
		return new FLOAT(value);
	}
}
