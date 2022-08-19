using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005F6 RID: 1526
	[Serializable]
	public class RayImpact
	{
		// Token: 0x06003107 RID: 12551 RVA: 0x0015DD32 File Offset: 0x0015BF32
		public float GetDamageAtDistance(float distance, float maxDistance)
		{
			return this.ApplyCurveToValue(this.m_MaxDamage, distance, maxDistance);
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x0015DD42 File Offset: 0x0015BF42
		public float GetImpulseAtDistance(float distance, float maxDistance)
		{
			return this.ApplyCurveToValue(this.m_MaxImpulse, distance, maxDistance);
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x0015DD54 File Offset: 0x0015BF54
		private float ApplyCurveToValue(float value, float distance, float maxDistance)
		{
			float num = Mathf.Abs(maxDistance);
			float num2 = Mathf.Clamp(distance, 0f, num);
			return value * this.m_DistanceCurve.Evaluate(num2 / num);
		}

		// Token: 0x04002B40 RID: 11072
		[Range(0f, 1000f)]
		[SerializeField]
		[Tooltip("The damage at close range.")]
		private float m_MaxDamage = 15f;

		// Token: 0x04002B41 RID: 11073
		[Range(0f, 1000f)]
		[SerializeField]
		[Tooltip("The impact impulse that will be transfered to the rigidbodies at contact.")]
		private float m_MaxImpulse = 15f;

		// Token: 0x04002B42 RID: 11074
		[SerializeField]
		[Tooltip("How damage and impulse lowers over distance.")]
		private AnimationCurve m_DistanceCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(0.8f, 0.5f),
			new Keyframe(1f, 0f)
		});
	}
}
