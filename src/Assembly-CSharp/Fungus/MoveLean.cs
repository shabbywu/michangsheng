using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001228 RID: 4648
	[CommandInfo("LeanTween", "Move", "Moves a game object to a specified position over time. The position can be defined by a transform in another object (using To Transform) or by setting an absolute position (using To Position, if To Transform is set to None).", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class MoveLean : BaseLeanTweenCommand
	{
		// Token: 0x0600716C RID: 29036 RVA: 0x002A5874 File Offset: 0x002A3A74
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

		// Token: 0x0600716D RID: 29037 RVA: 0x0004D133 File Offset: 0x0004B333
		public override bool HasReference(Variable variable)
		{
			return this._toTransform.transformRef == variable || this._toPosition.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x040063D8 RID: 25560
		[Tooltip("Target transform that the GameObject will move to")]
		[SerializeField]
		protected TransformData _toTransform;

		// Token: 0x040063D9 RID: 25561
		[Tooltip("Target world position that the GameObject will move to, if no From Transform is set")]
		[SerializeField]
		protected Vector3Data _toPosition;

		// Token: 0x040063DA RID: 25562
		[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
		[SerializeField]
		protected bool isLocal;
	}
}
