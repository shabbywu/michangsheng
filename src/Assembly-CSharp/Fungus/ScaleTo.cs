using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E2A RID: 3626
	[CommandInfo("iTween", "Scale To", "Changes a game object's scale to a specified value over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class ScaleTo : iTweenCommand
	{
		// Token: 0x06006635 RID: 26165 RVA: 0x00285A8C File Offset: 0x00283C8C
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

		// Token: 0x06006636 RID: 26166 RVA: 0x00285B80 File Offset: 0x00283D80
		public override bool HasReference(Variable variable)
		{
			return this._toTransform.transformRef == variable || this._toScale.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x06006637 RID: 26167 RVA: 0x00285BB4 File Offset: 0x00283DB4
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

		// Token: 0x040057A6 RID: 22438
		[Tooltip("Target transform that the GameObject will scale to")]
		[SerializeField]
		protected TransformData _toTransform;

		// Token: 0x040057A7 RID: 22439
		[Tooltip("Target scale that the GameObject will scale to, if no To Transform is set")]
		[SerializeField]
		protected Vector3Data _toScale = new Vector3Data(Vector3.one);

		// Token: 0x040057A8 RID: 22440
		[HideInInspector]
		[FormerlySerializedAs("toTransform")]
		public Transform toTransformOLD;

		// Token: 0x040057A9 RID: 22441
		[HideInInspector]
		[FormerlySerializedAs("toScale")]
		public Vector3 toScaleOLD;
	}
}
