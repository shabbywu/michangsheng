namespace KBEngine;

public struct UID1
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

	private UID1(byte[] value)
	{
		this.value = value;
	}

	public static implicit operator byte[](UID1 value)
	{
		return value.value;
	}

	public static implicit operator UID1(byte[] value)
	{
		return new UID1(value);
	}
}
