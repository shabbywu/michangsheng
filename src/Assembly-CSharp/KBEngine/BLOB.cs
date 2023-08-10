namespace KBEngine;

public struct BLOB
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

	private BLOB(byte[] value)
	{
		this.value = value;
	}

	public static implicit operator byte[](BLOB value)
	{
		return value.value;
	}

	public static implicit operator BLOB(byte[] value)
	{
		return new BLOB(value);
	}
}
