using System;
using System.Reflection;

// Token: 0x02000441 RID: 1089
public class InitCustomAttributes
{
	// Token: 0x06002296 RID: 8854 RVA: 0x000ED5E8 File Offset: 0x000EB7E8
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
