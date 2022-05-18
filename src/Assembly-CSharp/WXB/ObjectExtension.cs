using System;
using System.Collections.Generic;
using System.Text;

namespace WXB
{
	// Token: 0x020009A8 RID: 2472
	public static class ObjectExtension
	{
		// Token: 0x06003F00 RID: 16128 RVA: 0x001B887C File Offset: 0x001B6A7C
		public static StringBuilder AppendLine(this StringBuilder sb, string value, params string[] args)
		{
			return sb.AppendLine(string.Format(value, args));
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x0002D520 File Offset: 0x0002B720
		public static T pop_back<T>(this List<T> l)
		{
			T result = l[l.Count - 1];
			l.RemoveAt(l.Count - 1);
			return result;
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x0002D53E File Offset: 0x0002B73E
		public static T back<T>(this List<T> l)
		{
			return l[l.Count - 1];
		}
	}
}
