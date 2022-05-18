using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001255 RID: 4693
	[CommandInfo("iTween", "Move To", "Moves a game object to a specified position over time. The position can be defined by a transform in another object (using To Transform) or by setting an absolute position (using To Position, if To Transform is set to None).", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class MoveTo : iTweenCommand
	{
		// Token: 0x060071FF RID: 29183 RVA: 0x002A74F8 File Offset: 0x002A56F8
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

		// Token: 0x06007200 RID: 29184 RVA: 0x0004D8A4 File Offset: 0x0004BAA4
		public override bool HasReference(Variable variable)
		{
			return this._toTransform.transformRef == variable || this._toPosition.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x06007201 RID: 29185 RVA: 0x002A7604 File Offset: 0x002A5804
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

		// Token: 0x0400645C RID: 25692
		[Tooltip("Target transform that the GameObject will move to")]
		[SerializeField]
		protected TransformData _toTransform;

		// Token: 0x0400645D RID: 25693
		[Tooltip("Target world position that the GameObject will move to, if no From Transform is set")]
		[SerializeField]
		protected Vector3Data _toPosition;

		// Token: 0x0400645E RID: 25694
		[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
		[SerializeField]
		protected bool isLocal;

		// Token: 0x0400645F RID: 25695
		[HideInInspector]
		[FormerlySerializedAs("toTransform")]
		public Transform toTransformOLD;

		// Token: 0x04006460 RID: 25696
		[HideInInspector]
		[FormerlySerializedAs("toPosition")]
		public Vector3 toPositionOLD;
	}
}
