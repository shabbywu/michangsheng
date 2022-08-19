using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E07 RID: 3591
	[CommandInfo("iTween", "Move From", "Moves a game object from a specified position back to its starting position over time. The position can be defined by a transform in another object (using To Transform) or by setting an absolute position (using To Position, if To Transform is set to None).", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class MoveFrom : iTweenCommand
	{
		// Token: 0x0600656D RID: 25965 RVA: 0x002830DC File Offset: 0x002812DC
		public override void DoTween()
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("name", this._tweenName.Value);
			if (this._fromTransform.Value == null)
			{
				hashtable.Add("position", this._fromPosition.Value);
			}
			else
			{
				hashtable.Add("position", this._fromTransform.Value);
			}
			hashtable.Add("time", this._duration.Value);
			hashtable.Add("easetype", this.easeType);
			hashtable.Add("looptype", this.loopType);
			hashtable.Add("isLocal", this.isLocal);
			hashtable.Add("oncomplete", "OniTweenComplete");
			hashtable.Add("oncompletetarget", base.gameObject);
			hashtable.Add("oncompleteparams", this);
			iTween.MoveFrom(this._targetObject.Value, hashtable);
		}

		// Token: 0x0600656E RID: 25966 RVA: 0x002831E6 File Offset: 0x002813E6
		public override bool HasReference(Variable variable)
		{
			return this._fromTransform.transformRef == variable || this._fromPosition.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x0600656F RID: 25967 RVA: 0x00283218 File Offset: 0x00281418
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.fromTransformOLD != null)
			{
				this._fromTransform.Value = this.fromTransformOLD;
				this.fromTransformOLD = null;
			}
			if (this.fromPositionOLD != default(Vector3))
			{
				this._fromPosition.Value = this.fromPositionOLD;
				this.fromPositionOLD = default(Vector3);
			}
		}

		// Token: 0x04005722 RID: 22306
		[Tooltip("Target transform that the GameObject will move from")]
		[SerializeField]
		protected TransformData _fromTransform;

		// Token: 0x04005723 RID: 22307
		[Tooltip("Target world position that the GameObject will move from, if no From Transform is set")]
		[SerializeField]
		protected Vector3Data _fromPosition;

		// Token: 0x04005724 RID: 22308
		[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
		[SerializeField]
		protected bool isLocal;

		// Token: 0x04005725 RID: 22309
		[HideInInspector]
		[FormerlySerializedAs("fromTransform")]
		public Transform fromTransformOLD;

		// Token: 0x04005726 RID: 22310
		[HideInInspector]
		[FormerlySerializedAs("fromPosition")]
		public Vector3 fromPositionOLD;
	}
}
