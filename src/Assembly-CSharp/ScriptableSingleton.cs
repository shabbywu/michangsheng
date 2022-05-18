using System;
using UnityEngine;

// Token: 0x020007AB RID: 1963
public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject
{
	// Token: 0x1700045C RID: 1116
	// (get) Token: 0x060031F8 RID: 12792 RVA: 0x0018D0C8 File Offset: 0x0018B2C8
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

	// Token: 0x04002E28 RID: 11816
	private static T m_Instance;
}
