using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000857 RID: 2135
	public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060037A8 RID: 14248 RVA: 0x0002866B File Offset: 0x0002686B
		public static T Instance
		{
			get
			{
				if (MonoSingleton<T>.m_Instance == null)
				{
					MonoSingleton<T>.m_Instance = Object.FindObjectOfType<T>();
				}
				return MonoSingleton<T>.m_Instance;
			}
		}

		// Token: 0x040031F2 RID: 12786
		private static T m_Instance;
	}
}
