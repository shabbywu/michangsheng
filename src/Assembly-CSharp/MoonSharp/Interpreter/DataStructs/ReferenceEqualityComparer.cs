using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.DataStructs
{
	// Token: 0x02001187 RID: 4487
	internal class ReferenceEqualityComparer : IEqualityComparer<object>
	{
		// Token: 0x06006D60 RID: 28000 RVA: 0x0004A891 File Offset: 0x00048A91
		bool IEqualityComparer<object>.Equals(object x, object y)
		{
			return x == y;
		}

		// Token: 0x06006D61 RID: 28001 RVA: 0x0004A897 File Offset: 0x00048A97
		int IEqualityComparer<object>.GetHashCode(object obj)
		{
			return obj.GetHashCode();
		}
	}
}
