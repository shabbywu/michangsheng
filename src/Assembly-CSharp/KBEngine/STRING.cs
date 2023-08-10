namespace KBEngine;

public struct STRING
{
	private string value;

	private STRING(string value)
	{
		this.value = value;
	}

	public static implicit operator string(STRING value)
	{
		return value.value;
	}

	public static implicit operator STRING(string value)
	{
		return new STRING(value);
	}
}
