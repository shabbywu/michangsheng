using System;
using UnityEngine;

// Token: 0x020007B1 RID: 1969
public class AlphaPulse
{
	// Token: 0x0600320A RID: 12810 RVA: 0x0002482B File Offset: 0x00022A2B
	public AlphaPulse(Color tP, float min, float max)
	{
		this.m_ToPulse = tP;
		this.m_PulseMin = min;
		this.m_PulseMax = max;
	}

	// Token: 0x0600320B RID: 12811 RVA: 0x0018D2DC File Offset: 0x0018B4DC
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

	// Token: 0x0600320C RID: 12812 RVA: 0x0018D34C File Offset: 0x0018B54C
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

	// Token: 0x0600320D RID: 12813 RVA: 0x00024848 File Offset: 0x00022A48
	private void StopPulse()
	{
		this.m_StartTime = 0f;
		this.m_IsLerping = false;
	}

	// Token: 0x04002E31 RID: 11825
	private bool m_IsLerping;

	// Token: 0x04002E32 RID: 11826
	private bool m_PulsingAtMax;

	// Token: 0x04002E33 RID: 11827
	private float m_PulseMin;

	// Token: 0x04002E34 RID: 11828
	private float m_PulseMax;

	// Token: 0x04002E35 RID: 11829
	private float m_LerpDuration;

	// Token: 0x04002E36 RID: 11830
	private float m_StartTime;

	// Token: 0x04002E37 RID: 11831
	private Color m_ToPulse;
}
