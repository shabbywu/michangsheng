namespace KBEngine;

public struct DBID
{
	private ulong value;

	public static ulong MaxValue => ulong.MaxValue;

	public static ulong MinValue => 0uL;

	private DBID(ulong value)
	{
		this.value = value;
	}

	public static implicit operator ulong(DBID value)
	{
		return value.value;
	}

	public static implicit operator DBID(ulong value)
	{
		return new DBID(value);
	}
}
