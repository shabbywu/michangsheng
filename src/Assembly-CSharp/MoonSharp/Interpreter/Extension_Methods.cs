using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter
{
	// Token: 0x0200105B RID: 4187
	internal static class Extension_Methods
	{
		// Token: 0x06006495 RID: 25749 RVA: 0x00281ADC File Offset: 0x0027FCDC
		public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
		{
			TValue result;
			if (dictionary.TryGetValue(key, out result))
			{
				return result;
			}
			return default(TValue);
		}

		// Token: 0x06006496 RID: 25750 RVA: 0x00281B00 File Offset: 0x0027FD00
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
