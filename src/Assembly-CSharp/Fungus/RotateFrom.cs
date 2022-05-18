using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001272 RID: 4722
	[CommandInfo("iTween", "Rotate From", "Rotates a game object from the specified angles back to its starting orientation over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class RotateFrom : iTweenCommand
	{
		// Token: 0x0600728F RID: 29327 RVA: 0x002A85DC File Offset: 0x002A67DC
		public override void DoTween()
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("name", this._tweenName.Value);
			if (this._fromTransform.Value == null)
			{
				hashtable.Add("rotation", this._fromRotation.Value);
			}
			else
			{
				hashtable.Add("rotation", this._fromTransform.Value);
			}
			hashtable.Add("time", this._duration.Value);
			hashtable.Add("easetype", this.easeType);
			hashtable.Add("looptype", this.loopType);
			hashtable.Add("isLocal", this.isLocal);
			hashtable.Add("oncomplete", "OniTweenComplete");
			hashtable.Add("oncompletetarget", base.gameObject);
			hashtable.Add("oncompleteparams", this);
			iTween.RotateFrom(this._targetObject.Value, hashtable);
		}

		// Token: 0x06007290 RID: 29328 RVA: 0x0004E074 File Offset: 0x0004C274
		public override bool HasReference(Variable variable)
		{
			return this._fromTransform.transformRef == variable || this._fromRotation.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x06007291 RID: 29329 RVA: 0x002A86E8 File Offset: 0x002A68E8
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.fromTransformOLD != null)
			{
				this._fromTransform.Value = this.fromTransformOLD;
				this.fromTransformOLD = null;
			}
			if (this.fromRotationOLD != default(Vector3))
			{
				this._fromRotation.Value = this.fromRotationOLD;
				this.fromRotationOLD = default(Vector3);
			}
		}

		// Token: 0x040064B8 RID: 25784
		[Tooltip("Target transform that the GameObject will rotate from")]
		[SerializeField]
		protected TransformData _fromTransform;

		// Token: 0x040064B9 RID: 25785
		[Tooltip("Target rotation that the GameObject will rotate from, if no From Transform is set")]
		[SerializeField]
		protected Vector3Data _fromRotation;

		// Token: 0x040064BA RID: 25786
		[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
		[SerializeField]
		protected bool isLocal;

		// Token: 0x040064BB RID: 25787
		[HideInInspector]
		[FormerlySerializedAs("fromTransform")]
		public Transform fromTransformOLD;

		// Token: 0x040064BC RID: 25788
		[HideInInspector]
		[FormerlySerializedAs("fromRotation")]
		public Vector3 fromRotationOLD;
	}
}
