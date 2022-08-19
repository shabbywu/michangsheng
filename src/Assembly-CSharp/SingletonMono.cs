using System;
using UnityEngine;

// Token: 0x020001B0 RID: 432
public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
	// Token: 0x17000222 RID: 546
	// (get) Token: 0x06001230 RID: 4656 RVA: 0x0006E7A0 File Offset: 0x0006C9A0
	public static T Instance
	{
		get
		{
			if (SingletonMono<T>._instance == null)
			{
				return default(T);
			}
			return SingletonMono<T>._instance;
		}
	}

	// Token: 0x04000CDD RID: 3293
	protected static T _instance;
}
