namespace KBEngine;

public struct UID
{
	private ulong value;

	public static ulong MaxValue => ulong.MaxValue;

	public static ulong MinValue => 0uL;

	private UID(ulong value)
	{
		this.value = value;
	}

	public static implicit operator ulong(UID value)
	{
		return value.value;
	}

	public static implicit operator UID(ulong value)
	{
		return new UID(value);
	}
}
