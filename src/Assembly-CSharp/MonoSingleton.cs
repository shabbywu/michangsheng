using System;
using UnityEngine;

// Token: 0x02000115 RID: 277
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
	// Token: 0x170001EE RID: 494
	// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00050A80 File Offset: 0x0004EC80
	public static T instance
	{
		get
		{
			if (MonoSingleton<T>.m_Instance == null)
			{
				MonoSingleton<T>.m_Instance = (Object.FindObjectOfType(typeof(T)) as T);
				if (MonoSingleton<T>.m_Instance == null)
				{
					MonoSingleton<T>.m_Instance = new GameObject("Singleton of " + typeof(T).ToString(), new Type[]
					{
						typeof(T)
					}).GetComponent<T>();
					MonoSingleton<T>.m_Instance.Init();
				}
			}
			return MonoSingleton<T>.m_Instance;
		}
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x00050B1E File Offset: 0x0004ED1E
	private void Awake()
	{
		if (MonoSingleton<T>.m_Instance == null)
		{
			MonoSingleton<T>.m_Instance = (this as T);
		}
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x00004095 File Offset: 0x00002295
	public virtual void Init()
	{
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x00050B42 File Offset: 0x0004ED42
	private void OnApplicationQuit()
	{
		MonoSingleton<T>.m_Instance = default(T);
	}

	// Token: 0x04000975 RID: 2421
	private static T m_Instance;
}
