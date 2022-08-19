using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000DEC RID: 3564
	[CommandInfo("iTween", "Look To", "Rotates a GameObject to look at a supplied Transform or Vector3 over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class LookTo : iTweenCommand
	{
		// Token: 0x060064FD RID: 25853 RVA: 0x0028182C File Offset: 0x0027FA2C
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

		// Token: 0x060064FE RID: 25854 RVA: 0x00281971 File Offset: 0x0027FB71
		public override bool HasReference(Variable variable)
		{
			return this._toTransform.transformRef == variable || this._toPosition.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x060064FF RID: 25855 RVA: 0x002819A4 File Offset: 0x0027FBA4
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

		// Token: 0x040056E0 RID: 22240
		[Tooltip("Target transform that the GameObject will look at")]
		[SerializeField]
		protected TransformData _toTransform;

		// Token: 0x040056E1 RID: 22241
		[Tooltip("Target world position that the GameObject will look at, if no From Transform is set")]
		[SerializeField]
		protected Vector3Data _toPosition;

		// Token: 0x040056E2 RID: 22242
		[Tooltip("Restricts rotation to the supplied axis only")]
		[SerializeField]
		protected iTweenAxis axis;

		// Token: 0x040056E3 RID: 22243
		[HideInInspector]
		[FormerlySerializedAs("toTransform")]
		public Transform toTransformOLD;

		// Token: 0x040056E4 RID: 22244
		[HideInInspector]
		[FormerlySerializedAs("toPosition")]
		public Vector3 toPositionOLD;
	}
}
