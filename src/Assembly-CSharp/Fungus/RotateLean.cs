using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DE6 RID: 3558
	[CommandInfo("LeanTween", "Rotate", "Rotates a game object to the specified angles over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class RotateLean : BaseLeanTweenCommand
	{
		// Token: 0x060064E3 RID: 25827 RVA: 0x00280FD4 File Offset: 0x0027F1D4
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

		// Token: 0x060064E4 RID: 25828 RVA: 0x002811C3 File Offset: 0x0027F3C3
		public override bool HasReference(Variable variable)
		{
			return variable == this._toTransform.transformRef || this._toRotation.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x040056CE RID: 22222
		[Tooltip("Target transform that the GameObject will rotate to")]
		[SerializeField]
		protected TransformData _toTransform;

		// Token: 0x040056CF RID: 22223
		[Tooltip("Target rotation that the GameObject will rotate to, if no To Transform is set")]
		[SerializeField]
		protected Vector3Data _toRotation;

		// Token: 0x040056D0 RID: 22224
		[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
		[SerializeField]
		protected bool isLocal;

		// Token: 0x040056D1 RID: 22225
		[Tooltip("Whether to use the provided Transform or Vector as a target to look at rather than a euler to match.")]
		[SerializeField]
		protected RotateLean.RotateMode rotateMode;

		// Token: 0x020016B7 RID: 5815
		public enum RotateMode
		{
			// Token: 0x04007369 RID: 29545
			PureRotate,
			// Token: 0x0400736A RID: 29546
			LookAt2D,
			// Token: 0x0400736B RID: 29547
			LookAt3D
		}
	}
}
