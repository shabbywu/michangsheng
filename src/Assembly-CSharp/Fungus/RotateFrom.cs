using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E23 RID: 3619
	[CommandInfo("iTween", "Rotate From", "Rotates a game object from the specified angles back to its starting orientation over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class RotateFrom : iTweenCommand
	{
		// Token: 0x06006601 RID: 26113 RVA: 0x00284B3C File Offset: 0x00282D3C
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

		// Token: 0x06006602 RID: 26114 RVA: 0x00284C46 File Offset: 0x00282E46
		public override bool HasReference(Variable variable)
		{
			return this._fromTransform.transformRef == variable || this._fromRotation.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x06006603 RID: 26115 RVA: 0x00284C78 File Offset: 0x00282E78
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

		// Token: 0x0400577B RID: 22395
		[Tooltip("Target transform that the GameObject will rotate from")]
		[SerializeField]
		protected TransformData _fromTransform;

		// Token: 0x0400577C RID: 22396
		[Tooltip("Target rotation that the GameObject will rotate from, if no From Transform is set")]
		[SerializeField]
		protected Vector3Data _fromRotation;

		// Token: 0x0400577D RID: 22397
		[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
		[SerializeField]
		protected bool isLocal;

		// Token: 0x0400577E RID: 22398
		[HideInInspector]
		[FormerlySerializedAs("fromTransform")]
		public Transform fromTransformOLD;

		// Token: 0x0400577F RID: 22399
		[HideInInspector]
		[FormerlySerializedAs("fromRotation")]
		public Vector3 fromRotationOLD;
	}
}
