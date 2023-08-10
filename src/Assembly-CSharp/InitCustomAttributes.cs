using System;
using System.Reflection;

public class InitCustomAttributes
{
	public static void Init()
	{
		Type[] exportedTypes = Assembly.GetAssembly(typeof(BindPrefab)).GetExportedTypes();
		foreach (Type type in exportedTypes)
		{
			Attribute[] customAttributes = Attribute.GetCustomAttributes(type, inherit: true);
			foreach (Attribute attribute in customAttributes)
			{
				if (attribute is BindPrefab)
				{
					BindUtil.Bind((attribute as BindPrefab).Path, type);
				}
			}
		}
	}
}
