namespace KBEngine;

public struct PYTHON
{
	private byte[] value;

	public byte this[int ID]
	{
		get
		{
			return value[ID];
		}
		set
		{
			this.value[ID] = value;
		}
	}

	private PYTHON(byte[] value)
	{
		this.value = value;
	}

	public static implicit operator byte[](PYTHON value)
	{
		return value.value;
	}

	public static implicit operator PYTHON(byte[] value)
	{
		return new PYTHON(value);
	}
}
