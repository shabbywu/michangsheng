using System;
using System.Collections.Generic;
using System.Text;

namespace WXB
{
	// Token: 0x02000694 RID: 1684
	public static class ObjectExtension
	{
		// Token: 0x06003542 RID: 13634 RVA: 0x0017066C File Offset: 0x0016E86C
		public static StringBuilder AppendLine(this StringBuilder sb, string value, params string[] args)
		{
			return sb.AppendLine(string.Format(value, args));
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x00170688 File Offset: 0x0016E888
		public static T pop_back<T>(this List<T> l)
		{
			T result = l[l.Count - 1];
			l.RemoveAt(l.Count - 1);
			return result;
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x001706A6 File Offset: 0x0016E8A6
		public static T back<T>(this List<T> l)
		{
			return l[l.Count - 1];
		}
	}
}
