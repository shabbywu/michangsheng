namespace KBEngine;

public struct UINT64
{
	private ulong value;

	public static ulong MaxValue => ulong.MaxValue;

	public static ulong MinValue => 0uL;

	private UINT64(ulong value)
	{
		this.value = value;
	}

	public static implicit operator ulong(UINT64 value)
	{
		return value.value;
	}

	public static implicit operator UINT64(ulong value)
	{
		return new UINT64(value);
	}
}
