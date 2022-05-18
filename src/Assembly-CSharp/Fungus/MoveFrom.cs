using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001254 RID: 4692
	[CommandInfo("iTween", "Move From", "Moves a game object from a specified position back to its starting position over time. The position can be defined by a transform in another object (using To Transform) or by setting an absolute position (using To Position, if To Transform is set to None).", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class MoveFrom : iTweenCommand
	{
		// Token: 0x060071FB RID: 29179 RVA: 0x002A7380 File Offset: 0x002A5580
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

		// Token: 0x060071FC RID: 29180 RVA: 0x0004D873 File Offset: 0x0004BA73
		public override bool HasReference(Variable variable)
		{
			return this._fromTransform.transformRef == variable || this._fromPosition.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x060071FD RID: 29181 RVA: 0x002A748C File Offset: 0x002A568C
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

		// Token: 0x04006457 RID: 25687
		[Tooltip("Target transform that the GameObject will move from")]
		[SerializeField]
		protected TransformData _fromTransform;

		// Token: 0x04006458 RID: 25688
		[Tooltip("Target world position that the GameObject will move from, if no From Transform is set")]
		[SerializeField]
		protected Vector3Data _fromPosition;

		// Token: 0x04006459 RID: 25689
		[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
		[SerializeField]
		protected bool isLocal;

		// Token: 0x0400645A RID: 25690
		[HideInInspector]
		[FormerlySerializedAs("fromTransform")]
		public Transform fromTransformOLD;

		// Token: 0x0400645B RID: 25691
		[HideInInspector]
		[FormerlySerializedAs("fromPosition")]
		public Vector3 fromPositionOLD;
	}
}
