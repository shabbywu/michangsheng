using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DE7 RID: 3559
	[CommandInfo("LeanTween", "Scale", "Changes a game object's scale to a specified value over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class ScaleLean : BaseLeanTweenCommand
	{
		// Token: 0x060064E6 RID: 25830 RVA: 0x002811F4 File Offset: 0x0027F3F4
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

		// Token: 0x060064E7 RID: 25831 RVA: 0x002812A6 File Offset: 0x0027F4A6
		public override bool HasReference(Variable variable)
		{
			return variable == this._toTransform.transformRef || this._toScale.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x040056D2 RID: 22226
		[Tooltip("Target transform that the GameObject will scale to")]
		[SerializeField]
		protected TransformData _toTransform;

		// Token: 0x040056D3 RID: 22227
		[Tooltip("Target scale that the GameObject will scale to, if no To Transform is set")]
		[SerializeField]
		protected Vector3Data _toScale = new Vector3Data(Vector3.one);
	}
}
