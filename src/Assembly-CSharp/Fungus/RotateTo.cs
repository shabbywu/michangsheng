using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E24 RID: 3620
	[CommandInfo("iTween", "Rotate To", "Rotates a game object to the specified angles over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class RotateTo : iTweenCommand
	{
		// Token: 0x06006605 RID: 26117 RVA: 0x00284CE4 File Offset: 0x00282EE4
		public override void DoTween()
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("name", this._tweenName.Value);
			if (this._toTransform.Value == null)
			{
				hashtable.Add("rotation", this._toRotation.Value);
			}
			else
			{
				hashtable.Add("rotation", this._toTransform.Value);
			}
			hashtable.Add("time", this._duration.Value);
			hashtable.Add("easetype", this.easeType);
			hashtable.Add("looptype", this.loopType);
			hashtable.Add("isLocal", this.isLocal);
			hashtable.Add("oncomplete", "OniTweenComplete");
			hashtable.Add("oncompletetarget", base.gameObject);
			hashtable.Add("oncompleteparams", this);
			iTween.RotateTo(this._targetObject.Value, hashtable);
		}

		// Token: 0x06006606 RID: 26118 RVA: 0x00284DEE File Offset: 0x00282FEE
		public override bool HasReference(Variable variable)
		{
			return this._toTransform.transformRef == variable || this._toRotation.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x06006607 RID: 26119 RVA: 0x00284E20 File Offset: 0x00283020
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.toTransformOLD != null)
			{
				this._toTransform.Value = this.toTransformOLD;
				this.toTransformOLD = null;
			}
			if (this.toRotationOLD != default(Vector3))
			{
				this._toRotation.Value = this.toRotationOLD;
				this.toRotationOLD = default(Vector3);
			}
		}

		// Token: 0x04005780 RID: 22400
		[Tooltip("Target transform that the GameObject will rotate to")]
		[SerializeField]
		protected TransformData _toTransform;

		// Token: 0x04005781 RID: 22401
		[Tooltip("Target rotation that the GameObject will rotate to, if no To Transform is set")]
		[SerializeField]
		protected Vector3Data _toRotation;

		// Token: 0x04005782 RID: 22402
		[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
		[SerializeField]
		protected bool isLocal;

		// Token: 0x04005783 RID: 22403
		[HideInInspector]
		[FormerlySerializedAs("toTransform")]
		public Transform toTransformOLD;

		// Token: 0x04005784 RID: 22404
		[HideInInspector]
		[FormerlySerializedAs("toRotation")]
		public Vector3 toRotationOLD;
	}
}
