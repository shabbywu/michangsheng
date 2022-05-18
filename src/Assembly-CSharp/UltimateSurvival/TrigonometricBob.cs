using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200090A RID: 2314
	[Serializable]
	public class TrigonometricBob
	{
		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06003B29 RID: 15145 RVA: 0x0002ADE7 File Offset: 0x00028FE7
		public float Time
		{
			get
			{
				return this.m_Time;
			}
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x0002ADEF File Offset: 0x00028FEF
		public TrigonometricBob GetClone()
		{
			return (TrigonometricBob)base.MemberwiseClone();
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x001AB698 File Offset: 0x001A9898
		public Vector3 CalculateBob(float moveSpeed, float deltaTime)
		{
			if (!this.m_Enabled)
			{
				return Vector3.zero;
			}
			float num = Mathf.Sin(1f * this.m_Time * 3.1415927f * 2f);
			float num2 = 1f - Mathf.Cos(2f * this.m_Time * 3.1415927f * 2f);
			float num3 = this.m_Time + this.m_Speed * moveSpeed * deltaTime * this.m_Curve.Evaluate(this.m_Time);
			this.m_Time = Mathf.Repeat(num3, 1f);
			this.m_Vector.x = num * this.m_AmountX;
			this.m_Vector.y = num2 * this.m_AmountY;
			return this.m_Vector;
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x001AB758 File Offset: 0x001A9958
		public Vector3 Cooldown(float deltaTime)
		{
			if (!this.m_Enabled)
			{
				return Vector3.zero;
			}
			this.m_Time = Mathf.Lerp(this.m_Time, this.GetBestCooldownValue(this.m_Time), deltaTime * this.m_CooldownSpeed);
			float num = Mathf.Sin(1f * this.m_Time * 3.1415927f * 2f);
			float num2 = 1f - Mathf.Cos(2f * this.m_Time * 3.1415927f * 2f);
			this.m_Vector.x = num * this.m_AmountX;
			this.m_Vector.y = num2 * this.m_AmountY;
			return this.m_Vector;
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x001AB808 File Offset: 0x001A9A08
		private float GetBestCooldownValue(float time)
		{
			float result = 0f;
			if (this.m_Time > 0.25f && this.m_Time < 0.5f)
			{
				result = 0.5f;
			}
			else if (this.m_Time > 0.5f && this.m_Time < 0.75f)
			{
				result = 0.5f;
			}
			else if (this.m_Time > 0.75f)
			{
				result = 1f;
			}
			return result;
		}

		// Token: 0x04003569 RID: 13673
		private const float HORIZONTAL_SPEED = 1f;

		// Token: 0x0400356A RID: 13674
		private const float VERTICAL_SPEED = 2f;

		// Token: 0x0400356B RID: 13675
		[SerializeField]
		private bool m_Enabled = true;

		// Token: 0x0400356C RID: 13676
		[SerializeField]
		[Range(0.1f, 20f)]
		[Tooltip("How fast is the animation overall.")]
		private float m_Speed = 0.18f;

		// Token: 0x0400356D RID: 13677
		[SerializeField]
		[Range(0.1f, 20f)]
		[Tooltip("How fast it blends out, when it's no longer used (so the transition between walk and run bobs are smooth for example).")]
		private float m_CooldownSpeed = 5f;

		// Token: 0x0400356E RID: 13678
		[SerializeField]
		[Range(0f, 100f)]
		private float m_AmountX = 0.2f;

		// Token: 0x0400356F RID: 13679
		[SerializeField]
		[Range(0f, 100f)]
		private float m_AmountY = 0.2f;

		// Token: 0x04003570 RID: 13680
		[SerializeField]
		[Tooltip("You can control how fast the animation plays at different time intervals (time = 0 is the beginning, time = 1 is the end).")]
		private AnimationCurve m_Curve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04003571 RID: 13681
		private float m_Time;

		// Token: 0x04003572 RID: 13682
		private Vector3 m_Vector;
	}
}
