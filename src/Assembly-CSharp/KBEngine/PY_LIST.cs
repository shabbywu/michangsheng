namespace KBEngine;

public struct PY_LIST
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

	private PY_LIST(byte[] value)
	{
		this.value = value;
	}

	public static implicit operator byte[](PY_LIST value)
	{
		return value.value;
	}

	public static implicit operator PY_LIST(byte[] value)
	{
		return new PY_LIST(value);
	}
}
