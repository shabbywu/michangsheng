namespace KBEngine;

public struct PY_DICT
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

	private PY_DICT(byte[] value)
	{
		this.value = value;
	}

	public static implicit operator byte[](PY_DICT value)
	{
		return value.value;
	}

	public static implicit operator PY_DICT(byte[] value)
	{
		return new PY_DICT(value);
	}
}
