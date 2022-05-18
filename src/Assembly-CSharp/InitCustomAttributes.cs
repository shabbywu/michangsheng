using System;
using System.Reflection;

// Token: 0x020005F8 RID: 1528
public class InitCustomAttributes
{
	// Token: 0x06002655 RID: 9813 RVA: 0x0012E428 File Offset: 0x0012C628
	public static void Init()
	{
		foreach (Type type in Assembly.GetAssembly(typeof(BindPrefab)).GetExportedTypes())
		{
			foreach (Attribute attribute in Attribute.GetCustomAttributes(type, true))
			{
				if (attribute is BindPrefab)
				{
					BindUtil.Bind((attribute as BindPrefab).Path, type);
				}
			}
		}
	}
}
