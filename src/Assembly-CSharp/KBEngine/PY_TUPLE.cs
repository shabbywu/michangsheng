namespace KBEngine;

public struct PY_TUPLE
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

	private PY_TUPLE(byte[] value)
	{
		this.value = value;
	}

	public static implicit operator byte[](PY_TUPLE value)
	{
		return value.value;
	}

	public static implicit operator PY_TUPLE(byte[] value)
	{
		return new PY_TUPLE(value);
	}
}
