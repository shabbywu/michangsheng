using System;
using System.Collections.Generic;

namespace Fungus
{
	// Token: 0x02000DDF RID: 3551
	public static class ReflectionHelper
	{
		// Token: 0x060064C2 RID: 25794 RVA: 0x00280973 File Offset: 0x0027EB73
		public static Type GetType(string typeName)
		{
			if (ReflectionHelper.types.ContainsKey(typeName))
			{
				return ReflectionHelper.types[typeName];
			}
			ReflectionHelper.types[typeName] = Type.GetType(typeName);
			return ReflectionHelper.types[typeName];
		}

		// Token: 0x040056AE RID: 22190
		private static Dictionary<string, Type> types = new Dictionary<string, Type>();
	}
}
