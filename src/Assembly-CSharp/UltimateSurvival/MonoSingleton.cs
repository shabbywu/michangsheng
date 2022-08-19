using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200059B RID: 1435
	public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06002F2A RID: 12074 RVA: 0x001565F7 File Offset: 0x001547F7
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

		// Token: 0x04002971 RID: 10609
		private static T m_Instance;
	}
}
