using System;
using UnityEngine;

// Token: 0x020002AE RID: 686
public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
	// Token: 0x1700026A RID: 618
	// (get) Token: 0x060014DB RID: 5339 RVA: 0x000BC48C File Offset: 0x000BA68C
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

	// Token: 0x04001008 RID: 4104
	protected static T _instance;
}
