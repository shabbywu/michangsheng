using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.StandardAssets
{
	// Token: 0x02000920 RID: 2336
	[RequireComponent(typeof(Text))]
	public class FPSDisplayer : MonoBehaviour
	{
		// Token: 0x06003B69 RID: 15209 RVA: 0x0002AF75 File Offset: 0x00029175
		private void Start()
		{
			this.m_FpsNextPeriod = Time.realtimeSinceStartup + 0.5f;
			this.m_Text = base.GetComponent<Text>();
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x001AE284 File Offset: 0x001AC484
		private void Update()
		{
			this.m_FpsAccumulator++;
			if (Time.realtimeSinceStartup > this.m_FpsNextPeriod)
			{
				this.m_CurrentFps = (int)((float)this.m_FpsAccumulator / 0.5f);
				this.m_FpsAccumulator = 0;
				this.m_FpsNextPeriod += 0.5f;
				this.m_Text.text = string.Format("{0} FPS", this.m_CurrentFps);
			}
		}

		// Token: 0x04003627 RID: 13863
		private const float fpsMeasurePeriod = 0.5f;

		// Token: 0x04003628 RID: 13864
		private int m_FpsAccumulator;

		// Token: 0x04003629 RID: 13865
		private float m_FpsNextPeriod;

		// Token: 0x0400362A RID: 13866
		private int m_CurrentFps;

		// Token: 0x0400362B RID: 13867
		private const string display = "{0} FPS";

		// Token: 0x0400362C RID: 13868
		private Text m_Text;
	}
}
