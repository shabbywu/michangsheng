using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000885 RID: 2181
	public class SceneReflection : MonoBehaviour
	{
		// Token: 0x06003847 RID: 14407 RVA: 0x00028E8E File Offset: 0x0002708E
		private IEnumerator Start()
		{
			WaitForSeconds waitInterval = new WaitForSeconds(0.2f);
			for (;;)
			{
				this.m_ReflectionProbe.refreshMode = 2;
				this.m_ReflectionProbe.RenderProbe();
				yield return waitInterval;
			}
			yield break;
		}

		// Token: 0x040032A0 RID: 12960
		[SerializeField]
		private ReflectionProbe m_ReflectionProbe;
	}
}
