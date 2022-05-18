using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x0200127B RID: 4731
	[CommandInfo("iTween", "Scale To", "Changes a game object's scale to a specified value over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class ScaleTo : iTweenCommand
	{
		// Token: 0x060072C3 RID: 29379 RVA: 0x002A9354 File Offset: 0x002A7554
		public override void DoTween()
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("name", this._tweenName.Value);
			if (this._toTransform.Value == null)
			{
				hashtable.Add("scale", this._toScale.Value);
			}
			else
			{
				hashtable.Add("scale", this._toTransform.Value);
			}
			hashtable.Add("time", this._duration.Value);
			hashtable.Add("easetype", this.easeType);
			hashtable.Add("looptype", this.loopType);
			hashtable.Add("oncomplete", "OniTweenComplete");
			hashtable.Add("oncompletetarget", base.gameObject);
			hashtable.Add("oncompleteparams", this);
			iTween.ScaleTo(this._targetObject.Value, hashtable);
		}

		// Token: 0x060072C4 RID: 29380 RVA: 0x0004E248 File Offset: 0x0004C448
		public override bool HasReference(Variable variable)
		{
			return this._toTransform.transformRef == variable || this._toScale.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x060072C5 RID: 29381 RVA: 0x002A9448 File Offset: 0x002A7648
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.toTransformOLD != null)
			{
				this._toTransform.Value = this.toTransformOLD;
				this.toTransformOLD = null;
			}
			if (this.toScaleOLD != default(Vector3))
			{
				this._toScale.Value = this.toScaleOLD;
				this.toScaleOLD = default(Vector3);
			}
		}

		// Token: 0x040064EA RID: 25834
		[Tooltip("Target transform that the GameObject will scale to")]
		[SerializeField]
		protected TransformData _toTransform;

		// Token: 0x040064EB RID: 25835
		[Tooltip("Target scale that the GameObject will scale to, if no To Transform is set")]
		[SerializeField]
		protected Vector3Data _toScale = new Vector3Data(Vector3.one);

		// Token: 0x040064EC RID: 25836
		[HideInInspector]
		[FormerlySerializedAs("toTransform")]
		public Transform toTransformOLD;

		// Token: 0x040064ED RID: 25837
		[HideInInspector]
		[FormerlySerializedAs("toScale")]
		public Vector3 toScaleOLD;
	}
}
