namespace KBEngine;

public struct UNICODE
{
	private string value;

	private UNICODE(string value)
	{
		this.value = value;
	}

	public static implicit operator string(UNICODE value)
	{
		return value.value;
	}

	public static implicit operator UNICODE(string value)
	{
		return new UNICODE(value);
	}
}
