using System;
using System.Collections.Generic;

namespace SoftMasking;

internal struct ClearListAtExit<T> : IDisposable
{
	private List<T> _list;

	public ClearListAtExit(List<T> list)
	{
		_list = list;
	}

	public void Dispose()
	{
		_list.Clear();
	}
}
