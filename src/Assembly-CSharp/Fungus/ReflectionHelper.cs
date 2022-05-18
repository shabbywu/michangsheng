using System;
using System.Collections.Generic;

namespace Fungus
{
	// Token: 0x02001220 RID: 4640
	public static class ReflectionHelper
	{
		// Token: 0x0600714E RID: 29006 RVA: 0x0004CF3A File Offset: 0x0004B13A
		public static Type GetType(string typeName)
		{
			if (ReflectionHelper.types.ContainsKey(typeName))
			{
				return ReflectionHelper.types[typeName];
			}
			ReflectionHelper.types[typeName] = Type.GetType(typeName);
			return ReflectionHelper.types[typeName];
		}

		// Token: 0x040063B5 RID: 25525
		private static Dictionary<string, Type> types = new Dictionary<string, Type>();
	}
}
