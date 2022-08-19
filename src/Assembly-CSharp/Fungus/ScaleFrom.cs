using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E29 RID: 3625
	[CommandInfo("iTween", "Scale From", "Changes a game object's scale to the specified value and back to its original scale over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class ScaleFrom : iTweenCommand
	{
		// Token: 0x06006631 RID: 26161 RVA: 0x002858F8 File Offset: 0x00283AF8
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

		// Token: 0x06006632 RID: 26162 RVA: 0x002859EC File Offset: 0x00283BEC
		public override bool HasReference(Variable variable)
		{
			return this._fromTransform.transformRef == variable || this._fromScale.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x06006633 RID: 26163 RVA: 0x00285A20 File Offset: 0x00283C20
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

		// Token: 0x040057A2 RID: 22434
		[Tooltip("Target transform that the GameObject will scale from")]
		[SerializeField]
		protected TransformData _fromTransform;

		// Token: 0x040057A3 RID: 22435
		[Tooltip("Target scale that the GameObject will scale from, if no From Transform is set")]
		[SerializeField]
		protected Vector3Data _fromScale;

		// Token: 0x040057A4 RID: 22436
		[HideInInspector]
		[FormerlySerializedAs("fromTransform")]
		public Transform fromTransformOLD;

		// Token: 0x040057A5 RID: 22437
		[HideInInspector]
		[FormerlySerializedAs("fromScale")]
		public Vector3 fromScaleOLD;
	}
}
