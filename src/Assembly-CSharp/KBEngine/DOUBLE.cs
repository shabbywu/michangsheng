namespace KBEngine;

public struct DOUBLE
{
	private double value;

	public static double MaxValue => double.MaxValue;

	public static double MinValue => double.MinValue;

	private DOUBLE(double value)
	{
		this.value = value;
	}

	public static implicit operator double(DOUBLE value)
	{
		return value.value;
	}

	public static implicit operator DOUBLE(double value)
	{
		return new DOUBLE(value);
	}
}
