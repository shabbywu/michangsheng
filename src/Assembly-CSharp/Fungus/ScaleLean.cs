using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200122B RID: 4651
	[CommandInfo("LeanTween", "Scale", "Changes a game object's scale to a specified value over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class ScaleLean : BaseLeanTweenCommand
	{
		// Token: 0x06007172 RID: 29042 RVA: 0x002A5B3C File Offset: 0x002A3D3C
		public override LTDescr ExecuteTween()
		{
			Vector3 vector = (this._toTransform.Value == null) ? this._toScale.Value : this._toTransform.Value.localScale;
			if (base.IsInAddativeMode)
			{
				vector += this._targetObject.Value.transform.localScale;
			}
			if (base.IsInFromMode)
			{
				Vector3 localScale = this._targetObject.Value.transform.localScale;
				this._targetObject.Value.transform.localScale = vector;
				vector = localScale;
			}
			return LeanTween.scale(this._targetObject.Value, vector, this._duration);
		}

		// Token: 0x06007173 RID: 29043 RVA: 0x0004D19D File Offset: 0x0004B39D
		public override bool HasReference(Variable variable)
		{
			return variable == this._toTransform.transformRef || this._toScale.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x040063E3 RID: 25571
		[Tooltip("Target transform that the GameObject will scale to")]
		[SerializeField]
		protected TransformData _toTransform;

		// Token: 0x040063E4 RID: 25572
		[Tooltip("Target scale that the GameObject will scale to, if no To Transform is set")]
		[SerializeField]
		protected Vector3Data _toScale = new Vector3Data(Vector3.one);
	}
}
