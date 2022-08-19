using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DE5 RID: 3557
	[CommandInfo("LeanTween", "Move", "Moves a game object to a specified position over time. The position can be defined by a transform in another object (using To Transform) or by setting an absolute position (using To Position, if To Transform is set to None).", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class MoveLean : BaseLeanTweenCommand
	{
		// Token: 0x060064E0 RID: 25824 RVA: 0x00280EC4 File Offset: 0x0027F0C4
		public override LTDescr ExecuteTween()
		{
			Vector3 vector = (this._toTransform.Value == null) ? this._toPosition.Value : this._toTransform.Value.position;
			if (base.IsInAddativeMode)
			{
				vector += this._targetObject.Value.transform.position;
			}
			if (base.IsInFromMode)
			{
				Vector3 position = this._targetObject.Value.transform.position;
				this._targetObject.Value.transform.position = vector;
				vector = position;
			}
			if (this.isLocal)
			{
				return LeanTween.moveLocal(this._targetObject.Value, vector, this._duration);
			}
			return LeanTween.move(this._targetObject.Value, vector, this._duration);
		}

		// Token: 0x060064E1 RID: 25825 RVA: 0x00280F9B File Offset: 0x0027F19B
		public override bool HasReference(Variable variable)
		{
			return this._toTransform.transformRef == variable || this._toPosition.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x040056CB RID: 22219
		[Tooltip("Target transform that the GameObject will move to")]
		[SerializeField]
		protected TransformData _toTransform;

		// Token: 0x040056CC RID: 22220
		[Tooltip("Target world position that the GameObject will move to, if no From Transform is set")]
		[SerializeField]
		protected Vector3Data _toPosition;

		// Token: 0x040056CD RID: 22221
		[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
		[SerializeField]
		protected bool isLocal;
	}
}
