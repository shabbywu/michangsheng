using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001273 RID: 4723
	[CommandInfo("iTween", "Rotate To", "Rotates a game object to the specified angles over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class RotateTo : iTweenCommand
	{
		// Token: 0x06007293 RID: 29331 RVA: 0x002A8754 File Offset: 0x002A6954
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

		// Token: 0x06007294 RID: 29332 RVA: 0x0004E0A5 File Offset: 0x0004C2A5
		public override bool HasReference(Variable variable)
		{
			return this._toTransform.transformRef == variable || this._toRotation.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x06007295 RID: 29333 RVA: 0x002A8860 File Offset: 0x002A6A60
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

		// Token: 0x040064BD RID: 25789
		[Tooltip("Target transform that the GameObject will rotate to")]
		[SerializeField]
		protected TransformData _toTransform;

		// Token: 0x040064BE RID: 25790
		[Tooltip("Target rotation that the GameObject will rotate to, if no To Transform is set")]
		[SerializeField]
		protected Vector3Data _toRotation;

		// Token: 0x040064BF RID: 25791
		[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
		[SerializeField]
		protected bool isLocal;

		// Token: 0x040064C0 RID: 25792
		[HideInInspector]
		[FormerlySerializedAs("toTransform")]
		public Transform toTransformOLD;

		// Token: 0x040064C1 RID: 25793
		[HideInInspector]
		[FormerlySerializedAs("toRotation")]
		public Vector3 toRotationOLD;
	}
}
