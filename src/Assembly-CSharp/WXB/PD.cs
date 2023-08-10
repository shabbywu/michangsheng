using System;

namespace WXB;

internal struct PD<T> : IDisposable where T : new()
{
	public T value;

	private Action<T> free;

	public PD(Action<T> free)
	{
		value = PoolData<T>.Get();
		this.free = free;
	}

	public void Dispose()
	{
		free(value);
		PoolData<T>.Free(value);
	}
}
