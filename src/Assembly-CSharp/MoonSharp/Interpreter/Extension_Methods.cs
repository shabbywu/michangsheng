using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000C95 RID: 3221
	internal static class Extension_Methods
	{
		// Token: 0x060059D3 RID: 22995 RVA: 0x00256B34 File Offset: 0x00254D34
		public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
		{
			TValue result;
			if (dictionary.TryGetValue(key, out result))
			{
				return result;
			}
			return default(TValue);
		}

		// Token: 0x060059D4 RID: 22996 RVA: 0x00256B58 File Offset: 0x00254D58
		public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> creator)
		{
			TValue tvalue;
			if (!dictionary.TryGetValue(key, out tvalue))
			{
				tvalue = creator();
				dictionary.Add(key, tvalue);
			}
			return tvalue;
		}
	}
}
