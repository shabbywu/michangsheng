using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x0200127A RID: 4730
	[CommandInfo("iTween", "Scale From", "Changes a game object's scale to the specified value and back to its original scale over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class ScaleFrom : iTweenCommand
	{
		// Token: 0x060072BF RID: 29375 RVA: 0x002A91F4 File Offset: 0x002A73F4
		public override void DoTween()
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("name", this._tweenName.Value);
			if (this._fromTransform.Value == null)
			{
				hashtable.Add("scale", this._fromScale.Value);
			}
			else
			{
				hashtable.Add("scale", this._fromTransform.Value);
			}
			hashtable.Add("time", this._duration.Value);
			hashtable.Add("easetype", this.easeType);
			hashtable.Add("looptype", this.loopType);
			hashtable.Add("oncomplete", "OniTweenComplete");
			hashtable.Add("oncompletetarget", base.gameObject);
			hashtable.Add("oncompleteparams", this);
			iTween.ScaleFrom(this._targetObject.Value, hashtable);
		}

		// Token: 0x060072C0 RID: 29376 RVA: 0x0004E217 File Offset: 0x0004C417
		public override bool HasReference(Variable variable)
		{
			return this._fromTransform.transformRef == variable || this._fromScale.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x060072C1 RID: 29377 RVA: 0x002A92E8 File Offset: 0x002A74E8
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.fromTransformOLD != null)
			{
				this._fromTransform.Value = this.fromTransformOLD;
				this.fromTransformOLD = null;
			}
			if (this.fromScaleOLD != default(Vector3))
			{
				this._fromScale.Value = this.fromScaleOLD;
				this.fromScaleOLD = default(Vector3);
			}
		}

		// Token: 0x040064E6 RID: 25830
		[Tooltip("Target transform that the GameObject will scale from")]
		[SerializeField]
		protected TransformData _fromTransform;

		// Token: 0x040064E7 RID: 25831
		[Tooltip("Target scale that the GameObject will scale from, if no From Transform is set")]
		[SerializeField]
		protected Vector3Data _fromScale;

		// Token: 0x040064E8 RID: 25832
		[HideInInspector]
		[FormerlySerializedAs("fromTransform")]
		public Transform fromTransformOLD;

		// Token: 0x040064E9 RID: 25833
		[HideInInspector]
		[FormerlySerializedAs("fromScale")]
		public Vector3 fromScaleOLD;
	}
}
