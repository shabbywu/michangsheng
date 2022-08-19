using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005C4 RID: 1476
	public class SceneReflection : MonoBehaviour
	{
		// Token: 0x06002FBD RID: 12221 RVA: 0x001589BD File Offset: 0x00156BBD
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

		// Token: 0x04002A0E RID: 10766
		[SerializeField]
		private ReflectionProbe m_ReflectionProbe;
	}
}
