using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001229 RID: 4649
	[CommandInfo("LeanTween", "Rotate", "Rotates a game object to the specified angles over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class RotateLean : BaseLeanTweenCommand
	{
		// Token: 0x0600716F RID: 29039 RVA: 0x002A594C File Offset: 0x002A3B4C
		public override LTDescr ExecuteTween()
		{
			Vector3 vector = (this._toTransform.Value == null) ? this._toRotation.Value : this._toTransform.Value.rotation.eulerAngles;
			if (this.rotateMode == RotateLean.RotateMode.LookAt3D)
			{
				vector = Quaternion.LookRotation((((this._toTransform.Value == null) ? this._toRotation.Value : this._toTransform.Value.position) - this._targetObject.Value.transform.position).normalized).eulerAngles;
			}
			else if (this.rotateMode == RotateLean.RotateMode.LookAt2D)
			{
				Vector3 vector2 = ((this._toTransform.Value == null) ? this._toRotation.Value : this._toTransform.Value.position) - this._targetObject.Value.transform.position;
				vector2.z = 0f;
				vector = Quaternion.FromToRotation(this._targetObject.Value.transform.up, vector2.normalized).eulerAngles;
			}
			if (base.IsInAddativeMode)
			{
				vector += this._targetObject.Value.transform.rotation.eulerAngles;
			}
			if (base.IsInFromMode)
			{
				Vector3 eulerAngles = this._targetObject.Value.transform.rotation.eulerAngles;
				this._targetObject.Value.transform.rotation = Quaternion.Euler(vector);
				vector = eulerAngles;
			}
			if (this.isLocal)
			{
				return LeanTween.rotateLocal(this._targetObject.Value, vector, this._duration);
			}
			return LeanTween.rotate(this._targetObject.Value, vector, this._duration);
		}

		// Token: 0x06007170 RID: 29040 RVA: 0x0004D16C File Offset: 0x0004B36C
		public override bool HasReference(Variable variable)
		{
			return variable == this._toTransform.transformRef || this._toRotation.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x040063DB RID: 25563
		[Tooltip("Target transform that the GameObject will rotate to")]
		[SerializeField]
		protected TransformData _toTransform;

		// Token: 0x040063DC RID: 25564
		[Tooltip("Target rotation that the GameObject will rotate to, if no To Transform is set")]
		[SerializeField]
		protected Vector3Data _toRotation;

		// Token: 0x040063DD RID: 25565
		[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
		[SerializeField]
		protected bool isLocal;

		// Token: 0x040063DE RID: 25566
		[Tooltip("Whether to use the provided Transform or Vector as a target to look at rather than a euler to match.")]
		[SerializeField]
		protected RotateLean.RotateMode rotateMode;

		// Token: 0x0200122A RID: 4650
		public enum RotateMode
		{
			// Token: 0x040063E0 RID: 25568
			PureRotate,
			// Token: 0x040063E1 RID: 25569
			LookAt2D,
			// Token: 0x040063E2 RID: 25570
			LookAt3D
		}
	}
}
