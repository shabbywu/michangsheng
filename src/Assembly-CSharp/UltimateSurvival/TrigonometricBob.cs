using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000623 RID: 1571
	[Serializable]
	public class TrigonometricBob
	{
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060031F5 RID: 12789 RVA: 0x00161E03 File Offset: 0x00160003
		public float Time
		{
			get
			{
				return this.m_Time;
			}
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x00161E0B File Offset: 0x0016000B
		public TrigonometricBob GetClone()
		{
			return (TrigonometricBob)base.MemberwiseClone();
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x00161E18 File Offset: 0x00160018
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

		// Token: 0x060031F8 RID: 12792 RVA: 0x00161ED8 File Offset: 0x001600D8
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

		// Token: 0x060031F9 RID: 12793 RVA: 0x00161F88 File Offset: 0x00160188
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

		// Token: 0x04002C42 RID: 11330
		private const float HORIZONTAL_SPEED = 1f;

		// Token: 0x04002C43 RID: 11331
		private const float VERTICAL_SPEED = 2f;

		// Token: 0x04002C44 RID: 11332
		[SerializeField]
		private bool m_Enabled = true;

		// Token: 0x04002C45 RID: 11333
		[SerializeField]
		[Range(0.1f, 20f)]
		[Tooltip("How fast is the animation overall.")]
		private float m_Speed = 0.18f;

		// Token: 0x04002C46 RID: 11334
		[SerializeField]
		[Range(0.1f, 20f)]
		[Tooltip("How fast it blends out, when it's no longer used (so the transition between walk and run bobs are smooth for example).")]
		private float m_CooldownSpeed = 5f;

		// Token: 0x04002C47 RID: 11335
		[SerializeField]
		[Range(0f, 100f)]
		private float m_AmountX = 0.2f;

		// Token: 0x04002C48 RID: 11336
		[SerializeField]
		[Range(0f, 100f)]
		private float m_AmountY = 0.2f;

		// Token: 0x04002C49 RID: 11337
		[SerializeField]
		[Tooltip("You can control how fast the animation plays at different time intervals (time = 0 is the beginning, time = 1 is the end).")]
		private AnimationCurve m_Curve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04002C4A RID: 11338
		private float m_Time;

		// Token: 0x04002C4B RID: 11339
		private Vector3 m_Vector;
	}
}
