using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.DataStructs
{
	// Token: 0x02000D71 RID: 3441
	internal class ReferenceEqualityComparer : IEqualityComparer<object>
	{
		// Token: 0x0600616C RID: 24940 RVA: 0x002736AB File Offset: 0x002718AB
		bool IEqualityComparer<object>.Equals(object x, object y)
		{
			return x == y;
		}

		// Token: 0x0600616D RID: 24941 RVA: 0x002736B1 File Offset: 0x002718B1
		int IEqualityComparer<object>.GetHashCode(object obj)
		{
			return obj.GetHashCode();
		}
	}
}
