using System;
using System.Collections.Generic;

namespace SoftMasking
{
	// Token: 0x02000A06 RID: 2566
	internal struct ClearListAtExit<T> : IDisposable
	{
		// Token: 0x06004280 RID: 17024 RVA: 0x0002F6C8 File Offset: 0x0002D8C8
		public ClearListAtExit(List<T> list)
		{
			this._list = list;
		}

		// Token: 0x06004281 RID: 17025 RVA: 0x0002F6D1 File Offset: 0x0002D8D1
		public void Dispose()
		{
			this._list.Clear();
		}

		// Token: 0x04003AE4 RID: 15076
		private List<T> _list;
	}
}
