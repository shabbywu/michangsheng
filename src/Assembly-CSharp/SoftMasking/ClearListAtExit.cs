using System;
using System.Collections.Generic;

namespace SoftMasking
{
	// Token: 0x020006D8 RID: 1752
	internal struct ClearListAtExit<T> : IDisposable
	{
		// Token: 0x0600385E RID: 14430 RVA: 0x0018335E File Offset: 0x0018155E
		public ClearListAtExit(List<T> list)
		{
			this._list = list;
		}

		// Token: 0x0600385F RID: 14431 RVA: 0x00183367 File Offset: 0x00181567
		public void Dispose()
		{
			this._list.Clear();
		}

		// Token: 0x040030CC RID: 12492
		private List<T> _list;
	}
}
