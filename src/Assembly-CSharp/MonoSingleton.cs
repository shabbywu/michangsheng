using System;
using UnityEngine;

// Token: 0x020001D5 RID: 469
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
	// Token: 0x17000217 RID: 535
	// (get) Token: 0x06000F43 RID: 3907 RVA: 0x000A1808 File Offset: 0x0009FA08
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

	// Token: 0x06000F44 RID: 3908 RVA: 0x0000F8C4 File Offset: 0x0000DAC4
	private void Awake()
	{
		if (MonoSingleton<T>.m_Instance == null)
		{
			MonoSingleton<T>.m_Instance = (this as T);
		}
	}

	// Token: 0x06000F45 RID: 3909 RVA: 0x000042DD File Offset: 0x000024DD
	public virtual void Init()
	{
	}

	// Token: 0x06000F46 RID: 3910 RVA: 0x0000F8E8 File Offset: 0x0000DAE8
	private void OnApplicationQuit()
	{
		MonoSingleton<T>.m_Instance = default(T);
	}

	// Token: 0x04000BF2 RID: 3058
	private static T m_Instance;
}
