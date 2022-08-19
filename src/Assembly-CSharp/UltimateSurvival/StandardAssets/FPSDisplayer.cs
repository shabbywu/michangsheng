using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.StandardAssets
{
	// Token: 0x0200062E RID: 1582
	[RequireComponent(typeof(Text))]
	public class FPSDisplayer : MonoBehaviour
	{
		// Token: 0x0600322F RID: 12847 RVA: 0x00164A0F File Offset: 0x00162C0F
		private void Start()
		{
			this.m_FpsNextPeriod = Time.realtimeSinceStartup + 0.5f;
			this.m_Text = base.GetComponent<Text>();
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x00164A30 File Offset: 0x00162C30
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

		// Token: 0x04002CD6 RID: 11478
		private const float fpsMeasurePeriod = 0.5f;

		// Token: 0x04002CD7 RID: 11479
		private int m_FpsAccumulator;

		// Token: 0x04002CD8 RID: 11480
		private float m_FpsNextPeriod;

		// Token: 0x04002CD9 RID: 11481
		private int m_CurrentFps;

		// Token: 0x04002CDA RID: 11482
		private const string display = "{0} FPS";

		// Token: 0x04002CDB RID: 11483
		private Text m_Text;
	}
}
