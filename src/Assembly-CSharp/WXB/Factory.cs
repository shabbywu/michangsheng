using System;

namespace WXB;

internal class Factory<T> : IFactory where T : new()
{
	public Action<T> free;

	public Factory(Action<T> f)
	{
		free = f;
	}

	public object create()
	{
		return new PD<T>(free);
	}
}
