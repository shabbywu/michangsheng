using System;
using UnityEngine;

// Token: 0x02000517 RID: 1303
public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject
{
	// Token: 0x170002BD RID: 701
	// (get) Token: 0x060029E5 RID: 10725 RVA: 0x0013FE28 File Offset: 0x0013E028
	public static T Instance
	{
		get
		{
			if (!ScriptableSingleton<T>.m_Instance)
			{
				T[] array = Resources.LoadAll<T>("");
				if (array.Length != 0)
				{
					ScriptableSingleton<T>.m_Instance = array[0];
				}
				else
				{
					Debug.LogErrorFormat("[ScriptableSingleton] - No object of type '{0}' was found in the project!", new object[]
					{
						typeof(T).Name
					});
				}
			}
			return ScriptableSingleton<T>.m_Instance;
		}
	}

	// Token: 0x04002638 RID: 9784
	private static T m_Instance;
}
