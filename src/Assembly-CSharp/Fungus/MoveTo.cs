using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E08 RID: 3592
	[CommandInfo("iTween", "Move To", "Moves a game object to a specified position over time. The position can be defined by a transform in another object (using To Transform) or by setting an absolute position (using To Position, if To Transform is set to None).", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class MoveTo : iTweenCommand
	{
		// Token: 0x06006571 RID: 25969 RVA: 0x00283284 File Offset: 0x00281484
		public override void DoTween()
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("name", this._tweenName.Value);
			if (this._toTransform.Value == null)
			{
				hashtable.Add("position", this._toPosition.Value);
			}
			else
			{
				hashtable.Add("position", this._toTransform.Value);
			}
			hashtable.Add("time", this._duration.Value);
			hashtable.Add("easetype", this.easeType);
			hashtable.Add("looptype", this.loopType);
			hashtable.Add("isLocal", this.isLocal);
			hashtable.Add("oncomplete", "OniTweenComplete");
			hashtable.Add("oncompletetarget", base.gameObject);
			hashtable.Add("oncompleteparams", this);
			iTween.MoveTo(this._targetObject.Value, hashtable);
		}

		// Token: 0x06006572 RID: 25970 RVA: 0x0028338E File Offset: 0x0028158E
		public override bool HasReference(Variable variable)
		{
			return this._toTransform.transformRef == variable || this._toPosition.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x06006573 RID: 25971 RVA: 0x002833C0 File Offset: 0x002815C0
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.toTransformOLD != null)
			{
				this._toTransform.Value = this.toTransformOLD;
				this.toTransformOLD = null;
			}
			if (this.toPositionOLD != default(Vector3))
			{
				this._toPosition.Value = this.toPositionOLD;
				this.toPositionOLD = default(Vector3);
			}
		}

		// Token: 0x04005727 RID: 22311
		[Tooltip("Target transform that the GameObject will move to")]
		[SerializeField]
		protected TransformData _toTransform;

		// Token: 0x04005728 RID: 22312
		[Tooltip("Target world position that the GameObject will move to, if no From Transform is set")]
		[SerializeField]
		protected Vector3Data _toPosition;

		// Token: 0x04005729 RID: 22313
		[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
		[SerializeField]
		protected bool isLocal;

		// Token: 0x0400572A RID: 22314
		[HideInInspector]
		[FormerlySerializedAs("toTransform")]
		public Transform toTransformOLD;

		// Token: 0x0400572B RID: 22315
		[HideInInspector]
		[FormerlySerializedAs("toPosition")]
		public Vector3 toPositionOLD;
	}
}
