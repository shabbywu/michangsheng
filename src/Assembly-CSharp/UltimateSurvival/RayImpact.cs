using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008CD RID: 2253
	[Serializable]
	public class RayImpact
	{
		// Token: 0x060039F1 RID: 14833 RVA: 0x0002A239 File Offset: 0x00028439
		public float GetDamageAtDistance(float distance, float maxDistance)
		{
			return this.ApplyCurveToValue(this.m_MaxDamage, distance, maxDistance);
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x0002A249 File Offset: 0x00028449
		public float GetImpulseAtDistance(float distance, float maxDistance)
		{
			return this.ApplyCurveToValue(this.m_MaxImpulse, distance, maxDistance);
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x001A72EC File Offset: 0x001A54EC
		private float ApplyCurveToValue(float value, float distance, float maxDistance)
		{
			float num = Mathf.Abs(maxDistance);
			float num2 = Mathf.Clamp(distance, 0f, num);
			return value * this.m_DistanceCurve.Evaluate(num2 / num);
		}

		// Token: 0x04003423 RID: 13347
		[Range(0f, 1000f)]
		[SerializeField]
		[Tooltip("The damage at close range.")]
		private float m_MaxDamage = 15f;

		// Token: 0x04003424 RID: 13348
		[Range(0f, 1000f)]
		[SerializeField]
		[Tooltip("The impact impulse that will be transfered to the rigidbodies at contact.")]
		private float m_MaxImpulse = 15f;

		// Token: 0x04003425 RID: 13349
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
