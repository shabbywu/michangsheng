using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001230 RID: 4656
	[CommandInfo("iTween", "Look To", "Rotates a GameObject to look at a supplied Transform or Vector3 over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class LookTo : iTweenCommand
	{
		// Token: 0x06007189 RID: 29065 RVA: 0x002A5FB4 File Offset: 0x002A41B4
		public override void DoTween()
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("name", this._tweenName.Value);
			if (this._toTransform.Value == null)
			{
				hashtable.Add("looktarget", this._toPosition.Value);
			}
			else
			{
				hashtable.Add("looktarget", this._toTransform.Value);
			}
			switch (this.axis)
			{
			case iTweenAxis.X:
				hashtable.Add("axis", "x");
				break;
			case iTweenAxis.Y:
				hashtable.Add("axis", "y");
				break;
			case iTweenAxis.Z:
				hashtable.Add("axis", "z");
				break;
			}
			hashtable.Add("time", this._duration.Value);
			hashtable.Add("easetype", this.easeType);
			hashtable.Add("looptype", this.loopType);
			hashtable.Add("oncomplete", "OniTweenComplete");
			hashtable.Add("oncompletetarget", base.gameObject);
			hashtable.Add("oncompleteparams", this);
			iTween.LookTo(this._targetObject.Value, hashtable);
		}

		// Token: 0x0600718A RID: 29066 RVA: 0x0004D35B File Offset: 0x0004B55B
		public override bool HasReference(Variable variable)
		{
			return this._toTransform.transformRef == variable || this._toPosition.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x0600718B RID: 29067 RVA: 0x002A60FC File Offset: 0x002A42FC
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

		// Token: 0x040063F1 RID: 25585
		[Tooltip("Target transform that the GameObject will look at")]
		[SerializeField]
		protected TransformData _toTransform;

		// Token: 0x040063F2 RID: 25586
		[Tooltip("Target world position that the GameObject will look at, if no From Transform is set")]
		[SerializeField]
		protected Vector3Data _toPosition;

		// Token: 0x040063F3 RID: 25587
		[Tooltip("Restricts rotation to the supplied axis only")]
		[SerializeField]
		protected iTweenAxis axis;

		// Token: 0x040063F4 RID: 25588
		[HideInInspector]
		[FormerlySerializedAs("toTransform")]
		public Transform toTransformOLD;

		// Token: 0x040063F5 RID: 25589
		[HideInInspector]
		[FormerlySerializedAs("toPosition")]
		public Vector3 toPositionOLD;
	}
}
