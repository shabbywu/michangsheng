using System;
using UnityEngine;

// Token: 0x0200051D RID: 1309
public class AlphaPulse
{
	// Token: 0x060029F7 RID: 10743 RVA: 0x001400E3 File Offset: 0x0013E2E3
	public AlphaPulse(Color tP, float min, float max)
	{
		this.m_ToPulse = tP;
		this.m_PulseMin = min;
		this.m_PulseMax = max;
	}

	// Token: 0x060029F8 RID: 10744 RVA: 0x00140100 File Offset: 0x0013E300
	public void StartPulse(float lerpDuration)
	{
		if (this.m_StartTime == 0f && !this.m_IsLerping)
		{
			this.m_LerpDuration = lerpDuration;
			this.m_StartTime = Time.time;
			this.m_IsLerping = true;
			if (this.m_ToPulse.a == this.m_PulseMax)
			{
				this.m_PulsingAtMax = false;
				return;
			}
			if (this.m_ToPulse.a == this.m_PulseMin)
			{
				this.m_PulsingAtMax = true;
			}
		}
	}

	// Token: 0x060029F9 RID: 10745 RVA: 0x00140170 File Offset: 0x0013E370
	public float UpdatePulse()
	{
		if (!this.m_IsLerping)
		{
			return 0f;
		}
		float num = (Time.time - this.m_StartTime) / this.m_LerpDuration;
		float num2 = this.m_PulsingAtMax ? this.m_PulseMax : this.m_PulseMin;
		this.m_ToPulse.a = Mathf.Lerp(this.m_ToPulse.a, num2, num);
		if (this.m_ToPulse.a == num2)
		{
			this.StopPulse();
		}
		return this.m_ToPulse.a;
	}

	// Token: 0x060029FA RID: 10746 RVA: 0x001401F2 File Offset: 0x0013E3F2
	private void StopPulse()
	{
		this.m_StartTime = 0f;
		this.m_IsLerping = false;
	}

	// Token: 0x04002641 RID: 9793
	private bool m_IsLerping;

	// Token: 0x04002642 RID: 9794
	private bool m_PulsingAtMax;

	// Token: 0x04002643 RID: 9795
	private float m_PulseMin;

	// Token: 0x04002644 RID: 9796
	private float m_PulseMax;

	// Token: 0x04002645 RID: 9797
	private float m_LerpDuration;

	// Token: 0x04002646 RID: 9798
	private float m_StartTime;

	// Token: 0x04002647 RID: 9799
	private Color m_ToPulse;
}
